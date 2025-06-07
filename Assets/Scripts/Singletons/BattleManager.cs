using System;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    public static BattleManager Instance {get; private set;}
    
    [SerializeField] TurnState currentPhase = TurnState.PlayerTurn;

    void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    public void PlayerEndsTurn() {
        AdvanceTurn();
    }

    void PlayerStartsTurn() {
        UIManager.Instance.StartPlayerTurn();
    }

    void AdvanceTurn() {
        int next = (int)currentPhase + 1;
        
        // Check if it exceeds the max enum value, loop back to 0
        if (next >= Enum.GetValues(typeof(TurnState)).Length) next = 0;
        
        // Cast back to the enum and assign
        currentPhase = (TurnState)next;

        switch (currentPhase) {
            case TurnState.PlayerTurn:
                Debug.Log("Player Turn");
                PlayerStartsTurn();
                break;
            case TurnState.EnemyTurn:
                Debug.Log("Enemy Turn");
                AdvanceTurn();
                break;
        }
    }
}
