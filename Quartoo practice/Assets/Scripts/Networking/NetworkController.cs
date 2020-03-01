using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
    public static NetworkController NetController;

    private PhotonView photonView;
    private static string networkMessage;
    private static bool networkMessageReceived = false;

    // Move variable



    private void Awake()
    {
        NetController = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (photonView != null)
        {
            if (!photonView.IsMine)
                return;
        }
    }

    public IEnumerator WaitForTurn()
    {
        Debug.Log("Network waiting for turn");
        while (networkMessageReceived == false)
        {
            yield return null;
        }
        networkMessageReceived = false;
    }

    [PunRPC]
    void SendMove(string location, string piece)
    {
        if (photonView.IsMine)
            return;

        Debug.Log("Receiving move...");
        // nextmove.nextpiece = nextpiece
        // nextmove.nextlocation = nextlocation
        networkMessageReceived = true;
    }
}
