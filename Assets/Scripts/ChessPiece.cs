using System;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public PlayerControl MyPlayer;

    public MoveLogicBase MoveLogic;

    public Transform GraphicContainer;

    protected static Board BoardManager;

    protected BoardSpace CurrentSpace;

    void Start()
    {
        BoardManager = Board.Instance;
        if (BoardManager == null)
        {
            Debug.LogError("No board found");
            return;
        }
        BoardManager.OnCreatedBoard += Initialize;
    }

    public BoardSpace[] GetAvailableSpaces()
    {
        if (CurrentSpace != null)
        {
            var availableSpaces = MoveLogic?.GetAvailableSpaces(CurrentSpace.x, CurrentSpace.y);
            List<BoardSpace> result = new List<BoardSpace>();
            foreach (var space in availableSpaces)
            {
                if (space.Piece?.MyPlayer != MyPlayer)
                {
                    result.Add(space);
                }
            }
            return result.ToArray();
        }
        else
        {
            return new BoardSpace[0];
        }
    }

    private void Initialize()
    {
        InitializeGraphic();

        CurrentSpace = BoardManager.GetGridSpace(this);
        CurrentSpace.OccupySpace(this);
    }

    private void InitializeGraphic()
    {
        foreach (Transform child in GraphicContainer)
        {
            Destroy(child.gameObject);
        }
        GameObject pieceGraphic = Instantiate(MoveLogic.Graphic, GraphicContainer);
        pieceGraphic.transform.localPosition = Vector3.zero;
        pieceGraphic.transform.localRotation = Quaternion.identity;
    }

    public void MoveTo(BoardSpace targetSpace)
    {
        if (targetSpace.x >= BoardManager.Rows || targetSpace.y >= BoardManager.Columns)
        {
            Debug.LogError(string.Format("Invalid board position x:{0} | y:{1}", targetSpace.x, targetSpace.y));
            return;
        }

        var gridSpace = BoardManager.GetGridSpace(targetSpace.x, targetSpace.y);
        if (gridSpace == null)
        {
            Debug.LogError("Board space not found");
            return;
        }

        CurrentSpace.EmptySpace();
        CurrentSpace = gridSpace;
        CurrentSpace.OccupySpace(this);
    }

    public void ClearHighlight()
    {
        var spaces = GetAvailableSpaces();
        foreach (var space in spaces)
        {
            space.ClearHighlight();
        }
    }

    public void HighlightHover()
    {
        var spaces = GetAvailableSpaces();
        foreach (var space in spaces)
        {
            space.HighlightHover();
        }
    }

    public void HighlightClick()
    {
        var spaces = GetAvailableSpaces();
        foreach (var space in spaces)
        {
            space.HighlightClick();
        }
    }

    public void HighlightSelect()
    {
        var spaces = GetAvailableSpaces();
        foreach (var space in spaces)
        {
            space.HighlightSelect();
        }
    }

}
