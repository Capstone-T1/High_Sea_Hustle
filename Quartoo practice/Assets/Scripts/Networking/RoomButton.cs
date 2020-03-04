using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    public Text nameText;
    public string roomName;

    public void SetRoom()
    {
        nameText.text = roomName;
    }

    public void JoinRoomOnClick()
    {
        Debug.Log("F: RoomButton.cs/public void JoinRoomOnClick - Room we want to join was clicked");
        PhotonNetwork.JoinRoom(roomName);
        Debug.Log("F: RoomButton.cs/public void JoinRoomOnClick - Succesfully joined room: " + roomName);
    }
}
