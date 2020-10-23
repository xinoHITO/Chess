using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnUI : MonoBehaviour
{
    public GameObject PlayerLabel;
    private TMP_Text LabelText;
    private Animator LabelAnimator;
    private TurnManager TurnManager;

    // Start is called before the first frame update
    void Start()
    {
        TurnManager = GetComponent<TurnManager>();
        TurnManager.OnFinishPlayerTurn += OnEndPlayerTurn;
        TurnManager.OnStartNextTurn += OnStartNextTurn;
        LabelAnimator = PlayerLabel.GetComponentInChildren<Animator>();
        LabelText = PlayerLabel.GetComponentInChildren<TMP_Text>();
    }

    private void OnEndPlayerTurn(PlayerControl player)
    {
        LabelAnimator.Play("Base Layer.Show");
        LabelText.text = string.Format("{0}'s turn",player.gameObject.name);
    }

    private void OnStartNextTurn(PlayerControl player)
    {
        LabelAnimator.Play("Base Layer.Hide");
        
    }
}
