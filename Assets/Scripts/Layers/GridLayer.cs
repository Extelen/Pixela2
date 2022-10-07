using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public class GridLayer : MonoBehaviour, ILayer
{
    //Variables
    [Header("Settings")]
    [SerializeField][ColorUsage(false)] private Color m_colorA;
    [SerializeField][ColorUsage(false)] private Color m_colorB;

    private RectTransform m_rectTransform;
    private Image m_renderer;
    private Texture2D m_texture;

    //Methods
    /// <summary>
    /// Create the grid layer.
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
        m_texture.filterMode = FilterMode.Point;

        Fill(m_colorA, m_colorB);

        m_renderer.sprite = Sprite.Create(m_texture, new Rect(0, 0, width, height), Vector2.one / 2f);
    }

    /// <summary>
    /// Fill the layer using a color.
    /// </summary>
    /// <param name="color"> The color to set to every pixel </param>
    private void Fill(Color colorA, Color colorB)
    {
        colorA.a = 1;
        colorB.a = 1;

        for (int x = 0; x < m_texture.width; x++)
        {
            for (int y = 0; y < m_texture.height; y++)
            {
                Color color = (x + y) % 2 == 0 ? colorA : colorB;
                m_texture.SetPixel(x, y, color);
            }
        }

        m_texture.Apply();
    }
}
