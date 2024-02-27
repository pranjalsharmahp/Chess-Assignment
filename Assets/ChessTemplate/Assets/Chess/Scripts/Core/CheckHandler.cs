using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHandler : MonoBehaviour
{
    public int kingRow;
    public int kingColumn;
    private int[,] possibleKnightMoves={{2,1},{2,-1},{1,2},{1,-2},{-2,1},{-2,-1},{-1,2},{-1,-2}};
    private int[,] possibleKingMoves={{-1,-1},{-1,0},{-1,1},{0,-1},{0,1},{1,-1},{1,0},{1,1}};
    public ChessPieceSelectionHandler chessPieceSelectionHandler;
    public bool iskingincheck;
    internal static CheckHandler checkHandlerInstance;
    private bool isBlackTurn;
    public int tempRow;
    public int tempColumn;
    // Start is called before the first frame update
    void Start()
    {
        checkHandlerInstance=this;
        chessPieceSelectionHandler=GameObject.Find("SelectionManager").GetComponent<ChessPieceSelectionHandler>();
    }

    // Update is called once per frame
    void Update(){
        
    }
    public bool IsKingInCheck(){
        isBlackTurn=ChessPieceSelectionHandler.Instance.isBlackTurn;
        GetKingLocation();
        if(CheckDirectionalMoves(0,1,"Rook","Queen") || CheckDirectionalMoves(0,-1,"Rook","Queen") || CheckDirectionalMoves(1,0,"Rook","Queen") || CheckDirectionalMoves(-1,0,"Rook","Queen")
        || CheckDirectionalMoves(1,1,"Bishop","Queen") || CheckDirectionalMoves(-1,1,"Bishop","Queen") || CheckDirectionalMoves(1,-1,"Bishop","Queen") || CheckDirectionalMoves(-1,-1,"Bishop","Queen")){
            return true;
        }

        if(CheckKnightMoves()){
            return true;
        }
        if(CheckKingMoves()){
            return true;
        }
        if(CheckPawnMoves()){
            return true;
        }
        return false;
    }
    void GetKingLocation(){
        for(int i=0;i<8;i++){
            for(int j=0;j<8;j++){
                if(ChessBoardPlacementHandler.Instance._chessPiecePosition[i,j]!=null && ChessBoardPlacementHandler.Instance._chessPiecePosition[i,j].name=="King" && ChessBoardPlacementHandler.Instance._chessPiecePosition[i,j].tag==chessPieceSelectionHandler.playerTile){
                    kingRow=i;
                    kingColumn=j;
                    return;
                }
            }
        }
    }
    GameObject DirectionalMoves(int rowIncrement,int columnIncrement){
        int currentRow=kingRow;
        int currentColumn=kingColumn;
        for(int i=currentRow+rowIncrement,j=currentColumn+columnIncrement; (isValidMove(i,j)); i+=rowIncrement,j+=columnIncrement){
            if(!chessPieceSelectionHandler.isOccupiedByPlayer(i,j)){
                if(ChessBoardPlacementHandler.Instance._chessPiecePosition[i,j]!=null && ChessBoardPlacementHandler.Instance._chessPiecePosition[i,j].tag==chessPieceSelectionHandler.enemyTile){
                    return ChessBoardPlacementHandler.Instance._chessPiecePosition[i,j];
                }
            }
            else{
                return null;
            }
        }
        return null;
    }

    bool isValidMove(int row,int column){
        if(row>=0 && row<8 && column>=0 && column<8){
            return true;
        }
        return false;
    }

    bool CheckDirectionalMoves(int rowIncrement,int columnIncrement,string piece1,string piece2){
        GameObject temp=DirectionalMoves(rowIncrement,columnIncrement);
        return temp!=null && (temp.name==piece1 || temp.name==piece2);
    }
    bool CheckKnightMoves(){
        for(int i=0;i<possibleKnightMoves.GetLength(0);i++){
            int newRow=kingRow+possibleKnightMoves[i,0];
            int newColumn=kingColumn+possibleKnightMoves[i,1];
            if(isValidMove(newRow,newColumn) && !chessPieceSelectionHandler.isOccupiedByPlayer(newRow,newColumn)){
                if(ChessBoardPlacementHandler.Instance._chessPiecePosition[newRow,newColumn]!=null && ChessBoardPlacementHandler.Instance._chessPiecePosition[newRow,newColumn].tag==chessPieceSelectionHandler.enemyTile){
                    if(ChessBoardPlacementHandler.Instance._chessPiecePosition[newRow,newColumn].name=="Knight"){
                        return true;
                    }
                }
            }
        }
        return false;
    }

    bool CheckKingMoves(){
        for(int i=0;i<possibleKingMoves.GetLength(0);i++){
            int newRow=kingRow+possibleKingMoves[i,0];
            int newColumn=kingColumn+possibleKingMoves[i,1];
            if(isValidMove(newRow,newColumn) && !chessPieceSelectionHandler.isOccupiedByPlayer(newRow,newColumn)){
                if(ChessBoardPlacementHandler.Instance._chessPiecePosition[newRow,newColumn]!=null && ChessBoardPlacementHandler.Instance._chessPiecePosition[newRow,newColumn].tag==chessPieceSelectionHandler.enemyTile){
                    if(ChessBoardPlacementHandler.Instance._chessPiecePosition[newRow,newColumn].name=="King"){
                        return true;
                    }
                }
            }
        }
        
        return false;
    }
    bool CheckPawnMoves(){
        int pawnDirection = isBlackTurn ? -1 : 1;
        for(int i = -1; i <= 1; i += 2){
            Debug.Log("Pawn Direction "+pawnDirection);
            tempRow = kingRow + pawnDirection;
            tempColumn = kingColumn + i;
            if(isValidMove(tempRow, tempColumn) && ChessBoardPlacementHandler.Instance._chessPiecePosition[tempRow, tempColumn] != null && ChessBoardPlacementHandler.Instance._chessPiecePosition[tempRow, tempColumn].name == (isBlackTurn? "White Pawn" : "Black Pawn")){
            // Enemy pawn threatens the king
                return true;
            }
        }
        return false;
    }
    
}
