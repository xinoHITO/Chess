using System;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public MoveLogicBase MoveLogic;

    protected static Board BoardManager;

    protected BoardSpace CurrentSpace;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Move();
        }
    }

    private void Move()
    {
        BoardSpace[] spaces = GetAvailableSpaces();
        foreach (var space in spaces)
        {
            Debug.DrawRay(space.transform.position, Vector3.up * 1000, Color.green, 1.0f);
        }

        if (spaces.Length > 0)
        {
            int index = UnityEngine.Random.Range(0, spaces.Length);
            MoveTo(spaces[index]);
        }
    }

#endif
    public BoardSpace[] GetAvailableSpaces()
    {
        return MoveLogic?.GetAvailableSpaces(CurrentSpace.x, CurrentSpace.y);
    }

    private void Initialize()
    {
        Vector3 origin = transform.position + Vector3.up * 100;
        RaycastHit hitInfo;
        if (Physics.Raycast(origin, Vector3.down, out hitInfo, 1000))
        {
            CurrentSpace = hitInfo.collider?.GetComponent<BoardSpace>();
        }
        else
        {
            CurrentSpace = BoardManager.GetGridSpace(0, 0);
        }
    }

    private void MoveTo(BoardSpace targetSpace)
    {
        if (targetSpace.x >= BoardManager.Rows || targetSpace.y >= BoardManager.Columns)
        {
            Debug.LogError(string.Format("Invalid board position x:{0} | y:{1}", targetSpace.x, targetSpace.y));
            return;
        }

        var gridSpace = BoardManager.GetGridSpace(targetSpace.x, targetSpace.y);
        if (gridSpace == null)
        {
            Debug.LogError("Board space not found");
            return;
        }

        Debug.Log(string.Format("Moved to x:{0} | y:{1}", targetSpace.x, targetSpace.y));
        CurrentSpace = gridSpace;
        transform.position = gridSpace.transform.position;
    }

    public void ClearHighlight()
    {
        var spaces = GetAvailableSpaces();
        foreach (var space in spaces)
        {
            space.ClearHighlight();
        }
    }

    public void HighlightHover()
    {
        var spaces = GetAvailableSpaces();
        foreach (var space in spaces)
        {
            space.HighlightHover(true);
        }
    }

    public void HighlightClick()
    {
        var spaces = GetAvailableSpaces();
        foreach (var space in spaces)
        {
            space.HighlightClick(true);
        }
    }

}
