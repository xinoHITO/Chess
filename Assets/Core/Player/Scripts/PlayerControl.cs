using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    public enum PlayerState
    {
        Normal,
        HoveringOverPiece,
        ClickedPiece
    }

    public PlayerState State { get; set; }

    private PlayerState NextState;

    public LayerMask BoardSpaceMask;
    public LayerMask ChessPieceMask;
    private static Camera MainCamera;

    private ChessPiece LastSelectedPiece;
    private ChessPiece ClickedPiece;

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

        if (State == PlayerState.Normal || State == PlayerState.HoveringOverPiece)
        {
            HoverPieces(selectedPiece);
        }
        if (State == PlayerState.HoveringOverPiece)
        {
            ClickPiece(selectedPiece);
        }
        if (State == PlayerState.ClickedPiece)
        {
            ClickHighlightedBoardSpace();
        }

        State = NextState;

    }

    private void HoverPieces(ChessPiece selectedPiece)
    {
        if (selectedPiece == null || selectedPiece != LastSelectedPiece)
        {
            if (selectedPiece == null)
            {
                NextState = PlayerState.Normal;
            }
            Board.Instance.ClearHighlight();
        }
        else
        {
            selectedPiece.HighlightHover();
            NextState = PlayerState.HoveringOverPiece;
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
        else {
            NextState = PlayerState.Normal;
        }
    }

    private void ClickPiece(ChessPiece selectedPiece)
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Board.Instance.ClearHighlight();

        ClickedPiece = selectedPiece;
        ClickedPiece?.HighlightClick();

        if (ClickedPiece != null)
        {
            NextState = PlayerState.ClickedPiece;
        }
        else
        {
            NextState = PlayerState.Normal;
        }
    }

    private void EndTurn()
    {
        IsTurnReady = false;
        ReturnToNormal();
        OnTurnEnded?.Invoke();
    }

    private void ReturnToNormal()
    {
        NextState = PlayerState.Normal;
        ClickedPiece = null;
        Board.Instance.ClearHighlight();
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
        
        if (piece != null && piece.MyPlayer == this)
        {
            return piece;
        }
        return null;
    }

}
