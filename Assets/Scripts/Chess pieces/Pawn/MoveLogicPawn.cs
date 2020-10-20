using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveLogic_Pawn", menuName = "Pawn move logic", order = 1)]
public class MoveLogicPawn : MoveLogicBase
{
    public override BoardSpace[] GetAvailableSpaces(int x, int y)
    {
        List<BoardSpace> spaces = new List<BoardSpace>();
        for (int i = 1; i <= 2; i++)
        {
            var temp = Board.Instance.GetGridSpace(x, y + i);
            if (temp != null)
            {
                if (!temp.IsOccupied())
                {
                    spaces.Add(temp);
                }
                else
                {
                    break;
                }
            }
        }

        var t = Board.Instance.GetGridSpace(x + 1, y + 1);
        if (t != null && t.IsOccupied())
        {
            spaces.Add(t);
        }

        t = Board.Instance.GetGridSpace(x - 1, y + 1);
        if (t != null && t.IsOccupied())
        {
            spaces.Add(t);
        }

        return spaces.ToArray();
    }
}
