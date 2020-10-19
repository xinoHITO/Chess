using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    public LayerMask BoardSpaceMask;
    public LayerMask ChessPieceMask;
    private static Camera MainCamera;

    private ChessPiece LastSelectedPiece;
    private ChessPiece ClickedPiece;

    public List<ChessPiece> MyPieces;

    public bool IsTurnReady = false;

    public UnityAction OnTurnStart;
    public UnityAction OnTurnEnded;

    void Start()
    {
        MainCamera = Camera.main;
    }

    void Update()
    {
        if (!IsTurnReady) return;

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
        if (selectedPiece == null || selectedPiece != LastSelectedPiece)
        {
            Board.Instance.ClearHighlight();
        }
        else
        {
            selectedPiece.HighlightHover();
        }
        LastSelectedPiece = selectedPiece;
    }

    private void ClickHighlightedBoardSpace()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        BoardSpace boardSpace = GetBoardSpaceBelowMouse();

        if (boardSpace != null)
        {
            ClickedPiece?.MoveTo(boardSpace);
            EndTurn();
        }
    }

    private void ClickPiece(ChessPiece selectedPiece)
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Board.Instance.ClearHighlight();

        ClickedPiece = selectedPiece;
        ClickedPiece?.HighlightClick();
    }

    private void EndTurn()
    {
        IsTurnReady = false;
        OnTurnEnded?.Invoke();
    }

    private BoardSpace GetBoardSpaceBelowMouse()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        BoardSpace boardSpace = null;

        if (Physics.Raycast(ray, out hitInfo, 1000, BoardSpaceMask))
        {
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);
            boardSpace = hitInfo.collider?.GetComponent<BoardSpace>();
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.magenta);
        }

        return boardSpace;
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
        if (piece != null && MyPieces.Contains(piece))
        {
            return piece;
        }
        return null;
    }

}
