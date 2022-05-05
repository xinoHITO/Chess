using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

public class ChessPiece : NetworkBehaviour
{
    public delegate void OnDeadDelegate(ChessPiece killer, ChessPiece victim);
    public OnDeadDelegate OnDead;

    public GameObject MyPlayer;

    [SyncVar(hook = nameof(OnMyPlayerChanged))]
    public uint MyPlayerID;

    public MoveLogicBase MoveLogic;

    public Transform GraphicContainer;

    protected static Board BoardManager;

    [SyncVar(hook = nameof(OnPositionChanged))]
    protected Vector2 CurrentPosition;
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

    public BoardSpace[] GetAvailableSpaces()
    {
        if (CurrentSpace != null)
        {
            Vector2 forward = new Vector2(MyPlayer.transform.forward.x, MyPlayer.transform.forward.z);
            var availableSpaces = MoveLogic?.GetAvailableSpaces(CurrentSpace, forward, MyPlayer.transform.right);
            List<BoardSpace> result = new List<BoardSpace>();
            foreach (var space in availableSpaces)
            {
                if (space.Piece?.MyPlayer != MyPlayer)
                {
                    result.Add(space);
                }
            }
            return result.ToArray();
        }
        else
        {
            return new BoardSpace[0];
        }
    }

    private void Initialize()
    {
        InitializeGraphic();

        var startPosition = BoardManager.GetGridSpace(this);
        SetPosition(startPosition.x, startPosition.y);
    }

    private void SetPosition(int x, int y)
    {
        if (!hasAuthority) return;

        CmdSetPosition(x, y);
    }

    [Command]
    private void CmdSetPosition(int x, int y)
    {
        CurrentPosition = new Vector2(x, y);
    }

    private void InitializeGraphic()
    {
        foreach (Transform child in GraphicContainer)
        {
            Destroy(child.gameObject);
        }
        GameObject pieceGraphic = Instantiate(MoveLogic.Graphic, GraphicContainer);
        pieceGraphic.transform.localPosition = Vector3.zero;
        pieceGraphic.transform.localRotation = Quaternion.identity;
    }

    public void MoveTo(int x, int y)
    {
        BoardSpace targetSpace = BoardManager.GetGridSpace(x, y);
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

        SetPosition(gridSpace.x, gridSpace.y);
    }

    void OnPositionChanged(Vector2 _, Vector2 newValue)
    {
        int x = (int)newValue.x;
        int y = (int)newValue.y;

        CurrentSpace?.EmptySpace();
        CurrentSpace = BoardManager.GetGridSpace(x, y);
        CurrentSpace?.OccupySpace(this);
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
            space.HighlightHover();
        }
    }

    public void HighlightClick()
    {
        var spaces = GetAvailableSpaces();
        foreach (var space in spaces)
        {
            space.HighlightClick();
        }
    }

    public void HighlightSelect()
    {
        var spaces = GetAvailableSpaces();
        foreach (var space in spaces)
        {
            space.HighlightSelect();
        }
    }

    public void Die(ChessPiece killer)
    {
        OnDead?.Invoke(killer, this);

        Destroy(gameObject);
    }

    void OnMyPlayerChanged(uint _, uint newValue)
    {
        if (NetworkIdentity.spawned.TryGetValue(MyPlayerID, out NetworkIdentity identity))
        {
            MyPlayer = identity.gameObject;
        }
        else
            StartCoroutine(FindMyPlayer());
    }

    IEnumerator FindMyPlayer()
    {
        while (MyPlayer == null)
        {
            yield return null;
            if (NetworkIdentity.spawned.TryGetValue(MyPlayerID, out NetworkIdentity identity))
            {
                MyPlayer = identity.gameObject;
                Debug.Log("FOUND 2!");
            }
        }
    }

}
