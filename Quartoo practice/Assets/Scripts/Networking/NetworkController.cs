using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
    private GameController gameController;
    public static NetworkController NetController;
    //public static NetworkPlayer netPlayer;

    [SerializeField] private PhotonView photonView;
    private static char networkMessage;
    private static bool networkMessageReceived = false;

    // Move variable
    public static string movePiece;
    public static string moveLocation;


    void Awake()
    {
        NetController = this;
    }

    private void Start()
    {        
        Debug.Log("Instantiated PV");

        GameObject player = PhotonNetwork.Instantiate("NetworkPlayer", new Vector3(0, 0, 0), Quaternion.identity, 0);
    }

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    // Called to distinguish network game
    //    if (photonView != null)
    //    {
    //        // if my pv is not sending a message
    //        if (!photonView.IsMine)
    //            return;
    //        // check once per frame if a message has been sent
    //        else
    //            photonView.RPC("RPC_receiveNetworkMessage", RpcTarget.All, networkMessage);
    //    }
    //}

    /*
     * [PunRPC]
     * void functionName(data youWantToPass)
     * {
     *      code to be executed on the receiving end of the rpc
     * }
     * 
     * How to send RPC:
     * photonView.RPC("functionName", RpcTarget.whoYouWantToSendThisTo, valueToBeTransferred);
     */


    // if it is not our turn, we're waiting for a message
    public IEnumerator WaitForTurn()
    {
        Debug.Log("Network waiting for turn");
        Debug.Log(networkMessageReceived);
        while (networkMessageReceived == false)
        {
            yield return null;
        }

        gameController.NetworkMessageReceived();
        networkMessageReceived = false;
    }


    public void SendMove()
    {
        Debug.Log("MoveLocation = " + moveLocation);
        NetworkPlayer.networkPlayer.SendMove(moveLocation, movePiece);
        Debug.Log("MoveLocation = " + moveLocation);
    }

    public void SendPiece()
    {
        Debug.Log("sending piece " + movePiece);
        NetworkPlayer.networkPlayer.SendPiece(movePiece);
    }

    public void SetMovePiece(string id)
    {
        movePiece = id;
        Debug.Log("network movePiece = " + movePiece);
    }

    public string GetMovePiece()
    {
        return movePiece;
    }

    public void SetMoveLocation(string name)
    {
        moveLocation = name;
    }
    public string GetMoveLocation()
    {
        return moveLocation;
    }

    public void SetNetworkMesage(char message)
    {
        networkMessage = message;
    }

    public char GetNetworkMessage()
    {
        return networkMessage;
    }

    public void SetNetworkMessageRecieved(bool boolean)
    {
        networkMessageReceived = boolean;
    }

    public bool GetNetworkMessageRecieved()
    {
        return networkMessageReceived;
    }
}
