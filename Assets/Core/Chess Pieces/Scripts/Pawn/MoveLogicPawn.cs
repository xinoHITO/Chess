using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveLogic_Pawn", menuName = "Pawn move logic", order = 1)]
public class MoveLogicPawn : MoveLogicBase
{

    public int[] StartingRows;

    public override BoardSpace[] GetAvailableSpaces(BoardSpace currentBoardSpace, Vector2 forward, Vector2 right)
    {
        List<BoardSpace> spaces = new List<BoardSpace>();
        Vector2 currentPos = new Vector2(currentBoardSpace.x, currentBoardSpace.y);

        bool hasPawnMoved = true;
        foreach (var startingRow in StartingRows)
        {
            if (currentBoardSpace.y == startingRow)
            {
                hasPawnMoved = false;
                break;
            }
        }

        int limit = 1;
        if (!hasPawnMoved)
        {
            limit = 2;
        }

        for (int i = 1; i <= limit; i++)
        {
            var temp = Board.Instance.GetGridSpace(currentPos + (forward * i));
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


        var t = Board.Instance.GetGridSpace(currentPos + forward + right);
        if (t != null && t.IsOccupied())
        {
            spaces.Add(t);
        }

        t = Board.Instance.GetGridSpace(currentPos + forward - right);
        if (t != null && t.IsOccupied())
        {
            spaces.Add(t);
        }

        return spaces.ToArray();
    }
}
