using Packages.Rider.Editor.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public MoveLogicBase MoveLogic;

    protected static Board BoardManager;

    protected GridSpace CurrentSpace;

    void Start()
    {
        BoardManager = Board.Instance;
        if (BoardManager == null)
        {
            Debug.LogError("No board found");
            return;
        }
        BoardManager.OnCreatedBoard += Initialize;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            var spaces = MoveLogic?.GetAvailableSpaces(CurrentSpace.x, CurrentSpace.y);
            foreach (var space in spaces)
            {
                Debug.DrawRay(space.transform.position, Vector3.up * 1000, Color.green);
            }
        }
    }
#endif

    private void Initialize()
    {
        Vector3 origin = transform.position + Vector3.up * 100;
        RaycastHit hitInfo;
        if (Physics.Raycast(origin, Vector3.down, out hitInfo, 1000))
        {
            CurrentSpace = hitInfo.collider?.GetComponent<GridSpace>();
        }
        else
        {
            CurrentSpace = BoardManager.GetGridSpace(0, 0);
        }
    }

    protected virtual GridSpace[] GetAvailableSpaces()
    {
        GridSpace[] spaces = new GridSpace[1];
        spaces[0] = BoardManager.GetGridSpace(0, 0);
        return spaces;
    }

    private void MoveTo(int newX, int newY)
    {
        if (newX >= BoardManager.Rows || newY >= BoardManager.Columns)
        {
            Debug.LogError(string.Format("Invalid board position x:{0} | y:{1}", newX, newY));
            return;
        }

        var gridSpace = BoardManager.GetGridSpace(newX, newY);
        if (gridSpace == null)
        {
            Debug.LogError("Board space not found");
            return;
        }

        Debug.Log(string.Format("Moved to x:{0} | y:{1}", newX, newY));
        gridSpace = CurrentSpace;
        transform.position = gridSpace.transform.position;
    }

}
