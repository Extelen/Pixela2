using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Panel controller with a simple animation of position and alpha.
    /// </summary>
    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
    public class Panel : Openable
    {
        //Structs
        [System.Serializable]
        private struct ActiveValues
        {
            //Variables
            [SerializeField] private float m_time;
            [SerializeField][Range(0f, 1f)] private float m_alpha;
            [SerializeField] private AnimationCurve m_curve;
            [SerializeField] private Vector2 m_position;

            public float time => m_time;
            public float alpha => m_alpha;
            public AnimationCurve curve => m_curve;
            public Vector2 position => m_position;

            //Constructor
            public ActiveValues(float time, float alpha, Vector2 position)
            {
                m_time = time;
                m_alpha = alpha;
                m_position = position;
                m_curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
            }
        }

        //Variables
        [Header("Settings")]
        [SerializeField] private ActiveValues m_enable = new ActiveValues(0.25f, 1, Vector2.zero);
        [SerializeField] private ActiveValues m_disable = new ActiveValues(0.25f, 0, Vector2.down * 32);

        [Header("References")]
        [SerializeField] private RectTransform m_rectTransform = null;
        [SerializeField] private CanvasGroup m_canvasGroup = null;

        private Coroutine m_actualState = null;

        //Properties
        public float disableTime => m_disable.time;
        public bool isPlaying { get; private set; }

        //Methods
        /// <summary>
        /// Called when the script is loaded or a value is changed in the
        /// inspector (Called in the editor only).
        /// </summary>
        private void OnValidate()
        {
            if (m_rectTransform == null) TryGetComponent(out m_rectTransform);
            if (m_canvasGroup == null) TryGetComponent(out m_canvasGroup);
        }

        /// <summary>
        /// Set the default state of the panel.
        /// </summary>
        public override void SetDefaulState(bool active)
        {
            isOpen = active;
            if (active) OpenInstantly();
            else CloseInstantly();
        }

        /// <summary>
        /// Open the panel smoothly.
        /// </summary>
        public override void Open() => Open(null);

        /// <summary>
        /// Open the panel smoothly and invoke an event at the end.
        /// </summary>
        public void Open(System.Action onEnd)
        {
            if (isOpen) return;

            isOpen = true;
            gameObject.SetActive(true);

            if (m_actualState != null) StopCoroutine(m_actualState);
            m_actualState = StartCoroutine(Brain(true, onEnd));
        }

        /// <summary>
        /// Close the panel smoothly.
        /// </summary>
        public override void Close() => Close(null);

        /// <summary>
        /// Close the panel smoothly and invoke an event at the end.
        /// </summary>
        public void Close(System.Action onEnd)
        {
            if (!isOpen) return;

            isOpen = false;
            m_canvasGroup.interactable = false;
            if (m_actualState != null) StopCoroutine(m_actualState);
            m_actualState = StartCoroutine(Brain(false, onEnd));
        }

        /// <summary>
        /// Open the panel instantly.
        /// </summary>
        public void OpenInstantly()
        {
            isOpen = true;
            gameObject.SetActive(true);
            SetValues(1);
        }

        /// <summary>
        /// Close the panel instantly.
        /// </summary>
        public void CloseInstantly()
        {
            isOpen = false;
            gameObject.SetActive(false);
            SetValues(0);
        }

        /// <summary>
        /// Set the values of the rect transform and the canvas group depending of the interpolation value.
        /// </summary>
        /// <param name="t">The interpolation of the value. 0 is disabled, 1 is enabled.</param>
        private void SetValues(float t)
        {
            m_rectTransform.anchoredPosition = Vector2.Lerp(m_disable.position, m_enable.position, t);
            m_canvasGroup.alpha = Mathf.Lerp(m_disable.alpha, m_enable.alpha, t);
        }

        //Coroutines
        private IEnumerator Brain(bool active, System.Action onEnd)
        {
            //Set values
            isPlaying = true;
            float a = active ? 0 : 1;
            float b = active ? 1 : 0;

            //Do the animation
            ActiveValues values = active ? m_enable : m_disable;

            for (float i = 0; i < values.time; i += Time.deltaTime)
            {
                float t = (i / values.time);
                float lerp = Mathf.Lerp(a, b, values.curve.Evaluate(t));

                SetValues(lerp);
                yield return null;
            }

            //Set finish values
            if (active) m_canvasGroup.interactable = true;

            SetValues(b);

            isOpen = active;
            isPlaying = false;
            onEnd?.Invoke();

            if (!active) gameObject.SetActive(false);
        }
    }
}