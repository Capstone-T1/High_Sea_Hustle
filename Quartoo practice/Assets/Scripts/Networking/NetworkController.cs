using Photon.Pun;
using System.Collections;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
    #region Variables

    public static NetworkController NetController;
    public static string movePiece;
    public static string moveLocation;

    [SerializeField] private PhotonView photonView;
    private GameController gameController;
    private static char networkMessage;
    private static bool networkMessageReceived = false;

    #endregion

    #region Start Update Awake

    void Awake()
    {
        NetController = this;
    }

    private void Start()
    {        
        Debug.Log("Instantiated PV");

        GameObject player = PhotonNetwork.Instantiate("NetworkPlayer", new Vector3(0, 0, 0), Quaternion.identity, 0);
    }

    #endregion

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }


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
        Debug.Log("NetworkController.cs/WaitForTurn()");

        while (networkMessageReceived == false)
            yield return null;
        
        Debug.Log("Got out of loop");
        networkMessageReceived = false;
        gameController.NetworkMessageReceived();
    }

    #region Public Functions

    public void SendMove()
    {
        Debug.Log("NetworkController.cs/SendMove()");
        Debug.Log("moveLocation: " + moveLocation);
        NetworkPlayer.networkPlayer.SendMove(moveLocation, movePiece);
    }

    public void SendPiece()
    {
        Debug.Log("NetworkController.cs/SendPiece()");
        Debug.Log("Sending piece: " + movePiece);
        NetworkPlayer.networkPlayer.SendPiece(movePiece);
    }

    public void SetMovePiece(string id)
    {
        Debug.Log("NetworkController.cs/SetMovePiece(string id)");
        movePiece = id;
        Debug.Log("Network movePiece: " + movePiece);
    }

    public string GetMovePiece()
    {
        Debug.Log("NetworkController.cs/GetMovePiece()");
        Debug.Log("Returning movePiece: " + movePiece);
        return movePiece;
    }

    public void SetMoveLocation(string name)
    {
        Debug.Log("NetworkController.cs/SetMoveLocation(string name)");
        Debug.Log("Setting move location: " + name);
        moveLocation = name;
    }
    public string GetMoveLocation()
    {
        Debug.Log("NetworkController.cs/GetMoveLocation()");
        Debug.Log("Returning moveLocation: " + moveLocation);
        return moveLocation;
    }

    public void SetNetworkMessage(char message)
    {
        Debug.Log("NetworkController.cs/SetNetworkMesage");
        Debug.Log("Setting networkMessage to: " + message);
        networkMessage = message;
    }

    public char GetNetworkMessage()
    {
        Debug.Log("NetworkController.cs/GetNetworkMessage()");
        Debug.Log("Returning networkMessage: " + networkMessage);
        return networkMessage;
    }

    public void SetNetworkMessageReceived(bool boolean)
    {
        Debug.Log("NetworkController.cs/SetNetworkMessageReceived(bool boolean)");
        networkMessageReceived = boolean;
        Debug.Log("SetNetworkMessageReceived = " + networkMessageReceived);
    }

    public bool GetNetworkMessageRecieved()
    {
        Debug.Log("NetworkController.cs/GetNetworkMessageReceived");
        Debug.Log("Returning networkMessageReceived: " + networkMessageReceived);
        return networkMessageReceived;
    }

    #endregion
}
