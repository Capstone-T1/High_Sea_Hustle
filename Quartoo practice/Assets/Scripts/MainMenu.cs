﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject helpPanel;
    void Start()
    {
        
    }

    public void quickGame()
    {
        // NOTE: Sets every quickplay to an easy game. Change this to be either easy or hard
        // whenever that functionality is set in unity
        GameInfo.gameType = 'E';

        SceneManager.LoadScene("SampleScene");
    }

    public void multiplayerGame()
    {
        GameInfo.gameType = 'N';
        SceneManager.LoadScene("GameLobby");
    }

    public void settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void storyModeGame()
    {
        GameInfo.gameType = 'N';
        SceneManager.LoadScene("StoryMode");
    }

    public void showHelpPanel()
    {
        helpPanel.SetActive(true);
    }

    public void hideHelpPanel()
    {
        helpPanel.SetActive(false);
    }

}
