using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveLogic_New", menuName = "Piece move logic", order = 1)]
public class MoveLogicBase : ScriptableObject
{
    public virtual GridSpace[] GetAvailableSpaces(int x, int y)
    {
        GridSpace[] spaces = new GridSpace[1];
        spaces[0] = Board.Instance.GetGridSpace(0, 0);
        return spaces;
    }
}
