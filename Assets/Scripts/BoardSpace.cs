using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpace : MonoBehaviour
{
    public GameObject Highlight;
    public int x = 0;
    public int y = 0;

    public Color HoverColor;
    public Color ClickColor;

    Renderer HighlightRend;

    private void Start()
    {
        HighlightRend = Highlight.GetComponent<Renderer>();
    }

    public void HighlightHover(bool value)
    {
        DoHighlight(value, HoverColor);
    }

    public void HighlightClick(bool value)
    {
        DoHighlight(value, ClickColor);
    }

    public void ClearHighlight()
    {
        Highlight.SetActive(false);
    }

    private void DoHighlight(bool value, Color color)
    {
        Highlight.SetActive(value);
        HighlightRend.material.color = color;
        HighlightRend.material.SetColor("_Emission", color);
    }
}
