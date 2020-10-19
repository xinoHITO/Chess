using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_LayoutPieces : MonoBehaviour
{
    public PlayerControl Player;

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
                var piece = child.GetComponentInChildren<Renderer>();
                piece.material.color = new Color(84.0f / 255.0f, 84.0f / 255.0f, 84.0f / 255.0f);
            }
        }

    }

}
