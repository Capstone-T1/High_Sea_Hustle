using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashControl : MonoBehaviour
{
    public Image gameLogo;
    public Image teamLogo;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(myStart());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Initiate.Fade("MainMenu", Color.black, 4.0f);
        }
    }

    IEnumerator myStart()
    {
        gameLogo.canvasRenderer.SetAlpha(0);
        teamLogo.canvasRenderer.SetAlpha(0);
        gameLogoFadeIn();
        yield return new WaitForSeconds(5);
        gameLogoFadeOut();
        yield return new WaitForSeconds(5);
        teamLogoFadeIn();
        yield return new WaitForSeconds(5);
        teamLogoFadeOut();
        yield return new WaitForSeconds(3);
        Initiate.Fade("MainMenu", Color.black, 4.0f);
    }

    void gameLogoFadeIn()
    {
        gameLogo.CrossFadeAlpha(1,3, false);
    }

    void gameLogoFadeOut()
    {
        gameLogo.CrossFadeAlpha(0,3, false);
    }

    void teamLogoFadeIn()
    {
        teamLogo.CrossFadeAlpha(1,3, false);
    }

    void teamLogoFadeOut()
    {
        teamLogo.CrossFadeAlpha(0,3, false);
    }
}
