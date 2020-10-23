using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MoveLogicBase KingData;

    // Start is called before the first frame update
    void Start()
    {
        Board.Instance.OnCreatedBoard += FindKingPieces;
    }

    private void FindKingPieces()
    {
        var pieces = FindObjectsOfType<ChessPiece>();
        foreach (var piece in pieces)
        {
            if (piece.MoveLogic == KingData)
            {
                piece.OnDead += OnKingDied;
            }
        }
    }

    private void OnKingDied(ChessPiece killer, ChessPiece victim)
    {
        Debug.Log(string.Format("{0} won", killer.MyPlayer.name));
    }
}
