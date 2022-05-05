using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private PlayerControl[] Players;

    public PlayerControl MyPlayer { get; set; }

    public float PauseBetweenTurns = 2.5f;
    
    private int Index = 0;

    public delegate void OnNextTurnDelegate(PlayerControl player);
    public OnNextTurnDelegate OnFinishPlayerTurn;
    public OnNextTurnDelegate OnStartNextTurn;

    private void Start()
    {
        Board.Instance.OnCreatedBoard += Initialize;
    }

    public void Initialize()
    {
        Players = FindObjectsOfType<PlayerControl>();
        
        foreach (var player in Players)
        {
            player.IsTurnReady = false;
            player.OnTurnEnded += FinishPlayerTurn;
            if (player.isLocalPlayer)
            {
                MyPlayer = player;
                Debug.Log("My player is:" + MyPlayer);
            }
        }
        Index = -1;
        FinishPlayerTurn();
    }

    private void FinishPlayerTurn()
    {
        Index = (Index + 1) % Players.Length;
        OnFinishPlayerTurn?.Invoke(Players[Index]);
       
        StartCoroutine(StartNextTurnCoroutine());

        IEnumerator StartNextTurnCoroutine()
        {
            yield return new WaitForSeconds(PauseBetweenTurns);
            StartNextTurn();
        }
    }

    private void StartNextTurn() {
        Players[Index].IsTurnReady = true;
        Debug.Log("Next turn:" + Players[Index].name);

        OnStartNextTurn?.Invoke(Players[Index]);
    }
}
