using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_LayoutPieces : MonoBehaviour
{
    public ChessPiece[] Pieces;
    public PlayerControl Player;

    void Start()
    {
        if (Player.MyPieces == null)
        {
            Player.MyPieces = new List<ChessPiece>();
        }
        
        foreach (var piece in Pieces)
        {
            Player.MyPieces.Add(piece);
        }
    }

}
