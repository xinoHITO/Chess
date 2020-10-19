using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_LayoutPieces : MonoBehaviour
{
    public PlayerControl Player;
    public PlayerControl Player2;

    public Transform MyPieces;
    public Transform RivalPieces;

    void Start()
    {
        if (Player.MyPieces == null)
        {
            Player.MyPieces = new List<ChessPiece>();
        }

        foreach (Transform child in MyPieces)
        {
            var piece = child.GetComponentInChildren<ChessPiece>();
            Player.MyPieces.Add(piece);
        }

        StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.1f);
            foreach (Transform child in RivalPieces)
            {
                var rend = child.GetComponentInChildren<Renderer>();
                rend.material.color = new Color(84.0f / 255.0f, 84.0f / 255.0f, 84.0f / 255.0f);
                
                if (Player2.MyPieces == null)
                {
                    Player2.MyPieces = new List<ChessPiece>();
                }

                var piece = child.GetComponentInChildren<ChessPiece>();
                Player2.MyPieces.Add(piece);
            }
        }

    }

}
