using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public class PixelLayer : MonoBehaviour, ILayer
{
    //Variables
    private RectTransform m_rectTransform;
    private Image m_renderer;
    private Texture2D m_texture;

    //Methods
    /// <summary>
    /// Create the pixel layer using a width and a height.
    /// </summary>
    public void Create()
    {
        int width = CanvasBehaviour.width;
        int height = CanvasBehaviour.height;

        m_renderer = GetComponent<Image>();

        m_rectTransform = GetComponent<RectTransform>();
        m_rectTransform.offsetMin = Vector2.zero;
        m_rectTransform.offsetMax = Vector2.zero;

        m_texture = new Texture2D(width, height);
        m_texture.alphaIsTransparency = true;
        m_texture.filterMode = FilterMode.Point;

        Fill(Color.clear);

        m_renderer.material.SetTexture("_MainTex", m_texture);
    }

    /// <summary>
    /// Fill the layer using a color.
    /// </summary>
    /// <param name="color"> The color to set to every pixel </param>
    private void Fill(Color color)
    {
        for (int x = 0; x < m_texture.width; x++)
        {
            for (int y = 0; y < m_texture.height; y++)
            {
                m_texture.SetPixel(x, y, color);
            }
        }

        m_texture.Apply();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int x = 0; x < m_texture.width; x++)
            {
                for (int y = 0; y < m_texture.height; y++)
                {
                    m_texture.SetPixel(x, y, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1));
                }
            }

            m_texture.Apply();
        }
    }
}
