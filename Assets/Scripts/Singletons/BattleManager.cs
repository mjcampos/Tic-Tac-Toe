using System;
using UnityEngine;

namespace Singletons {
    public class BattleManager : MonoBehaviour {
        public static BattleManager Instance {get; private set;}
        
        [SerializeField] TurnState currentPhase = TurnState.PlayerTurn;
        
        CellState[,] _grid = new CellState[3, 3];
        Vector2Int? _lastPlayerMove;

        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        void Start() {
            ResetGrid();
        }

        void ResetGrid() {
            for (int row = 0; row < 3; row++) {
                for (int col = 0; col < 3; col++) {
                    _grid[row, col] = CellState.None;
                }
            }
        }

        public void PlayerEndsTurn() {
            LockCell();
            _lastPlayerMove = null;
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

        void SetCellState(int row, int col, CellState state) {
            _grid[row, col] = state;
        }

        public CellState GetCellState(int row, int column) {
            return _grid[row, column];
        }

        CellState GetNextState(CellState current) {
            switch (current) {
                case CellState.None:
                    return CellState.X;
                case CellState.X:
                    return CellState.O;
                case CellState.O:
                    return CellState.None;
                default:
                    return CellState.None;
            }
        }

        public void OnPlayerClickCell(int row, int col) {
            CellState current = GetCellState(row, col);
            CellState next = GetNextState(current);
            
            // Clear up the previously selected cell
            if (_lastPlayerMove  != null) {
                SetCellState(_lastPlayerMove.Value.x, _lastPlayerMove.Value.y, CellState.None);
                
                // Message the cell to convert its UI to none
                GridManager.Instance.ClearACell(_lastPlayerMove.Value.x, _lastPlayerMove.Value.y);
            }
            
            // Enable End Turn button is next value is not blank
            UIManager.Instance.InteractWithEndButton(next != CellState.None);
            
            SetCellState(row, col, next);
            _lastPlayerMove = new Vector2Int(row, col);
        }

        // Lock cell to prevent future interactions
        void LockCell() {
            if (_lastPlayerMove != null) {
                GridManager.Instance.LockACell(_lastPlayerMove.Value.x, _lastPlayerMove.Value.y);
            }
        }
    }
}
