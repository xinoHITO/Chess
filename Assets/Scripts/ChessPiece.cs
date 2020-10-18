using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    private static Board BoardManager;

    void Start()
    {
        BoardManager = Board.Instance;
        if (BoardManager == null)
        {
            Debug.LogError("No board found");
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MoveTo(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10));
        }
    }

    public void MoveTo(int x, int y)
    {
        if (x >= BoardManager.Rows || y >= BoardManager.Columns)
        {
            Debug.LogError(string.Format("Invalid board position x:{0} | y:{1}",x,y));
            return;
        }

        var gridSpace = BoardManager.GetGridSpace(x, y);
        if (gridSpace == null)
        {
            Debug.LogError("Board space not found");
            return;
        }

        transform.position = gridSpace.transform.position;
    }

    
}
