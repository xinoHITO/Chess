using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public LayerMask BoardSpaceMask;
    public LayerMask ChessPieceMask;
    private static Camera MainCamera;

    private ChessPiece LastSelectedPiece;
    private ChessPiece ClickedPiece;

    void Start()
    {
        MainCamera = Camera.main;
    }

    void Update()
    {
        ChessPiece selectedPiece = GetPieceBelowMouse();

        if (ClickedPiece == null)
        {
            HoverPieces(selectedPiece);
        }

        ClickHighlightedBoardSpace();

        ClickPiece(selectedPiece);
    }

    private void HoverPieces(ChessPiece selectedPiece)
    {
        if (selectedPiece == null)
        {
            LastSelectedPiece?.ClearHighlight();
        }
        else
        {
            selectedPiece.HighlightHover();
        }
        LastSelectedPiece = selectedPiece;
    }

    private void ClickPiece(ChessPiece selectedPiece)
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Board.Instance.ClearHighlight();

        ClickedPiece = selectedPiece;
        ClickedPiece?.HighlightClick();
    }

    private void ClickHighlightedBoardSpace()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        BoardSpace boardSpace = null;

        if (Physics.Raycast(ray, out hitInfo, 1000, BoardSpaceMask))
        {
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);
            boardSpace = hitInfo.collider?.GetComponent<BoardSpace>();
        }
        else {
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.magenta);
        }

        if (boardSpace != null)
        {
            ClickedPiece?.MoveTo(boardSpace);
        }
    }

    private ChessPiece GetPieceBelowMouse()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        ChessPiece piece = null;
        if (Physics.Raycast(ray, out hitInfo, 1000, ChessPieceMask))
        {
            piece = hitInfo.collider?.GetComponent<ChessPiece>();
        }
        return piece;
    }

}
