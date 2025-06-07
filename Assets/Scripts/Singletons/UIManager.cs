using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance {get; private set;}

    [SerializeField] GameObject endTurnButton;
    [SerializeField] TextMeshProUGUI turnText;

    void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    public void EndPlayerTurn() {
        endTurnButton.SetActive(false);
        turnText.text = "Enemy Turn";
        
        BattleManager.Instance.PlayerEndsTurn();
    }

    public void StartPlayerTurn() {
        endTurnButton.SetActive(true);
        turnText.text = "Player Turn";
    }
}
