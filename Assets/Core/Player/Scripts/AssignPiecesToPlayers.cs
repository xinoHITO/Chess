using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPiecesToPlayers : NetworkBehaviour
{
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
            PaintPieces(chessPieces, whitePlayerId, Color.white);
            PaintPieces(chessPieces, blackPlayerId, Color.black);
        }
    }

    void PaintPieces(ChessPiece[] pieces, uint playerId, Color color)
    {
        Debug.LogError(string.Format("PAINT PIECES | Color:{0}", color));
        foreach (ChessPiece chessPiece in pieces)
        {
            if (chessPiece.MyPlayerID == playerId)
            {
                chessPiece.GetComponentInChildren<Renderer>().material.color = color;
            }
        }
    }
}
