using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<GamePiece> gamePieces;
    private GameCore gameCore = new GameCore();
    public Button[] buttonList;
    public GamePiece selectedPiece;
    public Button recentMove;
    private int playerTurn;
    private int moveCount;

    void SetGameControllerReferenceOnGamePieces()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            gamePieces[i].GetComponent<GamePiece>().SetGameControllerReference(this);
        }
    }

    void Awake()
    {
        SetGameControllerReferenceOnGamePieces();
        playerTurn = 1;
        moveCount = 0;
    }

    public void SetPiece(Button button)
    {
        if (selectedPiece != null)
        {
            Vector3 newPosition = button.transform.position;
            selectedPiece.transform.position = newPosition;
            recentMove = button;
            button.interactable = false;
            changeSides();
            Debug.Log(playerTurn);
            // if this is true, game is over
            if (gameCore.SetPiece(selectedPiece.name, button.name))
            {
                GameOver();
            }
            selectedPiece = null;
        }
    }

    public void SetSelectedPiece(GamePiece gamePiece)
    {
        selectedPiece = gamePiece;
    }

    public void EndTurn()
    {
        changeSides();
    }

    void GameOver()
    {
        SetBoardInteractable(false);
    }

    void changeSides()
    {
        playerTurn = (playerTurn == 1) ? 2 : 1;
    }


    public void RestartGame()
    {
        playerTurn = 1;
        SetBoardInteractable(true);
    }

    public void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }
}
