using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveLogic_Knight", menuName = "Knight move logic", order = 1)]
public class MoveLogicKnight : MoveLogicBase
{

    public override BoardSpace[] GetAvailableSpaces(int x, int y)
    {
        List<BoardSpace> spaces = new List<BoardSpace>();
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
