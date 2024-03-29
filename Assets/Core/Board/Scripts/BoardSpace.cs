﻿using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardSpace : MonoBehaviour
{
    private const int LAYER_DEFAULT = 0;
    private const int LAYER_IGNORE_RAYCAST = 2;

    public GameObject Highlight;
    public int x = 0;
    public int y = 0;

    public Color SelectColor;

    public Color HoverColor;
    public Color ClickColor;

    public ChessPiece Piece { get; set; }

    Renderer HighlightRend;

    private void Start()
    {
        HighlightRend = Highlight.GetComponent<Renderer>();
    }

    public void OccupySpace(ChessPiece newPiece)
    {
        if (Piece != null)
        {
            Piece.Die(newPiece);
        }
        newPiece.transform.position = transform.position;
        Piece = newPiece;
    }

    public void EmptySpace()
    {
        Piece = null;
    }

    public bool IsOccupied()
    {
        return Piece != null;
    }

    public void HighlightHover()
    {
        DoHighlight(HoverColor);
    }

    public void HighlightClick()
    {
        DoHighlight(ClickColor);
        gameObject.layer = LAYER_DEFAULT;
    }

    public void HighlightSelect()
    {
        DoHighlight(SelectColor);
    }


    public void ClearHighlight()
    {
        gameObject.layer = LAYER_IGNORE_RAYCAST;
        Highlight.SetActive(false);
    }

    private void DoHighlight(Color color)
    {
        Highlight.SetActive(true);
        HighlightRend.material.color = color;
        HighlightRend.material.SetColor("_Emission", color);
    }

}
