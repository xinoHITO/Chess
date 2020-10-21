using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveLogic_Knight", menuName = "Knight move logic", order = 1)]
public class MoveLogicKnight : MoveLogicBase
{
    public override BoardSpace[] GetAvailableSpaces(BoardSpace currentBoardSpace, Vector2 forward, Vector2 right)
    {
        List<BoardSpace> spaces = new List<BoardSpace>();
        Vector2 currentPos = new Vector2(currentBoardSpace.x, currentBoardSpace.y);
        int x = (int)currentPos.x;
        int y = (int)currentPos.y;

        AddToAvailableSpaces(x + 2, y + 1, spaces);
        AddToAvailableSpaces(x + 1, y + 2, spaces);
        AddToAvailableSpaces(x - 2, y + 1, spaces);
        AddToAvailableSpaces(x - 1, y + 2, spaces);

        AddToAvailableSpaces(x + 2, y - 1, spaces);
        AddToAvailableSpaces(x + 1, y - 2, spaces);
        AddToAvailableSpaces(x - 2, y - 1, spaces);
        AddToAvailableSpaces(x - 1, y - 2, spaces);

        return spaces.ToArray();
    }

    private static void AddToAvailableSpaces(int x, int y, List<BoardSpace> spaces)
    {
        var space = Board.Instance.GetGridSpace(x, y);
        if (space != null)
        {
            spaces.Add(space);
        }
    }
}
