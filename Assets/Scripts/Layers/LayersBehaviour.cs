using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayersBehaviour : MonoBehaviour
{
    //Variables
    [Header("References")]
    [SerializeField] private RectTransform m_pixelLayersParent;
    [SerializeField] private GameObject m_pixelLayerPrefab;
    [Space]
    [SerializeField] private GridLayer m_gridLayer;

    private List<PixelLayer> m_pixelLayers;

    //Methods
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        m_pixelLayers = new List<PixelLayer>();
    }

    /// <summary>
    /// Create the grid layer using the dimensions.
    /// </summary>
    /// <param name="width"> The width on pixels of the texture. </param>
    /// <param name="height"> The height on pixels of the texture. </param>
    public void InitializeGridLayer()
    {
        m_gridLayer.Create();
    }

    /// <summary>
    /// Create the texture of the layer using the dimensions.
    /// </summary>
    /// <param name="width"> The width on pixels of the texture. </param>
    /// <param name="height"> The height on pixels of the texture. </param>
    public void CreatePixelLayer()
    {
        m_pixelLayersParent.offsetMin = Vector2.zero;
        m_pixelLayersParent.offsetMax = Vector2.zero;

        PixelLayer layer = Instantiate(m_pixelLayerPrefab, m_pixelLayersParent).GetComponent<PixelLayer>();
        layer.Create();
        m_pixelLayers.Add(layer);

        m_gridLayer.Create();
    }

    /// <summary>
    /// Delete all the layers in the canvas.
    /// </summary>
    public void RemoveAllLayers()
    {
        m_pixelLayers.ForEach(c => Destroy(c.gameObject));
        m_pixelLayers.Clear();
    }
}
