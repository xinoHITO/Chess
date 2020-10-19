using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public PlayerControl[] Players;
    
    private int Index = 0;

    // Start is called before the first frame update
    void Start()
    {
        Players = FindObjectsOfType<PlayerControl>();
        
        foreach (var player in Players)
        {
            player.IsTurnReady = false;
            player.OnTurnEnded += NextTurn;
        }
        Players[0].IsTurnReady = true;
    }

    private void NextTurn()
    {
        Debug.Log("Next turn");
        Index = (Index + 1) % Players.Length;
        Players[Index].IsTurnReady = true;
    }
}
