using System;
using UnityEngine;

namespace Chess.Scripts.Core {
    public class ChessPlayerPlacementHandler : MonoBehaviour {
        [SerializeField] public int row, column;
        public string pieceName;
        private void Start() {
            pieceName=gameObject.name;
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
            ChessBoardPlacementHandler.Instance._chessPiecePosition[row,column]=gameObject;
        }

        public void ChangePosition(int newRow,int newColumn){
            if(ChessPieceSelectionHandler.Instance.playerTurn%2==0){
                ChessPieceSelectionHandler.Instance.isBlackTurn=false;
                ChessPieceSelectionHandler.Instance.playerTile="White";
                ChessPieceSelectionHandler.Instance.enemyTile="Black";
            }
            else{
                ChessPieceSelectionHandler.Instance.isBlackTurn=true;;
                ChessPieceSelectionHandler.Instance.playerTile="Black";
                ChessPieceSelectionHandler.Instance.enemyTile="White";
            }
            ChessBoardPlacementHandler.Instance._chessPiecePosition[row,column]=null;
            row=newRow;
            column=newColumn;
            if(ChessBoardPlacementHandler.Instance._chessPiecePosition[newRow,newColumn]!=null){
                Destroy(ChessBoardPlacementHandler.Instance._chessPiecePosition[newRow,newColumn]);
            }
            // if(ChessBoardPlacementHandler.Instance._chessBoard[row,column]!=null){
            //     Destroy(ChessBoardPlacementHandler.Instance._chessBoard[row,column]);
            // }
            // ChessBoardPlacementHandler.Instance._chessBoard[row,column]=gameObject;
            ChessBoardPlacementHandler.Instance._chessPiecePosition[row,column]=gameObject;
            transform.position=ChessBoardPlacementHandler.Instance.GetTile(row,column).transform.position;
            CheckHandler.checkHandlerInstance.iskingincheck=CheckHandler.checkHandlerInstance.IsKingInCheck();
        }
    }
}