using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;
using Cinemachine;

public class NetworkManagerChess : NetworkManager
{
    [Header("Chess settings")]
    private PlayerControl Player;
    private PlayerControl Player2;

    public Transform MyPieces;
    public Transform RivalPieces;

    public delegate void OnGameIsReadyDelegate(uint whitePlayerId, uint blackPlayerId);
    public static OnGameIsReadyDelegate OnGameIsReady;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {

        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
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
            yield return new WaitForSeconds(0.5f);

            AssignPiecesToPlayers();

            Player.RpcSetName("White player");
            Player2.RpcSetName("Black player");

            OnGameIsReady?.Invoke(Player.netId, Player2.netId);
        }

    }

    private void AssignPiecesToPlayers()
    {
        foreach (Transform child in MyPieces)
        {
            var piece = child.GetComponentInChildren<ChessPiece>();
            piece.MyPlayerID = Player.netId;
            var playerConnection = Player.GetComponent<NetworkIdentity>().connectionToClient;
            piece.GetComponent<NetworkIdentity>().AssignClientAuthority(playerConnection);
        }
        foreach (Transform child in RivalPieces)
        {
            var piece = child.GetComponentInChildren<ChessPiece>();
            piece.MyPlayerID = Player2.netId;
            var player2Connection = Player2.GetComponent<NetworkIdentity>().connectionToClient;
            piece.GetComponent<NetworkIdentity>().AssignClientAuthority(player2Connection);
        }
    }

}
