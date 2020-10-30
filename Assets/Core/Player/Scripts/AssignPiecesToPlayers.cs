using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPiecesToPlayers : NetworkBehaviour
{
    public Color WhiteColor;
    public Color BlackColor;


    private uint WhitePlayerId;
    private uint BlackPlayerId;

    // Start is called before the first frame update
    void Start()
    {
        if (!isServer) return;

        NetworkManagerChess.OnGameIsReady += CachePlayerIds;
        Board.Instance.OnCreatedBoard += AssignPieces;
    }

    private void CachePlayerIds(uint whitePlayerId, uint blackPlayerId)
    {
        RpcCachePlayerIds(whitePlayerId, blackPlayerId);
    }

    [ClientRpc]
    private void RpcCachePlayerIds(uint whitePlayerId, uint blackPlayerId)
    {
        WhitePlayerId = whitePlayerId;
        BlackPlayerId = blackPlayerId;
    }

    private void AssignPieces()
    {
        RpcAssignPieces(WhitePlayerId, BlackPlayerId);
    }

    [ClientRpc]
    private void RpcAssignPieces(uint whitePlayerId, uint blackPlayerId)
    {
        StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.1f);
            var chessPieces = FindObjectsOfType<ChessPiece>();
            PaintPieces(chessPieces, whitePlayerId, WhiteColor);
            PaintPieces(chessPieces, blackPlayerId, BlackColor);
        }
    }

    void PaintPieces(ChessPiece[] pieces, uint playerId, Color color)
    {
        foreach (ChessPiece chessPiece in pieces)
        {
            if (chessPiece.MyPlayerID == playerId)
            {
                chessPiece.GetComponentInChildren<Renderer>().material.color = color;
            }
        }
    }
}
