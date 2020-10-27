using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChessPiece : NetworkBehaviour
{
    public delegate void OnDeadDelegate(ChessPiece killer,ChessPiece victim);
    public OnDeadDelegate OnDead;
    
    public UnityEngine.GameObject MyPlayer;

    [SyncVar(hook = nameof(OnMyPlayerChanged))]
    public uint MyPlayerID;

    public MoveLogicBase MoveLogic;

    public Transform GraphicContainer;

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

        CurrentSpace = BoardManager.GetGridSpace(this);
        CurrentSpace.OccupySpace(this);
    }

    private void InitializeGraphic()
    {
        foreach (Transform child in GraphicContainer)
        {
            Destroy(child.gameObject);
        }
        UnityEngine.GameObject pieceGraphic = Instantiate(MoveLogic.Graphic, GraphicContainer);
        pieceGraphic.transform.localPosition = Vector3.zero;
        pieceGraphic.transform.localRotation = Quaternion.identity;
    }

    public void MoveTo(BoardSpace targetSpace)
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

        CurrentSpace.EmptySpace();
        CurrentSpace = gridSpace;
        CurrentSpace.OccupySpace(this);
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
    
    public void Die(ChessPiece killer) {
        OnDead?.Invoke(killer,this);

        Destroy(gameObject);
    }


    void OnMyPlayerChanged(uint _, uint newValue)
    {
        Debug.Log(string.Format("OnMyPlayerChanged | {0} | uid:{1}", gameObject.name, newValue));
        if (NetworkIdentity.spawned.TryGetValue(MyPlayerID, out NetworkIdentity identity)) { 
            MyPlayer = identity.gameObject;
            Debug.Log("FOUND 1!");
        }
        else
            StartCoroutine(FindMyPlayer());
    }

    IEnumerator FindMyPlayer()
    {
        while (MyPlayer == null)
        {
            yield return null;
            if (NetworkIdentity.spawned.TryGetValue(MyPlayerID, out NetworkIdentity identity)) { 
                MyPlayer = identity.gameObject;
                Debug.Log("FOUND 2!");
            }
        }
    }

}
