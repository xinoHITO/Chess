using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveLogic_King", menuName = "King move logic", order = 1)]
public class MoveLogicKing : MoveLogicBase
{
    public override BoardSpace[] GetAvailableSpaces(int x, int y)
    {
        List<BoardSpace> spaces = new List<BoardSpace>();

        AddToSpaces(x + 1, y, spaces);
        AddToSpaces(x + 1, y + 1, spaces);
        AddToSpaces(x, y + 1, spaces);
        AddToSpaces(x - 1, y + 1, spaces);
        AddToSpaces(x - 1, y, spaces);
        AddToSpaces(x - 1, y - 1, spaces);
        AddToSpaces(x, y - 1, spaces);
        AddToSpaces(x + 1, y - 1, spaces);

        return spaces.ToArray();
    }

    private static void AddToSpaces(int x, int y, List<BoardSpace> spaces)
    {
        BoardSpace space = Board.Instance.GetGridSpace(x, y);
        if (space != null)
        {
            spaces.Add(space);
        }

    }
}
