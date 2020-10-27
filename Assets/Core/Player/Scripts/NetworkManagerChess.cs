using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerChess : NetworkManager
{
    [Header("Chess settings")]
    public TurnManager TurnManager;

    private PlayerControl Player;
    private PlayerControl Player2;

    public Transform MyPieces;
    public Transform RivalPieces;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {

        Transform startPos = GetStartPosition();
        UnityEngine.GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);

        NetworkServer.AddPlayerForConnection(conn, player);
        
        if (Player == null)
        {
            Player = player.GetComponent<PlayerControl>();
            Player.name = "White player";
            
        }
        else
        {
            Player2 = player.GetComponent<PlayerControl>();
            Player2.name = "Black player";

            StartCoroutine(Delay());
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.1f);

            AssignPiecesToPlayers();
            PaintBlackPieces();

            Player.RpcSetName("White player");
            Player2.RpcSetName("Black player");

            TurnManager.Initialize();
        }

    }

    private void AssignPiecesToPlayers()
    {
        foreach (Transform child in MyPieces)
        {
            var piece = child.GetComponentInChildren<ChessPiece>();
            piece.MyPlayerID = Player.netId;
        }
        foreach (Transform child in RivalPieces)
        {
            var piece = child.GetComponentInChildren<ChessPiece>();
            piece.MyPlayerID = Player2.netId;
        }
    }

    private void PaintBlackPieces()
    {
        foreach (Transform child in RivalPieces)
        {
            var rend = child.GetComponentInChildren<Renderer>();
            rend.material.color = new Color(84.0f / 255.0f, 84.0f / 255.0f, 84.0f / 255.0f);
        }
    }
}
