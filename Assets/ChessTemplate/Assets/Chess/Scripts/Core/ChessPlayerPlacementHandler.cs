using System;
using UnityEngine;

namespace Chess.Scripts.Core {
    public class ChessPlayerPlacementHandler : MonoBehaviour {
        [SerializeField] public int row, column;

        private void Start() {
            ChessBoardPlacementHandler.Instance._occupiedTiles[row,column]="B";
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        }
    }
}