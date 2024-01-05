using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Core;
public class ChessPieceSelectionHandler : MonoBehaviour
{
    Ray ray;
    public Camera cameraa;
    public ChessPlayerPlacementHandler chessPlayerPlacementHandler;
    private const int chessBoardSize=8;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            ChessBoardPlacementHandler.Instance.ClearHighlights();
            SelectPiece();
        }
    }

    private void SelectPiece(){
        RaycastHit hit;
        ray=cameraa.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit)){
            Transform objectHit=hit.transform;
            HighlightPossibleMoves(hit.transform.gameObject);
            Debug.Log("hit");
        }
        else{
            Debug.Log("Not hit");
        }
    }

    private void HighlightPossibleMoves(GameObject SelectedPiece){
        chessPlayerPlacementHandler=SelectedPiece.gameObject.GetComponent<ChessPlayerPlacementHandler>();
        switch(SelectedPiece.gameObject.tag){
            case "Black Pawn":
                HighlightBlackPawnMoves();
                break;
            case "Black Rook":
                HighlightRookMoves();
                break;
            case "Black Bishop":
                HighlightBishopMoves();
                break;
            case "Black Knight":
                HighlightKnightMoves();
                break;
            case "Black Queen":
                HighlightRookMoves();
                HighlightBishopMoves();
                break;
            case "Black King":
                HighlightKingMoves();
                break;
        }

    }
    void HighlightBlackPawnMoves(){
        int currentRow=chessPlayerPlacementHandler.row;
        int currentColumn=chessPlayerPlacementHandler.column;
        if(currentRow==1 && ChessBoardPlacementHandler.Instance._occupiedTiles[2,currentColumn]!="B"){
            ChessBoardPlacementHandler.Instance.Highlight(2,currentColumn);
            if(ChessBoardPlacementHandler.Instance._occupiedTiles[3,currentColumn]!="B"){
                ChessBoardPlacementHandler.Instance.Highlight(3,currentColumn);
            }
        }
        else{
            if(ChessBoardPlacementHandler.Instance._occupiedTiles[currentRow+1,currentColumn]!="B"){
                ChessBoardPlacementHandler.Instance.Highlight(currentRow+1,currentColumn);
            }
        }
    }

    void HighlightRookMoves(){
        HighlightDirectionalMoves(0,1);
        HighlightDirectionalMoves(0,-1);
        HighlightDirectionalMoves(1,0);
        HighlightDirectionalMoves(-1,0);
    }

    void HighlightBishopMoves(){
        HighlightDirectionalMoves(1,1);
        HighlightDirectionalMoves(-1,1);
        HighlightDirectionalMoves(-1,-1);
        HighlightDirectionalMoves(1,-1);
    }

    void HighlightKingMoves(){
            int currentRow=chessPlayerPlacementHandler.row;
            int currentColumn=chessPlayerPlacementHandler.column;
            int[,] kingMoves={{-1,-1},{-1,0},{-1,1},{0,-1},{0,1},{1,-1},{1,0},{1,1}};
            for(int i=0;i<kingMoves.GetLength(0);i++){
                int newRow=currentRow+kingMoves[i,0];
                int newColumn=currentColumn+kingMoves[i,1];
                if(ChessBoardPlacementHandler.Instance._occupiedTiles[newRow,newColumn]!="B"){
                    ChessBoardPlacementHandler.Instance.Highlight(newRow,newColumn);
                }
            }
    }

    void HighlightKnightMoves(){
        int currentRow=chessPlayerPlacementHandler.row;
            int currentColumn=chessPlayerPlacementHandler.column;
            int[,] knightMoves={{2,1},{2,-1},{1,2},{1,-2},{-2,1},{-2,-1},{-1,2},{-1,-2}};
            for(int i=0;i<knightMoves.GetLength(0);i++){
                int newRow=currentRow+knightMoves[i,0];
                int newColumn=currentColumn+knightMoves[i,1];
                if(newRow>=0 && newRow<8 && newColumn>=0 && newColumn<8 && ChessBoardPlacementHandler.Instance._occupiedTiles[newRow,newColumn]!="B"){
                    ChessBoardPlacementHandler.Instance.Highlight(newRow,newColumn);
                }
            }
    }

    void HighlightDirectionalMoves(int rowIncrement,int columnIncrement){
        int currentRow=chessPlayerPlacementHandler.row;
        int currentColumn=chessPlayerPlacementHandler.column;
        for(int i=currentRow+rowIncrement,j=currentColumn+columnIncrement; (i>=0 && i<chessBoardSize && j>=0 && j< chessBoardSize); i+=rowIncrement,j+=columnIncrement){
            if(!TryHighlightPosition(i,j)) break;
        }
    }

    bool TryHighlightPosition(int row,int column){
        if(ChessBoardPlacementHandler.Instance._occupiedTiles[row,column]!="B"){
            ChessBoardPlacementHandler.Instance.Highlight(row,column);
            return true;
        }
        return false;
    }

}

