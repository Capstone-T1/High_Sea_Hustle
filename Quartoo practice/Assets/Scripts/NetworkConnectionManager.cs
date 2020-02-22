using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// setting a namespace prevents errors with other parts of the program
namespace NetSpace
{
    public class NetworkConnectionManager : MonoBehaviourPunCallbacks
    {

        public Button BtnConnectMaster;
        public Button BtnConnectRoom;

        // bools to help identify what state we are in
        public bool TriesToConnectToMaster;
        public bool TriesToConnectToRoom;

        void Start()
        {
            TriesToConnectToMaster = false;
            TriesToConnectToRoom = false;
        }

        
        void Update()
        {
            // Active when we are not connected and we do not try to connect to master, so the user can click
            BtnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster);
            // Active when we are connected and we do not try to connect to Master or Room 
            BtnConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !TriesToConnectToMaster && !TriesToConnectToRoom);
        }

        public void OnClickConnectToMaster()
        {
            // Settings
            PhotonNetwork.OfflineMode = false;      // true would fake online connection
            PhotonNetwork.NickName = "Player";
            PhotonNetwork.AutomaticallySyncScene = true;        // to call PhotonNetwork.LoadLevel()
            PhotonNetwork.GameVersion = "v1";                           // only players with same version can play together

            TriesToConnectToMaster = true;
            //PhotonNetwork.ConnectToMaster(ip, port, appid)    // manual connection
            PhotonNetwork.ConnectUsingSettings();                   // based on config file in Photon/PhotonUnityNetworking/Resources/PhotonServerSettings
        }
        
        // Should be called if kicked out of game, master, server or internet
        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            TriesToConnectToMaster = false;
            TriesToConnectToRoom = false;
            Debug.Log(cause);
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            TriesToConnectToMaster = false;
            Debug.Log("Connected to Master");
        }

        public void OnClickConnectToRoom()
        {
            if (!PhotonNetwork.IsConnected)
                return;

            TriesToConnectToRoom = true;
            //PhotonNetwork.CreateRoom("Custom room name 1");       // Create a specific room        Error: OnCreateRoomFailed
            //PhotonNetwork.JoinRoom("Custom room name 1");           // Join a specific room            Error: OnJoinRoomFailed
            PhotonNetwork.JoinRandomRoom();                                     // Join a random room             Error: OnJoinRandomRoomFailed
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            TriesToConnectToRoom = false;
            Debug.Log("Master: " + PhotonNetwork.IsMasterClient +  " | Players in room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name);
            SceneManager.LoadScene("SampleScene");
        }

        // Should be called when first player tries to join a room
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            // no room available
            // create a room (null = name of room doesn't matter, for random matchmaking)
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
        }

        // Big problems if we get this - maybe too many people for PUN servers?
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            Debug.Log(message);
            TriesToConnectToRoom = false;
        }
    }
}
