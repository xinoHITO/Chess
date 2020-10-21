using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveLogic_King", menuName = "King move logic", order = 1)]
public class MoveLogicKing : MoveLogicBase
{
    public override BoardSpace[] GetAvailableSpaces(BoardSpace currentBoardSpace, Vector2 forward, Vector2 right)
    {
        List<BoardSpace> spaces = new List<BoardSpace>();
        Vector2 currentPos = new Vector2(currentBoardSpace.x, currentBoardSpace.y);

        AddToSpaces(currentPos + right          , spaces);
        AddToSpaces(currentPos + right + forward, spaces);
        AddToSpaces(currentPos         + forward, spaces);
        AddToSpaces(currentPos - right + forward, spaces);
        AddToSpaces(currentPos - right          , spaces);
        AddToSpaces(currentPos - right - forward, spaces);
        AddToSpaces(currentPos         - forward, spaces);
        AddToSpaces(currentPos + right - forward , spaces);

        return spaces.ToArray();
    }

    private static void AddToSpaces(Vector2 pos, List<BoardSpace> spaces)
    {
        BoardSpace space = Board.Instance.GetGridSpace(pos);
        if (space != null)
        {
            spaces.Add(space);
        }

    }
}
