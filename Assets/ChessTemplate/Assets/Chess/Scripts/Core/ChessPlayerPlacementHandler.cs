using System;
using UnityEngine;

namespace Chess.Scripts.Core {
    public class ChessPlayerPlacementHandler : MonoBehaviour {
        [SerializeField] public int row, column;

        private void Start() {
            if(gameObject.tag!="White"){
                ChessBoardPlacementHandler.Instance._occupiedTiles[row,column]="B";
            }
            else{
                ChessBoardPlacementHandler.Instance._occupiedTiles[row,column]="W";
            }
            
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        }
    }
}