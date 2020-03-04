﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject settingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showHelpPanel()
    {
        helpPanel.SetActive(true);
        Debug.Log("make panel visible");
    }

    public void hideHelpPanel()
    {
        //Panel helpPanel = GameObject.Find("helpPanel").GetComponent<Panel>();
        helpPanel.SetActive(false);
    }

    public void showSettingsPanel()
    {
        settingsPanel.SetActive(true);
        Debug.Log("make panel visible");
    }

    public void hideSettingsPanel()
    {
        //Panel helpPanel = GameObject.Find("helpPanel").GetComponent<Panel>();
        settingsPanel.SetActive(false);
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void meFirst()
    {
        GameInfo.selectPieceAtStart = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void themFirst()
    {
        GameInfo.selectPieceAtStart = 2;
        SceneManager.LoadScene("GameScene");
    }
}
