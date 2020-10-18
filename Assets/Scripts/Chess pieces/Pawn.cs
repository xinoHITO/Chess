using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    protected override GridSpace[] GetAvailableSpaces()
    {
        List<GridSpace> spaces = new List<GridSpace>();
        spaces.Add(BoardManager.GetGridSpace(CurrentSpace.x, CurrentSpace.y+1));
        spaces.Add(BoardManager.GetGridSpace(CurrentSpace.x, CurrentSpace.y+2));
        return spaces.ToArray();
    }
}
