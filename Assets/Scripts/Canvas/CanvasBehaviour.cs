using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehaviour : MonoBehaviour
{
    //Variables
    /// <summary>
    /// Check if the canvas is initialized.
    /// </summary>
    public static bool initialized { get; private set; }

    /// <summary>
    /// The width of the canvas.
    /// </summary>
    public static Vector2Int size { get; private set; }

    /// <summary>
    /// The width of the canvas.
    /// </summary>
    public static int width { get; private set; }

    /// <summary>
    /// The height of the canvas.
    /// </summary>
    public static int height { get; private set; }

    [Header("Pixel Canvas Scale")]
    [SerializeField] private RectTransform m_pixelCanvas;
    [SerializeField] private int m_initialScaleScreenSideOffset = 64;
    [Space]
    [SerializeField] private float m_scaleTime = 0.2f;
    [SerializeField] private AnimationCurve m_scaleCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

    [Header("References")]
    [SerializeField] private Canvas m_canvas;
    [SerializeField] private LayersBehaviour m_layersBehaviour;

    //Methods
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        m_pixelCanvas.localScale = Vector3.zero;
    }

    /// <summary>
    /// Initialize the canvas using a width and an height.
    /// </summary>
    /// <param name="width"> The width to set to the canvas </param>
    /// <param name="height"> The height to set to the canvas </param>
    public void Initialize(int width, int height)
    {
        initialized = true;

        CanvasBehaviour.size = new Vector2Int(width, height);
        CanvasBehaviour.width = width;
        CanvasBehaviour.height = height;

        m_pixelCanvas.sizeDelta = new Vector2(width, height);

        float screenSide;
        float maxCanvasSide = Mathf.Max(width, height);

        if (width > height) screenSide = Screen.width;
        else if (width < height) screenSide = Screen.height;
        else screenSide = Mathf.Min(Screen.width, Screen.height);

        screenSide /= m_canvas.scaleFactor;
        screenSide -= m_initialScaleScreenSideOffset * 2;

        m_layersBehaviour.InitializeGridLayer();
        m_layersBehaviour.CreatePixelLayer();

        StartCoroutine(PixelCanvasScaleTween(screenSide / maxCanvasSide));
    }

    //Coroutines
    private IEnumerator PixelCanvasScaleTween(float scale)
    {
        Vector3 a = m_pixelCanvas.localScale;
        Vector3 b = Vector3.one * scale;

        for (float i = 0; i < m_scaleTime; i += Time.deltaTime)
        {
            float t = m_scaleCurve.Evaluate(i / m_scaleTime);
            m_pixelCanvas.localScale = Vector3.Lerp(a, b, t);
            yield return null;
        }

        m_pixelCanvas.localScale = b;
    }
}
