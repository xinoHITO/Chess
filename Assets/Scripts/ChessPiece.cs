using System;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public MoveLogicBase MoveLogic;

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

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Move();
        }
    }

    private void Move()
    {
        BoardSpace[] spaces = GetAvailableSpaces();
        foreach (var space in spaces)
        {
            Debug.DrawRay(space.transform.position, Vector3.up * 1000, Color.green, 1.0f);
        }

        if (spaces.Length > 0)
        {
            int index = UnityEngine.Random.Range(0, spaces.Length);
            MoveTo(spaces[index]);
        }
    }

#endif

    public BoardSpace[] GetAvailableSpaces()
    {
        if (CurrentSpace != null)
        {
            return MoveLogic?.GetAvailableSpaces(CurrentSpace.x, CurrentSpace.y);
        }
        else
        {
            return new BoardSpace[0];
        }
    }

    private void Initialize()
    {
        CurrentSpace = BoardManager.GetGridSpace(this);
        CurrentSpace.OccupySpace(this);
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

}
