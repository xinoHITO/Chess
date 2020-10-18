using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveLogic_Pawn", menuName = "Pawn move logic", order = 1)]
public class MoveLogicPawn : MoveLogicBase
{
    public override GridSpace[] GetAvailableSpaces(int x, int y)
    {
        List<GridSpace> spaces = new List<GridSpace>();
        spaces.Add(Board.Instance.GetGridSpace(x, y + 1));
        spaces.Add(Board.Instance.GetGridSpace(x, y + 2));
        return spaces.ToArray();
    }
}
