using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Singletons {
    public class UIManager : MonoBehaviour {
        public static UIManager Instance {get; private set;}

        [SerializeField] GameObject endTurnButton;
        [SerializeField] TextMeshProUGUI winnerText;
        [SerializeField] TextMeshProUGUI replayText;

        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        void Start() {
            endTurnButton.GetComponent<Button>().interactable = false;
            replayText.gameObject.SetActive(false);
        }

        public void EndPlayerTurn() {
            endTurnButton.SetActive(false);
            endTurnButton.GetComponent<Button>().interactable = false;
            
            BattleManager.Instance.EndTurn();
        }

        public void StartPlayerTurn() {
            endTurnButton.SetActive(true);
        }

        public void InteractWithEndButton(bool val) {
            endTurnButton.GetComponent<Button>().interactable = val;
        }

        public void DisplayWinner(string winnerMessage) {
            winnerText.text = winnerMessage;replayText.gameObject.SetActive(true);
        }
    }
}
