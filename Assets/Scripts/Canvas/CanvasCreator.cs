using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UI.Panel))]
public class CanvasCreator : MonoBehaviour
{
    //Variables
    [Header("References")]
    [SerializeField] private CanvasBehaviour m_canvasBehaviour;
    [Space]
    [SerializeField] private TMPro.TMP_InputField m_widthInput;
    [SerializeField] private TMPro.TMP_InputField m_heightInput;
    [Space]
    [SerializeField] private Button m_confirmButton;

    //Methods
    /// <summary>
    /// Check if the canvas can be created using the width and height of the input fields.
    /// </summary>
    public void CheckCreationRequirements()
    {
        int width = int.Parse(m_widthInput.text);
        int height = int.Parse(m_heightInput.text);
        m_confirmButton.interactable = width > 0 && height > 0;
    }

    /// <summary>
    /// Create the canvas with the width and height of the input fields.
    /// </summary>
    public void Create()
    {
        int width = int.Parse(m_widthInput.text);
        int height = int.Parse(m_heightInput.text);
        if (width == 0 || height == 0) return;

        m_canvasBehaviour.Initialize(width, height);
    }
}
