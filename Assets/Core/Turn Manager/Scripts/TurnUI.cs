using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnUI : NetworkBehaviour
{
    public GameObject PlayerLabel;
    private TMP_Text LabelText;
    private Animator LabelAnimator;
    private TurnManager TurnManager;

    // Start is called before the first frame update
    void Start()
    {
        TurnManager = GetComponent<TurnManager>();
        LabelAnimator = PlayerLabel.GetComponentInChildren<Animator>();
        LabelText = PlayerLabel.GetComponentInChildren<TMP_Text>();

        if (isServer)
        {
            TurnManager.OnFinishPlayerTurn += OnEndPlayerTurn;
            TurnManager.OnStartNextTurn += OnStartNextTurn;
        }
    }

    private void OnEndPlayerTurn(PlayerControl player)
    {
        RpcShowTurnLabel(player.name);
    }

    [ClientRpc]
    private void RpcShowTurnLabel(string playerName)
    {
        LabelAnimator.Play("Base Layer.Show");
        LabelText.text = string.Format("{0}'s turn", playerName);
    }

    private void OnStartNextTurn(PlayerControl player)
    {
        RpcHideTurnLabel();
    }

    [ClientRpc]
    private void RpcHideTurnLabel()
    {
        LabelAnimator.Play("Base Layer.Hide");
    }
}
