using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class ExportBehaviour : MonoBehaviour
{
    //Variables
    [Header("UI Management")]
    [SerializeField] private UI.PanelsManager m_panelsManager;
    [SerializeField] private string m_exportPanelIdentifier;

    private bool m_onExportUI;

    //Methods
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        CheckInput();
    }

    /// <summary>
    /// Handle the input check.
    /// </summary>
    private void CheckInput()
    {
        if (m_onExportUI) return;

        bool inputControl = Input.GetKey(KeyCode.LeftControl);
        bool inputShift = Input.GetKey(KeyCode.LeftShift);
        bool inputE = Input.GetKey(KeyCode.E);

        if (inputControl && inputShift && inputE)
        {
            OpenExportPanel();
        }
    }

    /// <summary>
    /// Open the export panel
    /// </summary>
    private void OpenExportPanel()
    {
        m_panelsManager.ChangeContainer(m_exportPanelIdentifier);
        m_onExportUI = true;
    }

    /// <summary>
    /// Close the export panel.
    /// </summary>
    public void OnExportPanelClose()
    {
        m_onExportUI = false;
    }

    /// <summary>
    /// Export the texture to the selected path.
    /// </summary>
    public void Export()
    {
        // byte[] layerBytes = ImageConversion.EncodeToPNG(LayersBehaviour.layer);
        // File.WriteAllBytes("C:/tmp/layer.png", layerBytes);
        // Debug.Log("Export");
        m_onExportUI = false;
    }
}
