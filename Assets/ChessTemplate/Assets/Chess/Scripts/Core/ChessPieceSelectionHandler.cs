using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Core;
public class ChessPieceSelectionHandler : MonoBehaviour
{
    Ray ray;
    public Camera cameraa;
    public ChessPlayerPlacementHandler chessPlayerPlacementHandler;

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
                if(chessPlayerPlacementHandler.row==1 && ChessBoardPlacementHandler.Instance._occupiedTiles[2,chessPlayerPlacementHandler.column]!="B"){
                    ChessBoardPlacementHandler.Instance.Highlight(2,chessPlayerPlacementHandler.column);
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[3,chessPlayerPlacementHandler.column]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(3,chessPlayerPlacementHandler.column);
                    }
                    
                }
                else{
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[chessPlayerPlacementHandler.row+1,chessPlayerPlacementHandler.column]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(chessPlayerPlacementHandler.row+1,chessPlayerPlacementHandler.column);
                    }
                }
                Debug.Log("Black Pawn Selected");
                break;
            
            case "Black Rook":
                for(int i=chessPlayerPlacementHandler.row+1;i<8;i++){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[i,chessPlayerPlacementHandler.column]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(i,chessPlayerPlacementHandler.column);
                    }
                    else{
                        break;
                    }
                }
                for(int i=chessPlayerPlacementHandler.row-1;i>=0;i--){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[i,chessPlayerPlacementHandler.column]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(i,chessPlayerPlacementHandler.column);
                    }
                    else{
                        break;
                    }
                }
                for(int j=chessPlayerPlacementHandler.column+1;j<8;j++){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[chessPlayerPlacementHandler.row,j]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(chessPlayerPlacementHandler.row,j);
                    }
                    else{
                        break;
                    }
                }
                for(int j=chessPlayerPlacementHandler.column-1;j>=0;j--){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[chessPlayerPlacementHandler.row,j]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(chessPlayerPlacementHandler.row,j);
                    }
                    else{
                        break;
                    }
                }
                break;

            case "Black Bishop":
                for(int i=chessPlayerPlacementHandler.row+1,j=chessPlayerPlacementHandler.column+1;i<8&&j<8;i++,j++){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[i,j]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(i,j);
                    }
                    else{
                        break;
                    }
                }
                for(int i=chessPlayerPlacementHandler.row-1,j=chessPlayerPlacementHandler.column-1;i>=0&&j>=0;i--,j--){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[i,j]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(i,j);
                    }
                    else{
                        break;
                    }
                }
                for(int i=chessPlayerPlacementHandler.row-1,j=chessPlayerPlacementHandler.column+1;i>=0&&j<8;i--,j++){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[i,j]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(i,j);
                    }
                    else{
                        break;
                    }
                }
                for(int i=chessPlayerPlacementHandler.row+1,j=chessPlayerPlacementHandler.column-1;i<8&&j>=0;i++,j--){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[i,j]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(i,j);
                    }
                    else{
                        break;
                    }
                }
                break;
            case "Black Knight":
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
                break;
            case "Black Queen":
                for(int i=chessPlayerPlacementHandler.row+1;i<8;i++){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[i,chessPlayerPlacementHandler.column]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(i,chessPlayerPlacementHandler.column);
                    }
                    else{
                        break;
                    }
                }
                for(int i=chessPlayerPlacementHandler.row-1;i>=0;i--){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[i,chessPlayerPlacementHandler.column]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(i,chessPlayerPlacementHandler.column);
                    }
                    else{
                        break;
                    }
                }
                for(int j=chessPlayerPlacementHandler.column+1;j<8;j++){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[chessPlayerPlacementHandler.row,j]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(chessPlayerPlacementHandler.row,j);
                    }
                    else{
                        break;
                    }
                }
                for(int j=chessPlayerPlacementHandler.column-1;j>=0;j--){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[chessPlayerPlacementHandler.row,j]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(chessPlayerPlacementHandler.row,j);
                    }
                    else{
                        break;
                    }
                }
                for(int i=chessPlayerPlacementHandler.row+1,j=chessPlayerPlacementHandler.column+1;i<8&&j<8;i++,j++){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[i,j]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(i,j);
                    }
                    else{
                        break;
                    }
                }
                for(int i=chessPlayerPlacementHandler.row-1,j=chessPlayerPlacementHandler.column-1;i>=0&&j>=0;i--,j--){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[i,j]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(i,j);
                    }
                    else{
                        break;
                    }
                }
                for(int i=chessPlayerPlacementHandler.row-1,j=chessPlayerPlacementHandler.column+1;i>=0&&j<8;i--,j++){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[i,j]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(i,j);
                    }
                    else{
                        break;
                    }
                }
                for(int i=chessPlayerPlacementHandler.row+1,j=chessPlayerPlacementHandler.column-1;i<8&&j>=0;i++,j--){
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[i,j]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(i,j);
                    }
                    else{
                        break;
                    }
                }
                break;
            case "Black King":
                currentRow=chessPlayerPlacementHandler.row;
                currentColumn=chessPlayerPlacementHandler.column;
                int[,] kingMoves={{-1,-1},{-1,0},{-1,1},{0,-1},{0,1},{1,-1},{1,0},{1,1}};
                for(int i=0;i<kingMoves.GetLength(0);i++){
                    int newRow=currentRow+kingMoves[i,0];
                    int newColumn=currentColumn+kingMoves[i,1];
                    if(ChessBoardPlacementHandler.Instance._occupiedTiles[newRow,newColumn]!="B"){
                        ChessBoardPlacementHandler.Instance.Highlight(newRow,newColumn);
                    }
                }
                break;
            }

                
        }
}

