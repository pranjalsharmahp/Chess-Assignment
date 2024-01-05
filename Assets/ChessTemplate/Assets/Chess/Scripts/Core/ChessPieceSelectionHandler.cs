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
    private const string playerTile="B";
    private const string enemyTile="W";

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
            case "Rook":
                HighlightRookMoves();
                break;
            case "Bishop":
                HighlightBishopMoves();
                break;
            case "Knight":
                HighlightKnightMoves();
                break;
            case "Queen":
                HighlightRookMoves();
                HighlightBishopMoves();
                break;
            case "King":
                HighlightKingMoves();
                break;
        }

    }
    void HighlightBlackPawnMoves(){
        int currentRow=chessPlayerPlacementHandler.row;
        int currentColumn=chessPlayerPlacementHandler.column;
        //Front Highlight
        if(!isOccupiedByPlayer(currentRow+1,currentColumn) && !isOccupiedByEnemy(currentRow+1,currentColumn)){
            Highlight(currentRow+1,currentColumn);
        }
        //Highlight if there is a enemy on the left
        if(isOccupiedByEnemy(currentRow+1,currentColumn-1)){
            Highlight(currentRow+1,currentColumn-1);
        }
        //Highlight if there is a enemy on the right
        if(isOccupiedByEnemy(currentRow+1,currentColumn+1)){
            Highlight(currentRow+1,currentColumn+1);
        }
        //Highlight the 2nd row too if the pawn is on the default position
        if(currentRow==1 && !isOccupiedByEnemy(currentRow+1,currentColumn) && !isOccupiedByPlayer(currentRow+1,currentColumn)){
            Highlight(currentRow+2,currentColumn);
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
            int[,] kingMoves={{-1,-1},{-1,0},{-1,1},{0,-1},{0,1},{1,-1},{1,0},{1,1}}; //All the possible moves a king can make
            for(int i=0;i<kingMoves.GetLength(0);i++){
                int newRow=currentRow+kingMoves[i,0];
                int newColumn=currentColumn+kingMoves[i,1];
                if(!isOccupiedByPlayer(newRow,newColumn)){
                    if(isOccupiedByEnemy(newRow,newColumn)){
                        Highlight(newRow,newColumn);
                        break;
                    }
                    else{
                        Highlight(newRow,newColumn);
                    }
                    
                }
            }
    }

    void HighlightKnightMoves(){
        int currentRow=chessPlayerPlacementHandler.row;
            int currentColumn=chessPlayerPlacementHandler.column;
            int[,] knightMoves={{2,1},{2,-1},{1,2},{1,-2},{-2,1},{-2,-1},{-1,2},{-1,-2}}; // All the possible moves a knight can make
            for(int i=0;i<knightMoves.GetLength(0);i++){
                int newRow=currentRow+knightMoves[i,0];
                int newColumn=currentColumn+knightMoves[i,1];
                if(isValidMove(newRow,newColumn) && !isOccupiedByPlayer(newRow,newColumn)){
                    if(!isOccupiedByPlayer(newRow,newColumn)){
                        Highlight(newRow,newColumn);
                    }
                    Highlight(newRow,newColumn);
                }
            }
    }

    void HighlightDirectionalMoves(int rowIncrement,int columnIncrement){
        int currentRow=chessPlayerPlacementHandler.row;
        int currentColumn=chessPlayerPlacementHandler.column;
        for(int i=currentRow+rowIncrement,j=currentColumn+columnIncrement; (isValidMove(i,j)); i+=rowIncrement,j+=columnIncrement){
            if(!TryHighlightPosition(i,j)) break;
        }
    }

    bool TryHighlightPosition(int row,int column){
        if(!isOccupiedByPlayer(row,column)){
            //Check if we can take the enemy pieces and highlight them
            if(ChessBoardPlacementHandler.Instance._occupiedTiles[row,column]==enemyTile){
                Highlight(row,column);
                return false;
            }
            Highlight(row,column);
            return true;
        }
        return false;
    }

    bool isOccupiedByPlayer(int row,int column){
        if(ChessBoardPlacementHandler.Instance._occupiedTiles[row,column]==playerTile){
            return true;
        }
        return false;
    }

    bool isOccupiedByEnemy(int row,int column){
        if(isValidMove(row,column+1) && isValidMove(row,column-1)){
            if(ChessBoardPlacementHandler.Instance._occupiedTiles[row,column]==enemyTile){
                return true;
            }
        }
        return false;
    }

    bool isValidMove(int row,int column){
        if(row>=0 && row<8 && column>=0 && column<8){
            return true;
        }
        return false;
    }

    void Highlight(int row, int column){
        if(isValidMove(row,column)){
            ChessBoardPlacementHandler.Instance.Highlight(row,column);
        }
    }

}

