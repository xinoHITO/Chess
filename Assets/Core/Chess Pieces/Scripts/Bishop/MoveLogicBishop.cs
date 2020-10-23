using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "MoveLogic_Bishop", menuName = "Bishop move logic", order = 1)]
public class MoveLogicBishop : MoveLogicBase
{
    public override BoardSpace[] GetAvailableSpaces(BoardSpace currentBoardSpace, Vector2 forward, Vector2 right)
    {
        List<BoardSpace> spaces = new List<BoardSpace>();
        Vector2 currentPos = new Vector2(currentBoardSpace.x, currentBoardSpace.y);

        //forward-right
        int counter = 1;
        var space = Board.Instance.GetGridSpace(currentPos + ((forward + right) * counter));
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

        return spaces.ToArray();
    }
}
