using System.Collections;
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
        SceneManager.LoadScene("SampleScene");
    }

    public void multiplayerGame()
    {
        SceneManager.LoadScene("GameLobby");
    }

    public void settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void storyModeGame()
    {
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
