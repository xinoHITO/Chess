using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveLogic_Queen", menuName = "Queen move logic", order = 1)]
public class MoveLogicQueen : MoveLogicBase
{
    public override BoardSpace[] GetAvailableSpaces(BoardSpace currentBoardSpace, Vector2 forward, Vector2 right)
    {
        List<BoardSpace> spaces = new List<BoardSpace>();
        Vector2 currentPos = new Vector2(currentBoardSpace.x, currentBoardSpace.y);

        //right
        int counter = 1;
        var space = Board.Instance.GetGridSpace(currentPos + (right * counter));
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(currentPos + (right * counter));
        }
        //left
        counter = 1;
        space = Board.Instance.GetGridSpace(currentPos + (-right * counter));
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(currentPos + (-right * counter));
        }
        //forward
        counter = 1;
        space = Board.Instance.GetGridSpace(currentPos + (forward * counter));
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(currentPos + (forward * counter));
        }
        //backwards
        counter = 1;
        space = Board.Instance.GetGridSpace(currentPos + (-forward * counter));
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(currentPos + (-forward * counter));
        }



        //backward-left
        counter = 1;
        space = Board.Instance.GetGridSpace(currentPos + ((-forward - right) * counter));
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(currentPos + ((-forward - right) * counter));
        }
        //forward-left
        counter = 1;
        space = Board.Instance.GetGridSpace(currentPos + ((forward - right) * counter));
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(currentPos + ((forward - right) * counter));
        }
        //forward-right
        counter = 1;
        space = Board.Instance.GetGridSpace(currentPos + ((forward + right) * counter));
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(currentPos + ((forward + right) * counter));
        }
        //backward-right
        counter = 1;
        space = Board.Instance.GetGridSpace(currentPos + ((-forward + right) * counter));
        while (space != null)
        {
            spaces.Add(space);
            counter++;
            if (space.IsOccupied())
            {
                break;
            }
            space = Board.Instance.GetGridSpace(currentPos + ((-forward + right) * counter));
        }



        return spaces.ToArray();
    }
}
