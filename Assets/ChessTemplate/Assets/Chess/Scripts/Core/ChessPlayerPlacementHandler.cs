using System;
using UnityEngine;

namespace Chess.Scripts.Core {
    public class ChessPlayerPlacementHandler : MonoBehaviour {
        [SerializeField] public int row, column;
        public bool hasMoved;
        public string pieceName;
        private PlayerManager playerManager;
        private void Start() {
            playerManager=GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
            pieceName=gameObject.name;
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
            ChessBoardPlacementHandler.Instance._chessPiecePosition[row,column]=gameObject;
            hasMoved=false;
            // Invoke("ChangePieceRotation",6.0f);
            
        }

        public void ChangePosition(int newRow,int newColumn){
            hasMoved=true;
            UpdateTurnColor();
            ChessBoardPlacementHandler.Instance._chessPiecePosition[row,column]=null;
            row=newRow;
            column=newColumn;
            if(ChessBoardPlacementHandler.Instance._chessPiecePosition[newRow,newColumn]!=null){
                Destroy(ChessBoardPlacementHandler.Instance._chessPiecePosition[newRow,newColumn]);
            }
            ChessBoardPlacementHandler.Instance._chessPiecePosition[row,column]=gameObject;
            transform.position=ChessBoardPlacementHandler.Instance.GetTile(row,column).transform.position;
            CheckHandler.checkHandlerInstance.iskingincheck=CheckHandler.checkHandlerInstance.IsKingInCheck();
        }

        public void Castle(int newRow,int newColumn){
            hasMoved=true;
            UpdateTurnColor();
            if(newRow==0){
                if(newColumn==2){
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[0,2]=null;
                    row=newRow;
                    column=newColumn;
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[0,2]=gameObject;
                    transform.position=ChessBoardPlacementHandler.Instance.GetTile(row,column).transform.position;
                    ChessPlayerPlacementHandler rookPlacementHandler=ChessBoardPlacementHandler.Instance._chessPiecePosition[0,0].GetComponent<ChessPlayerPlacementHandler>();
                    GameObject temp=ChessBoardPlacementHandler.Instance._chessPiecePosition[0,0];
                    rookPlacementHandler.row=0;
                    rookPlacementHandler.column=3;
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[0,0]=null;
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[0,3]=temp;
                    temp.transform.position=ChessBoardPlacementHandler.Instance.GetTile(0,3).transform.position;
                    CheckHandler.checkHandlerInstance.iskingincheck=CheckHandler.checkHandlerInstance.IsKingInCheck();

                }
                if(newColumn==6){
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[0,5]=null;
                    row=newRow;
                    column=newColumn;
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[0,6]=gameObject;
                    transform.position=ChessBoardPlacementHandler.Instance.GetTile(row,column).transform.position;
                    ChessPlayerPlacementHandler rookPlacementHandler=ChessBoardPlacementHandler.Instance._chessPiecePosition[0,7].GetComponent<ChessPlayerPlacementHandler>();
                    GameObject temp=ChessBoardPlacementHandler.Instance._chessPiecePosition[0,7];
                    rookPlacementHandler.row=0;
                    rookPlacementHandler.column=5;
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[0,7]=null;
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[0,5]=temp;
                    temp.transform.position=ChessBoardPlacementHandler.Instance.GetTile(0,5).transform.position;
                    CheckHandler.checkHandlerInstance.iskingincheck=CheckHandler.checkHandlerInstance.IsKingInCheck();
                }
            }
            if(newRow==7){
                if(newColumn==2){
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[7,2]=null;
                    row=newRow;
                    column=newColumn;
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[7,2]=gameObject;
                    transform.position=ChessBoardPlacementHandler.Instance.GetTile(row,column).transform.position;
                    ChessPlayerPlacementHandler rookPlacementHandler=ChessBoardPlacementHandler.Instance._chessPiecePosition[7,0].GetComponent<ChessPlayerPlacementHandler>();
                    GameObject temp=ChessBoardPlacementHandler.Instance._chessPiecePosition[7,0];
                    rookPlacementHandler.row=7;
                    rookPlacementHandler.column=3;
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[7,0]=null;
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[7,3]=temp;
                    temp.transform.position=ChessBoardPlacementHandler.Instance.GetTile(7,3).transform.position;
                    CheckHandler.checkHandlerInstance.iskingincheck=CheckHandler.checkHandlerInstance.IsKingInCheck();

                }
                if(newColumn==6){
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[7,5]=null;
                    row=newRow;
                    column=newColumn;
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[7,6]=gameObject;
                    transform.position=ChessBoardPlacementHandler.Instance.GetTile(row,column).transform.position;
                    ChessPlayerPlacementHandler rookPlacementHandler=ChessBoardPlacementHandler.Instance._chessPiecePosition[7,7].GetComponent<ChessPlayerPlacementHandler>();
                    GameObject temp=ChessBoardPlacementHandler.Instance._chessPiecePosition[7,7];
                    rookPlacementHandler.row=0;
                    rookPlacementHandler.column=5;
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[7,7]=null;
                    ChessBoardPlacementHandler.Instance._chessPiecePosition[7,5]=temp;
                    temp.transform.position=ChessBoardPlacementHandler.Instance.GetTile(7,5).transform.position;
                    CheckHandler.checkHandlerInstance.iskingincheck=CheckHandler.checkHandlerInstance.IsKingInCheck();
                }
            }
        }
        void UpdateTurnColor(){
            ChessPieceSelectionHandler.Instance.playerTurn++;
            if(ChessPieceSelectionHandler.Instance.playerTurn%2==0){
                ChessPieceSelectionHandler.Instance.isBlackTurn=true;
                // ChessPieceSelectionHandler.Instance.playerTile="White";
                // ChessPieceSelectionHandler.Instance.enemyTile="Black";
            }
            else{
                ChessPieceSelectionHandler.Instance.isBlackTurn=false;;
                // ChessPieceSelectionHandler.Instance.playerTile="Black";
                // ChessPieceSelectionHandler.Instance.enemyTile="White";
            }
        }
        // public void ChangePieceRotation(){
        //     if(playerManager.playerId==2){
        //         gameObject.transform.rotation=Quaternion.Euler(0,0,180);
        //     }
        // }
    }
}