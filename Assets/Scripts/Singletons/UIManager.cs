using System;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Singletons {
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

        void Start() {
            endTurnButton.GetComponent<Button>().interactable = false;
        }

        public void EndPlayerTurn() {
            endTurnButton.SetActive(false);
            endTurnButton.GetComponent<Button>().interactable = false;
            turnText.text = "Enemy Turn";
            
            BattleManager.Instance.PlayerEndsTurn();
        }

        public void StartPlayerTurn() {
            endTurnButton.SetActive(true);
            turnText.text = "Player Turn";
        }

        public void InteractWithEndButton(bool val) {
            endTurnButton.GetComponent<Button>().interactable = val;
        }
    }
}
