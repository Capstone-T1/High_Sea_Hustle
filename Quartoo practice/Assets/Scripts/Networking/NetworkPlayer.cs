using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviour
{
    public static NetworkPlayer networkPlayer;
    [SerializeField] private NetworkController networkController; 
    [SerializeField] private PhotonView photonView;
    private static string networkMessage = "";

    public static string movePiece;
    public static string moveLocation;
    public static string onDeckPiece;

    private void Start()
    {
        photonView = PhotonView.Get(this);
        networkPlayer = this;
    }

    // need?
    [PunRPC]
    public void RPC_receiveNetworkMessage(string message)
    {
        if (!photonView.IsMine)
            return;

        Debug.Log("Message sent over network: " + message);
    }


    [PunRPC]
    public void RPC_SendMove(string location, string piece)
    {
        Debug.Log("F: NetworkPlayer.cs/RPC_SendMove was called");
        if (!photonView.IsMine)
            return;

        Debug.Log("Receiving move...");
        networkController.SetMovePiece(piece);
        networkController.SetMoveLocation(location);
        networkController.SetNetworkMesage('M');
        networkController.SetNetworkMessageReceived(true);
    }

    [PunRPC]
    public void RPC_SendPiece(string piece)
    {
        Debug.Log("F: NetworkPlayer.cs/RPC_SendPiece was called");
        if (!photonView.IsMine)
            return;

        Debug.Log("NetworkController = " + networkController);
        Debug.Log("piece = " + piece);
        networkController.SetMovePiece(piece);
        networkController.SetNetworkMesage('P');
        networkController.SetNetworkMessageReceived(true);
    }

    public void SendMove(string moveLocation, string movePiece)
    {
        photonView.RPC("RPC_SendMove", RpcTarget.All, moveLocation, movePiece);
    }

    public void SendPiece(string movePiece)
    {
        photonView.RPC("RPC_SendPiece", RpcTarget.All, movePiece);
    }

 
}
