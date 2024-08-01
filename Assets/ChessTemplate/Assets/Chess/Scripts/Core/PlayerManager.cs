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
    void Start()
    {
        Instance=this;
        PhotonNetwork.ConnectUsingSettings();
        // mainCamera=GameObject.Find("Main Camera");
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
        ChessPieceSelectionHandler.Instance.playerTile=(playerId==1?"White":"Black");
        // if(playerId==2){
        //     mainCamera.transform.rotation=Quaternion.Euler(0,0,180);
        // }

    }
    public void SendMove(int fromRow,int fromColumn,int toRow,int toColumn){
        photonView.RPC("OnMove", RpcTarget.All,fromRow,fromColumn,toRow, toColumn);
    }
    [PunRPC]
    public void OnMove(int fromRow,int fromColumn,int toRow,int toColumn){
        GameObject SelectedPiece=ChessBoardPlacementHandler.Instance._chessPiecePosition[fromRow,fromColumn];
        ChessPlayerPlacementHandler chessPlayerPlacementHandler=SelectedPiece.GetComponent<ChessPlayerPlacementHandler>();
        chessPlayerPlacementHandler.ChangePosition(toRow,toColumn);
    }
}
