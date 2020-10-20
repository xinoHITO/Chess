using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveLogic_Queen", menuName = "Queen move logic", order = 1)]
public class MoveLogicQueen : MoveLogicBase
{
    public override BoardSpace[] GetAvailableSpaces(int x, int y)
    {
        List<BoardSpace> spaces = new List<BoardSpace>();
        int counter = 1;
        var space = Board.Instance.GetGridSpace(x + counter, y);
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(x + counter, y);
        }

        counter = 1;
        space = Board.Instance.GetGridSpace(x - counter, y);
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(x - counter, y);
        }

        counter = 1;
        space = Board.Instance.GetGridSpace(x, y + counter);
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(x, y + counter);
        }

        counter = 1;
        space = Board.Instance.GetGridSpace(x, y - counter);
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(x, y - counter);
        }




        counter = 1;
        space = Board.Instance.GetGridSpace(x - counter, y - counter);
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(x - counter, y - counter);
        }
        counter = 1;
        space = Board.Instance.GetGridSpace(x - counter, y + counter);
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(x - counter, y + counter);
        }

        counter = 1;
        space = Board.Instance.GetGridSpace(x + counter, y + counter);
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(x + counter, y + counter);
        }

        counter = 1;
        space = Board.Instance.GetGridSpace(x + counter, y - counter);
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(x + counter, y - counter);
        }



        return spaces.ToArray();
    }
}
