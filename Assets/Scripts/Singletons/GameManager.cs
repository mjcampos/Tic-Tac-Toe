using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Singletons {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance {get; private set;}
        
        [SerializeField] InputActionAsset inputActionAsset;
        
        InputActionMap _uiInputActionMap;
        InputAction _restartInputAction;

        bool _canRestart = false;
        
        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        void Start() {
            _uiInputActionMap = inputActionAsset.FindActionMap("UI");

            _restartInputAction = _uiInputActionMap.FindAction("Restart");
        }

        void Update() {
            if (_restartInputAction.triggered && _canRestart) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        /*
         * When the game ends, either by a winner or draw, perform the following sequence:
         * 1. Lock all cells to prevent updates
         * 2. Give player the chance to restart
         * 3. Display winner
         */
        public void GameOverSequence(WinState winner) {
            // Step 1
            GridManager.Instance.LockAllCells();
            
            // Step 2
            _canRestart = true;

            // Step 3
            switch (winner) {
                case WinState.Player:
                    UIManager.Instance.DisplayWinner("Player Wins!");
                    break;
                case WinState.Enemy:
                    UIManager.Instance.DisplayWinner("Enemy Wins!");
                    break;
                case WinState.Draw:
                    UIManager.Instance.DisplayWinner("Draw!");
                    break;
                default:
                    UIManager.Instance.DisplayWinner("");
                    break;
            }
        }
    }
}
