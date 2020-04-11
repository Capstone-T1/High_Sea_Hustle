using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentAvatarBarController : MonoBehaviourPun
{
    public GameObject opponentAvatarBar;
    public Image avatarImage;
    public Text usernametext;
    private static NetworkController networkController = new NetworkController();

    public Text debugText;

    public Sprite[] avatarImageOptions;

    //THESE NEED TO BE SET DYNAMICALLY
    private string opponentsAvatar;
    private string opponentsUsername;
    void Start()
    {
        if (GameInfo.gameType == 'T') //Tutorial
            opponentAvatarBar.SetActive(false); //There is no opponent
        else
        {
            opponentAvatarBar.SetActive(true);

            if (GameInfo.gameType == 'H') // Quick game hard
            {
                if (GameInfo.avatar == "PirateCaptain")
                {
                    //Make opponent navy captain
                    avatarImage.sprite = avatarImageOptions[2];
                    usernametext.text = "Navy Captain";
                }
                else if (GameInfo.avatar == "NavyCaptain")
                {
                    //Make opponent pirate captain
                    avatarImage.sprite = avatarImageOptions[0];
                    usernametext.text = "Pirate Captain";
                }
                else
                {
                    //Default to navy captain
                    avatarImage.sprite = avatarImageOptions[2];
                    usernametext.text = "Navy Captain";
                }
            }
            else if (GameInfo.gameType == 'E') //Quick game easy
            {
                if (GameInfo.avatar == "PirateSailor")
                {
                    //make opponent navy sailor
                    avatarImage.sprite = avatarImageOptions[3];
                    usernametext.text = "Navy Sailor";
                }
                else if (GameInfo.avatar == "NavySailor")
                {
                    //make opponent pirate sailor
                    avatarImage.sprite = avatarImageOptions[1];
                    usernametext.text = "Pirate Sailor";
                }
                else
                {
                    //default to navy sailor
                    avatarImage.sprite = avatarImageOptions[3];
                    usernametext.text = "Navy Sailor";
                }
            }
            else if (GameInfo.gameType == 'N') //Networked game
            {
                if (gameObject.GetComponent<PhotonView>() == null)
                {
                    PhotonView photonView = gameObject.AddComponent<PhotonView>();
                    photonView.ViewID = 20;
                }
                else
                {
                    photonView.ViewID = 20;
                }

                photonView.RPC("SendUserInfo", RpcTarget.Others, GameInfo.avatar, GameInfo.username);
                usernametext.text = opponentsUsername;
            }
            else if (GameInfo.gameType == 'S') //Story Mode
            {
                if (GameInfo.storyModeType == 'E')
                {
                    opponentsAvatar = "NavySailor";
                    usernametext.text = "Navy Sailor";
                }
                else if (GameInfo.storyModeType == 'H')
                {
                    opponentsAvatar = "NavyCaptain";
                    usernametext.text = "Navy Captain";
                }
                else
                {
                    return;
                }
                ShowAvatar(opponentsAvatar);
            }
            else
            {
                //Give the opponent a default avatar and username 
                opponentsAvatar = "PirateCaptain";
                usernametext.text = "Opponent Player";
                ShowAvatar(opponentsUsername);
                return;
            }

            
        }
    }

    private void ShowAvatar(string avatar)
    {
        if (avatar == "PirateCaptain")
        {
            avatarImage.sprite = avatarImageOptions[0];
        }
        else if (avatar == "PirateSailor")
        {
            avatarImage.sprite = avatarImageOptions[1];
        }
        else if (avatar == "NavyCaptain")
        {
            avatarImage.sprite = avatarImageOptions[2];
        }
        else if (avatar == "NavySailor")
        {
            avatarImage.sprite = avatarImageOptions[3];
        }
        else
        {
            Debug.Log("Player avatar name was invalid");
            return;
        }
    }

    [PunRPC]
    void SendUserInfo(string avatar, string username)
    {
        SetOpponentAvatar(avatar, username);
    }

    private void SetOpponentAvatar(string avatar, string username)
    {
        usernametext.text = username;
        ShowAvatar(avatar);
    }
}
