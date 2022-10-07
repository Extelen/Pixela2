using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class ExportBehaviour : MonoBehaviour
{
    //Methods
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        bool inputControl = Input.GetKey(KeyCode.LeftControl);
        bool inputShift = Input.GetKey(KeyCode.LeftShift);
        bool inputE = Input.GetKey(KeyCode.E);

        if (inputControl && inputShift && inputE)
        {
            TryExport();
        }
    }

    /// <summary>
    /// Try to export the texture;
    /// </summary>
    private void TryExport()
    {
        // byte[] layerBytes = ImageConversion.EncodeToPNG(LayersBehaviour.layer);
        // File.WriteAllBytes("C:/tmp/layer.png", layerBytes);
        // Debug.Log("Export");
    }
}
