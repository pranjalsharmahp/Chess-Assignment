using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Core;
public class ChessPieceSelectionHandler : MonoBehaviour
{
    Ray ray;
    public Camera cameraa;
    public ChessPlayerPlacementHandler chessPlayerPlacementHandler;
    private const int chessBoardSize=8;
    public string playerTile="White";
    public string enemyTile="Black";
    public int playerTurn=1;
    public bool isBlackTurn;
    internal static ChessPieceSelectionHandler Instance;
    public GameObject temp;
    public GameObject _highlightPrefab;
    public PlayerManager playerManager;
    public int fromRow;
    public int fromColumn;

    void Start(){
        isBlackTurn=false;
        Instance=this;
        playerManager=GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            SelectPiece();
        }
    }

    private void SelectPiece(){
        RaycastHit hit;
        ray=cameraa.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit)){
            Transform objectHit=hit.transform;
            Debug.Log(hit.transform.name+" "+playerTurn+" "+hit.transform.tag);
            if(objectHit.tag!="Highlighter"){
                ChessBoardPlacementHandler.Instance.ClearHighlights();
                if(isBlackTurn && objectHit.tag=="Black" &&playerTile=="Black"){
                    temp=hit.transform.gameObject;
                    HighlightPossibleMoves(hit.transform.gameObject);
                }
                if(!isBlackTurn && objectHit.tag=="White" && playerTile=="White"){
                    temp=hit.transform.gameObject;
                    HighlightPossibleMoves(hit.transform.gameObject);
                }
            }
            if(objectHit.tag=="Highlighter"){
                ChessBoardPlacementHandler.Instance.ClearHighlights();
                int highlightRow=Int32.Parse(new string(objectHit.transform.parent.gameObject.transform.parent.gameObject.name[5],1)) - 1;
                int highlightColumn=Int32.Parse(objectHit.transform.parent.gameObject.name);
                fromRow=chessPlayerPlacementHandler.row;
                fromColumn=chessPlayerPlacementHandler.column;
                // chessPlayerPlacementHandler.ChangePosition(highlightRow,highlightColumn);
                
                playerManager.SendMove(fromRow,fromColumn,highlightRow,highlightColumn);
                playerTurn++;
                Debug.Log("Highlighter Hit "+playerTurn);
            }
            if(objectHit.tag=="Castling Highlighter"){
                ChessBoardPlacementHandler.Instance.ClearHighlights();
                int highlightRow=Int32.Parse(new string(objectHit.transform.parent.gameObject.transform.parent.gameObject.name[5],1)) - 1;
                int highlightColumn=Int32.Parse(objectHit.transform.parent.gameObject.name);
                chessPlayerPlacementHandler.Castle(highlightRow,highlightColumn);
                playerTurn++;
            }
        }
        else{
            Debug.Log("Not hit");
        }
    }

    private void HighlightPossibleMoves(GameObject SelectedPiece){
        chessPlayerPlacementHandler=SelectedPiece.gameObject.GetComponent<ChessPlayerPlacementHandler>();
        
        switch(chessPlayerPlacementHandler.pieceName){
            case "Black Pawn":
                HighlightBlackPawnMoves();
                break;
            case "White Pawn":
                HighlightWhitePawnMoves();
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
        if(!isOccupiedByPlayer(currentRow-1,currentColumn) && !isOccupiedByEnemy(currentRow-1,currentColumn)){
                MovesRestricter(currentRow-1,currentColumn);
        }
        //Highlight if there is a enemy on the left
        if(isValidMove(currentRow-1,currentColumn-1) && isOccupiedByEnemy(currentRow-1,currentColumn-1)){
                MovesRestricter(currentRow-1,currentColumn-1);
        }
        //Highlight if there is a enemy on the right
        if(isValidMove(currentRow-1,currentColumn+1) && isOccupiedByEnemy(currentRow-1,currentColumn+1)){
                MovesRestricter(currentRow-1,currentColumn+1);
        }
        //Highlight the 2nd row too if the pawn is on the default position
        if(currentRow==6 && !isOccupiedByEnemy(currentRow-1,currentColumn) && !isOccupiedByPlayer(currentRow-1,currentColumn) && !isOccupiedByPlayer(currentRow-2,currentColumn) && !isOccupiedByEnemy(currentRow-2,currentColumn)){
                MovesRestricter(currentRow-2,currentColumn);
        }
        
    }
    void HighlightWhitePawnMoves(){
        int currentRow=chessPlayerPlacementHandler.row;
        int currentColumn=chessPlayerPlacementHandler.column;
        //Front Highlight
        if(!isOccupiedByPlayer(currentRow+1,currentColumn) && !isOccupiedByEnemy(currentRow+1,currentColumn)){
            MovesRestricter(currentRow+1,currentColumn);
        }
        //Highlight if there is a enemy on the left
        if(isValidMove(currentRow+1,currentColumn-1) && isOccupiedByEnemy(currentRow+1,currentColumn-1)){
                MovesRestricter(currentRow+1,currentColumn-1);
        }
        //Highlight if there is a enemy on the right
        if(isValidMove(currentRow+1,currentColumn+1) && isOccupiedByEnemy(currentRow+1,currentColumn+1)){
                MovesRestricter(currentRow+1,currentColumn  +1);
        }
        //Highlight the 2nd row too if the pawn is on the default position
        if(currentRow==1 && !isOccupiedByEnemy(currentRow+1,currentColumn) && !isOccupiedByPlayer(currentRow+1,currentColumn) && !isOccupiedByPlayer(currentRow+2,currentColumn) && !isOccupiedByEnemy(currentRow+2,currentColumn)){
                MovesRestricter(currentRow+2,currentColumn);
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
            if(isValidMove(newRow,newColumn) && !isOccupiedByPlayer(newRow,newColumn)){
                MovesRestricter(newRow,newColumn);                
            }
            if(!chessPlayerPlacementHandler.hasMoved){
                if(isBlackTurn){
                    if(!isOccupied(7,1) && !isOccupied(7,2) && !isOccupied(7,3)&&!ChessBoardPlacementHandler.Instance._chessPiecePosition[7,0].GetComponent<ChessPlayerPlacementHandler>().hasMoved){
                        if(SimulateMove(7,2)){
                            CastlingHighlight(7,2);
                        }
                        
                    }
                    if(!isOccupied(7,5) && !isOccupied(7,6) && !ChessBoardPlacementHandler.Instance._chessPiecePosition[7,7].GetComponent<ChessPlayerPlacementHandler>().hasMoved){
                        if(SimulateMove(7,6)){
                            CastlingHighlight(7,6);
                        }
                    }
                }
                else{
                   if(!isOccupied(0,1) && !isOccupied(0,2) && !isOccupied(0,3)&& !ChessBoardPlacementHandler.Instance._chessPiecePosition[0,0].GetComponent<ChessPlayerPlacementHandler>().hasMoved){
                        if(SimulateMove(0,2)){
                            CastlingHighlight(0,2);
                        }
                    }
                    if(!isOccupied(0,5) && !isOccupied(0,6)&&!ChessBoardPlacementHandler.Instance._chessPiecePosition[7,7].GetComponent<ChessPlayerPlacementHandler>().hasMoved){
                        if(SimulateMove(0,6)){
                            CastlingHighlight(0,6);
                        }
                    } 
                } 
            }
        }
    }

    void CastlingHighlight(int row,int col){
        var tile = ChessBoardPlacementHandler.Instance.GetTile(row, col).transform;
        if (tile == null) {
            Debug.LogError("Invalid row or column.");
            return;
        }

        Instantiate(_highlightPrefab, tile.transform.position, Quaternion.identity, tile.transform);
    }

    void HighlightKnightMoves(){
        int currentRow=chessPlayerPlacementHandler.row;
        int currentColumn=chessPlayerPlacementHandler.column;
        int[,] knightMoves={{2,1},{2,-1},{1,2},{1,-2},{-2,1},{-2,-1},{-1,2},{-1,-2}}; // All the possible moves a knight can make
            for(int i=0;i<knightMoves.GetLength(0);i++){
                int newRow=currentRow+knightMoves[i,0];
                int newColumn=currentColumn+knightMoves[i,1];
                if(isValidMove(newRow,newColumn) && !isOccupiedByPlayer(newRow,newColumn)){
                    MovesRestricter(newRow,newColumn);
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
            if(ChessBoardPlacementHandler.Instance._chessPiecePosition[row,column]!=null && ChessBoardPlacementHandler.Instance._chessPiecePosition[row,column].tag==enemyTile){
                MovesRestricter(row,column);
                return false;
            }
            MovesRestricter(row,column);
            return true;
        }
        return false;
    }

    public bool isOccupiedByPlayer(int row,int column){
        if(ChessBoardPlacementHandler.Instance._chessPiecePosition[row,column]!=null && ChessBoardPlacementHandler.Instance._chessPiecePosition[row,column].tag==playerTile){
            return true;
        }
        return false;
    }

    bool isOccupiedByEnemy(int row,int column){
        if(ChessBoardPlacementHandler.Instance._chessPiecePosition[row,column]!=null && ChessBoardPlacementHandler.Instance._chessPiecePosition[row,column].tag==enemyTile ){
            return true;
        }
        return false;
    }
    bool isOccupied(int row,int column){
        if(ChessBoardPlacementHandler.Instance._chessPiecePosition[row,column]!=null){
            return true;
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
    void MovesRestricter(int row, int column)
    {
        int tempRow = chessPlayerPlacementHandler.row;
        int tempColumn = chessPlayerPlacementHandler.column;

        // Store the original state
        GameObject originalPiece = ChessBoardPlacementHandler.Instance._chessPiecePosition[tempRow, tempColumn];
        GameObject movedPiece = ChessBoardPlacementHandler.Instance._chessPiecePosition[row, column];

        // Simulate the move
        ChessBoardPlacementHandler.Instance._chessPiecePosition[tempRow, tempColumn] = null;
        ChessBoardPlacementHandler.Instance._chessPiecePosition[row, column] = temp;

        // Check if the move is valid after the simulated move
        if (!CheckHandler.checkHandlerInstance.IsKingInCheck())
        {
            Highlight(row, column);
        }

        // Undo the simulation
        ChessBoardPlacementHandler.Instance._chessPiecePosition[tempRow, tempColumn] = originalPiece;
        ChessBoardPlacementHandler.Instance._chessPiecePosition[row, column] = movedPiece;  
    }
    //this function is to simulate the move and if the move is valid then the function will return true
    bool SimulateMove(int row,int column){
        int tempRow = chessPlayerPlacementHandler.row;
        int tempColumn = chessPlayerPlacementHandler.column;

        // Store the original state
        GameObject originalPiece = ChessBoardPlacementHandler.Instance._chessPiecePosition[tempRow, tempColumn];
        GameObject movedPiece = ChessBoardPlacementHandler.Instance._chessPiecePosition[row, column];

        // Simulate the move
        ChessBoardPlacementHandler.Instance._chessPiecePosition[tempRow, tempColumn] = null;
        ChessBoardPlacementHandler.Instance._chessPiecePosition[row, column] = temp;

        // Check if the move is valid after the simulated move
        if (!CheckHandler.checkHandlerInstance.IsKingInCheck())
        {
            ChessBoardPlacementHandler.Instance._chessPiecePosition[tempRow, tempColumn] = originalPiece;
            ChessBoardPlacementHandler.Instance._chessPiecePosition[row, column] = movedPiece;
            return true;
        }

        // Undo the simulation
        ChessBoardPlacementHandler.Instance._chessPiecePosition[tempRow, tempColumn] = originalPiece;
        ChessBoardPlacementHandler.Instance._chessPiecePosition[row, column] = movedPiece;  
        return false;
    }

    

}

