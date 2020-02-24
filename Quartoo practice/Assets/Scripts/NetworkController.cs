using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class NetworkController : MonoBehaviourPunCallbacks
{

    public PhotonView photonView;
    public GameController gameController = null;
    private static bool isNetworkGame = false;
    public Button move;

    private void Start()
    {
        print("Connecting to server...");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to server...");
        print(PhotonNetwork.LocalPlayer.NickName);

        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason " + cause.ToString());
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            GameInfo.selectPieceAtStart = 1;
            Debug.Log("Player on this screen going first...");
        }
        else
        {
            GameInfo.selectPieceAtStart = 2;
            Debug.Log("Player on this screen going second...");
        }
    }

    public void networkGame()
    {
        photonView = PhotonView.Get(this);
        GameInfo.gameType = 'N';
        isNetworkGame = true;
    }

    public void onMoveToSend(Button moveToSend)
    {
        photonView.RPC("sendMove", RpcTarget.Others, PhotonNetwork.LocalPlayer, moveToSend);
    }


    [PunRPC]
    public void sendMove(Button sentMove)
    {
        Button newMove = sentMove;
        gameController.receiveMoveFromNetwork(newMove);
        Debug.Log(sentMove);
    }
}
