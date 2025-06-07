using System;
using System.Collections.Generic;
using UnityEngine;

namespace Singletons {
    public class GridManager : MonoBehaviour {
        public static GridManager Instance {get; private set;}
        
        Cell[,] _grid = new  Cell[3, 3];
        
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
            int rows = 3;
            int cols = 3;

            for (int row = 0; row < rows; row++) {
                for (int col = 0; col < cols; col++) {
                    _grid[row, col] = childCells[row *  cols + col];
                }
            }
        }

        public void ClearACell(int row, int col) {
            _grid[row, col].ClearCell();
        }
        
        public void LockACell(int row, int col) {
            _grid[row, col].LockCell();
        }
    }
}
