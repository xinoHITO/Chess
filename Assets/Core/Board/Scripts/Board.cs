using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Board : MonoBehaviour
{
    public BoardSpace GridSpacePrefab;
    public float GridSpaceSize = 1;

    [Header("Board size")]
    public int Rows = 8;
    public int Columns = 8;

    public UnityAction OnCreatedBoard;

    public static Board Instance;

    private static BoardSpace[] BoardSpaces;

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
                BoardSpace boardGrid = Instantiate(GridSpacePrefab, transform);
                boardGrid.x = j;
                boardGrid.y = i;
                boardGrid.transform.position = transform.position + new Vector3(GridSpaceSize * j, 0, GridSpaceSize * i);
            }
        }

    }

    private void CacheBoardSpaces()
    {
        if (BoardSpaces == null)
        {
            BoardSpaces = FindObjectsOfType<BoardSpace>();
        }
    }

    public BoardSpace GetGridSpace(ChessPiece piece)
    {
        Vector3 origin = piece.transform.position + Vector3.up * 100;
        RaycastHit hitInfo;

        List<string> layerNames = new List<string>();
        layerNames.Add(LayerMask.LayerToName(piece.gameObject.layer));
        int mask = LayerMask.GetMask(layerNames.ToArray());
        mask = ~mask;

        BoardSpace space;
        if (Physics.Raycast(origin, Vector3.down, out hitInfo, 1000, mask))
        {
            space = hitInfo.collider?.GetComponent<BoardSpace>();
        }
        else
        {
            space = GetGridSpace(0, 0);
        }

        return space;
    }

    public BoardSpace GetGridSpace(Vector2 pos) {
        int x = (int)pos.x;
        int y = (int)pos.y;
        return GetGridSpace(x, y);
    }

    public BoardSpace GetGridSpace(int x, int y)
    {
        foreach (var gridSpace in BoardSpaces)
        {
            if (gridSpace.x == x && gridSpace.y == y)
            {
                return gridSpace;
            }
        }
        return null;
    }

    public void ClearHighlight()
    {
        foreach (var space in BoardSpaces)
        {
            space.ClearHighlight();
        }
    }

}

