using UnityEngine;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviour
{
    #region Variables

    [SerializeField] private NetworkController networkController; 
    [SerializeField] private PhotonView photonView;

    public static NetworkPlayer networkPlayer;
    public static string movePiece;
    public static string moveLocation;
    public static string onDeckPiece;

    #endregion

    private void Start()
    {
        photonView = PhotonView.Get(this);
        networkPlayer = this;
    }

    #region PunRPC's

    // need?
    [PunRPC]
    public void RPC_receiveNetworkMessage(string message)
    {
        Debug.Log("NetworkPlayer.cs/RPC_receivedNetworkMessage(string message)");

        if (!photonView.IsMine)
            return;

        Debug.Log("Message sent over network: " + message);
    }


    [PunRPC]
    public void RPC_SendMove(string location, string piece)
    {
        Debug.Log("NetworkPlayer.cs/RPC_SendMove(string location, string piece)");
        if (!photonView.IsMine)
            return;

        Debug.Log("Receiving move...");
        networkController.SetMovePiece(piece);
        networkController.SetMoveLocation(location);
        networkController.SetNetworkMessage('M');
        networkController.SetNetworkMessageReceived(true);
    }

    [PunRPC]
    public void RPC_SendPiece(string piece)
    {
        Debug.Log("NetworkPlayer.cs/RPC_SendPiece(string piece)");
        if (!photonView.IsMine)
            return;

        Debug.Log("NetworkController = " + networkController);
        Debug.Log("piece = " + piece);
        networkController.SetMovePiece(piece);
        networkController.SetNetworkMessage('P');
        networkController.SetNetworkMessageReceived(true);
    }

    #endregion

    #region Functions

    public void SendMove(string moveLocation, string movePiece)
    {
        Debug.Log("NetworkPlayer.cs/SendMove(string moveLocation, string movePiece)");
        photonView.RPC("RPC_SendMove", RpcTarget.All, moveLocation, movePiece);
    }

    public void SendPiece(string movePiece)
    {
        Debug.Log("NetworkPlayer.cs/SendPiece(string movePiece)");
        photonView.RPC("RPC_SendPiece", RpcTarget.All, movePiece);
    }

    #endregion
}
