using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveLogic_New", menuName = "Piece move logic", order = 1)]
public class MoveLogicBase : ScriptableObject
{
    public UnityEngine.GameObject Graphic;

    public virtual BoardSpace[] GetAvailableSpaces(BoardSpace currentBoardSpace, Vector2 forward, Vector2 right)
    {
        BoardSpace[] spaces = new BoardSpace[1];
        spaces[0] = Board.Instance.GetGridSpace(0, 0);
        return spaces;
    }
}
