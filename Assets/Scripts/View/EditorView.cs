using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class EditorView : MonoBehaviour
{
    //States
    private enum State
    {
        None,
        Move,
        Rotate,
        Scale
    }

    //Variables
    [Header("References")]
    [SerializeField] private RectTransform m_positionPivot;
    [SerializeField] private Transform m_rotationPivot;
    [SerializeField] private Transform m_scalePivot;

    [Header("Position")]
    [SerializeField] private float m_positionDampSmoothTime = 0.1f;

    [Header("Scale")]
    [SerializeField] private float m_scaleDampSmoothTime = 0.1f;
    [SerializeField] private float m_scaleIntensity = 1f;
    [SerializeField] private float m_scaleMin = 0.1f;
    [SerializeField] private float m_scaleMax = 4f;

    private State m_lastState;
    private State m_state;

    private Vector2 m_canvasSize;

    //Input
    private bool m_inputSpace;
    private bool m_inputShift;
    private bool m_inputCtrl;

    //Drag
    private bool m_dragging;

    private Vector2 m_dragStart;

    //Position
    private Vector2 m_positionDragReach;
    private Vector2 m_positionDragVelocity;
    private Vector2 m_positionDragEnd;

    //Zoom
    private float m_scale;
    private float m_scaleDragReach;
    private float m_scaleDragVelocity;
    private float m_scaleDragEnd;

    //Components
    private Canvas m_canvas;

    //Methods
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        m_canvas = GetComponentInParent<Canvas>();
        m_canvasSize = (m_canvas.transform as RectTransform).sizeDelta / m_canvas.scaleFactor;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        m_state = State.None;

        m_scale = 1;
        m_scaleDragReach = 1;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition / m_canvas.scaleFactor;

        CheckInputs();
        CheckDrag(mousePosition);

        switch (m_state)
        {
            case State.Move: MoveEditor(mousePosition); break;
            case State.Rotate: RotateEditor(); break;
            case State.Scale: ScaleEditor(mousePosition.x); break;
        }

        HandleEditorTransform();
    }

    /// <summary>
    /// Check the inputs and set the state.
    /// </summary>
    private void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space)) m_inputSpace = true;
        if (Input.GetKeyUp(KeyCode.Space)) m_inputSpace = false;

        if (Input.GetKeyDown(KeyCode.LeftShift)) m_inputShift = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) m_inputShift = false;

        if (Input.GetKeyDown(KeyCode.LeftControl)) m_inputCtrl = true;
        if (Input.GetKeyUp(KeyCode.LeftControl)) m_inputCtrl = false;

        if (m_inputSpace && m_inputCtrl) { m_state = State.Scale; return; }
        else if (m_inputSpace && m_inputShift) { m_state = State.Rotate; return; }
        else if (m_inputSpace) { m_state = State.Move; return; }
        else { m_state = State.None; return; }
    }

    /// <summary>
    /// Check if the user is dragging.
    /// </summary>
    private void CheckDrag(Vector2 mousePosition)
    {
        if (m_lastState != m_state)
        {
            m_lastState = m_state;
            m_dragging = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            m_dragging = true;
            m_dragStart = mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_dragging = false;

            m_positionDragEnd = m_positionDragReach;
            m_scaleDragEnd = m_scaleDragReach;
        }
    }

    /// <summary>
    /// Move the position of the canvas editor rect transform.
    /// </summary>
    private void MoveEditor(Vector2 mousePosition)
    {
        if (m_dragging)
        {
            m_positionDragReach = m_positionDragEnd + ((mousePosition - m_dragStart) / m_scale);
        }
    }

    /// <summary>
    /// Rotate the editor using the rotation pivot transform reference.
    /// </summary>
    private void RotateEditor()
    {

    }

    /// <summary>
    /// Scale to the mouse position.
    /// </summary>
    private void ScaleEditor(float mouseX)
    {
        if (m_dragging)
        {
            m_scaleDragReach = ((mouseX - m_dragStart.x) / m_canvasSize.x) * m_scaleIntensity;
            m_scaleDragReach = m_scaleDragEnd + m_scaleDragReach;
            m_scaleDragReach = Mathf.Clamp(m_scaleDragReach, m_scaleMin, m_scaleMax);
        }
    }

    /// <summary>
    /// Handle the transform position and rotation.
    /// </summary>
    private void HandleEditorTransform()
    {
        //Position
        m_positionPivot.anchoredPosition = Vector2.SmoothDamp(m_positionPivot.anchoredPosition, m_positionDragReach, ref m_positionDragVelocity, m_positionDampSmoothTime);

        //Scale
        m_scale = Mathf.SmoothDamp(m_scale, m_scaleDragReach, ref m_scaleDragVelocity, m_scaleDampSmoothTime);
        m_scalePivot.localScale = Vector3.one * m_scale;
    }
}

