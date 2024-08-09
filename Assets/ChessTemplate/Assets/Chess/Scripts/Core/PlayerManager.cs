using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Chess.Scripts.Core;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public int playerId;
    // private PhotonView photonView;
    public PlayerManager Instance;
    private GameObject mainCamera;
    private int[] temp={0,1,6,7};
    
    void Start()
    {
        Instance=this;
        PhotonNetwork.ConnectUsingSettings();
        mainCamera=GameObject.Find("Main Camera");
        // photonView=gameObject.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room1", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom(){
        playerId=PhotonNetwork.LocalPlayer.ActorNumber;
        ChessPieceSelectionHandler.Instance.enemyTile=(playerId==1?"Black":"White");
        ChessPieceSelectionHandler.Instance.playerTile=(playerId==1?"White":"Black");
        if(playerId==2){
            mainCamera.transform.rotation=Quaternion.Euler(0,0,180);
            RotatePieces();
        }

    }
    public void SendMove(int fromRow,int fromColumn,int toRow,int toColumn){
        photonView.RPC("OnMove", RpcTarget.All,fromRow,fromColumn,toRow, toColumn);
    }
    public void SendCastle(int fromRow,int fromColumn,int toRow,int toColumn){
        photonView.RPC("OnCastle",RpcTarget.All,fromRow,fromColumn,toRow,toColumn);
    }
    [PunRPC]
    public void OnMove(int fromRow,int fromColumn,int toRow,int toColumn){
        
        ChessPlayerPlacementHandler chessPlayerPlacementHandler=GetPlacementHandler(fromRow,fromColumn);
        chessPlayerPlacementHandler.ChangePosition(toRow,toColumn);
    }
    [PunRPC]
    public void OnCastle(int fromRow,int fromColumn,int toRow,int toColumn){
        ChessPlayerPlacementHandler chessPlayerPlacementHandler=GetPlacementHandler(fromRow,fromColumn);
        chessPlayerPlacementHandler.Castle(toRow,toColumn);
    }

    ChessPlayerPlacementHandler GetPlacementHandler(int row,int column){
        return ChessBoardPlacementHandler.Instance._chessPiecePosition[row,column].GetComponent<ChessPlayerPlacementHandler>();
    }

    private void RotatePieces(){
        foreach (int i in temp){
            for(int j=0;j<8;j++){
                ChessBoardPlacementHandler.Instance._chessPiecePosition[i,j].transform.rotation=Quaternion.Euler(0,0,180);
            }
        }
    }
}
