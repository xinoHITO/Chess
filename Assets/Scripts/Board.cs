using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Board : MonoBehaviour
{
    public GridSpace GridSpacePrefab;
    public float GridSpaceSize = 1;

    [Header("Board size")]
    public int Rows = 8;
    public int Columns = 8;

    public UnityAction OnCreatedBoard;

    public static Board Instance;

    private static GridSpace[] GridSpaces;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateBoardSpaces();

        CacheBoardSpaces();

        StartCoroutine(DelayCoroutine());

        IEnumerator DelayCoroutine()
        {
            yield return null;
            OnCreatedBoard?.Invoke();
        }
    }

    private void CreateBoardSpaces()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                GridSpace boardGrid = Instantiate(GridSpacePrefab, transform);
                boardGrid.x = j;
                boardGrid.y = i;
                boardGrid.transform.position = transform.position + new Vector3(GridSpaceSize * j, 0, GridSpaceSize * i);
            }
        }

    }

    private void CacheBoardSpaces()
    {
        if (GridSpaces == null)
        {
            GridSpaces = FindObjectsOfType<GridSpace>();
        }
    }

    public GridSpace GetGridSpace(int x, int y)
    {
        foreach (var gridSpace in GridSpaces)
        {
            if (gridSpace.x == x && gridSpace.y == y)
            {
                return gridSpace;
            }
        }
        return null;
    }

}

