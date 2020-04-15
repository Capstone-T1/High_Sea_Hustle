﻿using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviourPun
{
    public GameObject helpPanel;
    public GameObject settingsPanel;
    public GameObject gameOverPanel;
    public GameObject darkenBackground;
    public GameObject networkGameOverPanel;
    public GameObject storyModeLosePanel;
    public GameObject storyModeWinPanel;
    public GameObject storyModeWin2Panel;
    public GameObject tutorial;
    public GameObject playerAvatar;
    public GameObject opponentAvatar;
    public GameObject topBar;
    public GameObject errorMessagePopup;
    public UnityEngine.UI.Text errorMessageText;
    public UnityEngine.UI.Text gameOverMessage;
    public UnityEngine.UI.Text networkGameOverMessage;

    void Start()
    {
        showTopBar();
        showOpponentAvatar();
        showPlayerAvatar();
    }
    #region General Panels
    public void showGameOverPanel(char endgameStatus)
    {
        string endgameMessage = getGameOverMessage(endgameStatus);

        gameOverMessage.text = endgameMessage;
        gameOverPanel.SetActive(true);
    }

    public void hideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void showNetworkGameOverPanel(char endgameStatus)
    {

        string endgameMessage = getGameOverMessage(endgameStatus);

        networkGameOverMessage.text = endgameMessage;
        networkGameOverPanel.SetActive(true);
    }

    public void hideNetworkGameOverPanel()
    {
        networkGameOverPanel.SetActive(false);
    }

    public void showStoryModeWinPanel()
    {
        Debug.Log("Inside showStoryModeWinPanel()");
        storyModeWinPanel.SetActive(true);
        storyModeWinPanel.GetComponentInChildren<UnityEngine.UI.Text>().text = "Congratulations!";
    }

    public void hideStoryModeWinPanel()
    {
        storyModeWinPanel.SetActive(false);
    }

    public void showStoryModeWin2Panel()
    {
        Debug.Log("Inside showStoryModeWin2Panel()");
        storyModeWin2Panel.SetActive(true);
        storyModeWin2Panel.GetComponentInChildren<UnityEngine.UI.Text>().text = "Congratulations!";
    }

    public void hideStoryModeWin2Panel()
    {
        storyModeWin2Panel.SetActive(false);
    }

    public void showStoryModeLosePanel()
    {
        Debug.Log("Inside showStoryModeLosePanel()");
        storyModeLosePanel.SetActive(true);
        storyModeLosePanel.GetComponentInChildren<UnityEngine.UI.Text>().text = "Bummer!";
    }

    public void hideStoryModeLosePanel()
    {
        storyModeLosePanel.SetActive(false);
    }

    public void showPlayerLeft()
    {
        errorMessageText.text = "Your oppenent has forfeited the game.";
        errorMessagePopup.SetActive(true);
    }

    public void showForfeitGame()
    {

    }
    #endregion

    #region Settings/Help Panels
    public void showSettingsPanel()
    {
        darkenBackground.SetActive(true);
        settingsPanel.SetActive(true);
    }

    public void hideSettingsPanel()
    {
        settingsPanel.SetActive(false);
        darkenBackground.SetActive(false);
    }

    public void showHelpPanel()
    {
        darkenBackground.SetActive(true);
        helpPanel.SetActive(true);
        Debug.Log("make panel visible");
    }

    public void hideHelpPanel()
    {
        helpPanel.SetActive(false);
        darkenBackground.SetActive(false);
    }

    public void closeCurrentPanel()
    {
        if (settingsPanel.activeSelf)
            hideSettingsPanel();
        else
            hideHelpPanel();
    }
    #endregion

    #region Animations
    public void showParrot()
    {
        Animator animator = tutorial.GetComponent<Animator>();
        if(animator != null)
        {
            bool isOpen = animator.GetBool("isActive");

            animator.SetBool("isActive", !isOpen);
        }
    }

    public void showPlayerAvatar()
    {
        Animator animator = playerAvatar.GetComponent<Animator>();
        if (animator != null)
        {
            bool isOpen = animator.GetBool("open");

            animator.SetBool("open", !isOpen);
        }
    }

    public void showOpponentAvatar()
    {
        Animator animator = opponentAvatar.GetComponent<Animator>();
        if (animator != null)
        {
            bool isOpen = animator.GetBool("open");

            animator.SetBool("open", !isOpen);
        }
    }

    public void showTopBar()
    {
        Animator animator = topBar.GetComponent<Animator>();
        if (animator != null)
        {
            bool isOpen = animator.GetBool("open");

            animator.SetBool("open", !isOpen);
        }
    }
    #endregion

    #region SceneChanges
    public void returnToMainMenu()
    {
        if (GameInfo.gameType == 'N')
        {
            PhotonNetwork.AutomaticallySyncScene = false;

            PhotonNetwork.LeaveRoom();

            PhotonNetwork.Disconnect();
        }
        Initiate.Fade("MainMenu", Color.black, 4.0f);
    }

    public void meFirst()
    {
        GameInfo.selectPieceAtStart = 1;
        Initiate.Fade("GameScene", Color.black, 4.0f);
    }

    public void themFirst()
    {
        GameInfo.selectPieceAtStart = 2;
        Initiate.Fade("GameScene", Color.black, 4.0f);
    }

    public void playAgain()
    {
        Initiate.Fade("GameScene", Color.black, 4.0f);
    }

    public void continueStoryMode()
    {
        if (GameInfo.storyModeType == 'E')
        {
            GameInfo.storyModeType = 'H';
        }

        Initiate.Fade("StoryMode2", Color.black, 4.0f);
    }

    private string getGameOverMessage(char endgameStatus)
    {
        string message;

        if (endgameStatus == 'L')
        {
            message = "YOU LOST";
        }
        else
        {
            message = endgameStatus == 'W' ? "YOU WON!" : "YOU TIED";
        }

        return message;
    }
    #endregion
}
