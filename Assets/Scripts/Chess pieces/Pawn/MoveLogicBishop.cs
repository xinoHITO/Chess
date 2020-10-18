using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "MoveLogic_Bishop", menuName = "Bishop move logic", order = 1)]
public class MoveLogicBishop : MoveLogicBase
{
    public override BoardSpace[] GetAvailableSpaces(int x, int y)
    {
        List<BoardSpace> spaces = new List<BoardSpace>();

        int counter = 1;
        var temp = Board.Instance.GetGridSpace(x + counter, y + counter);
        while (temp != null)
        {
            spaces.Add(temp);
            counter++;
            temp = Board.Instance.GetGridSpace(x + counter, y + counter);
        }

        counter = 1;
        temp = Board.Instance.GetGridSpace(x - counter, y + counter);
        while (temp != null)
        {
            spaces.Add(temp);
            counter++;
            temp = Board.Instance.GetGridSpace(x - counter, y + counter);
        }

        return spaces.ToArray();
    }
}
