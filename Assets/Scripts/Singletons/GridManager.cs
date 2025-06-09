using UnityEngine;

namespace Singletons {
    public class GridManager : MonoBehaviour {
        public static GridManager Instance {get; private set;}
        
        Cell[,] _grid = new  Cell[3, 3];
        int _rows = 3;
        int _cols = 3;
        
        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
                
            Instance = this;
        }

        void Start() {
            GetCells();
        }

        void GetCells() {
            Cell[] childCells = GetComponentsInChildren<Cell>();

            for (int row = 0; row < _rows; row++) {
                for (int col = 0; col < _cols; col++) {
                    _grid[row, col] = childCells[row *  _cols + col];
                }
            }
        }

        public void ClearACell(int row, int col) {
            _grid[row, col].ClearCell();
        }
        
        public void LockACell(int row, int col) {
            _grid[row, col].LockCell();
        }
        
        public void LockAllCells() {
            for (int row = 0; row < _rows; row++) {
                for (int col = 0; col < _cols; col++) {
                    _grid[row, col].LockCell();
                }
            }
        }
    }
}
