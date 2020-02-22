using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<GamePiece> gamePieces;
    private GameCore gameCore = new GameCore();
    AIv1 aiController = new AIv1();
    public Button[] buttonList;
    public GamePiece selectedPiece;
    public Button recentMove;
    private int playerTurn;

    void Awake()
    {
        SetGameControllerReferenceOnGamePieces();
        playerTurn = 1;
    }

    void EasyAIGame()
    {
        // Player's turn
        if (playerTurn == 1)
        {
            EnableUserInput();
            //Make everything interactable
        }
        // AI's turn
        else
        {
            DisableUserInput();
            string aiPieceChosen = aiController.chooseGamePiece(gameCore.availablePieces);
            ConvertAIPiece(aiPieceChosen);
            string aiBoardSpaceChosen = aiController.choosePosition(gameCore.availableBoardSpaces);
            Button boardSpace = ConvertBoardSpace(aiBoardSpaceChosen);
            StartCoroutine("DelayAIMove", boardSpace);
        }
    }

    void SetGameControllerReferenceOnGamePieces()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            gamePieces[i].GetComponent<GamePiece>().SetGameControllerReference(this);
        }
    }

    // Coroutine that waits a certain amount of time before the ai sets a piece
    IEnumerator DelayAIMove(Button boardSpace)
    {
        yield return new WaitForSeconds(3);
        SetPiece(boardSpace);
    }

    public void DisableUserInput()
    {
        foreach (Button button in buttonList)
            button.interactable = false;
    }

    public void EnableUserInput()
    {
        foreach (GameCore.BoardSpace availableButton in gameCore.availableBoardSpaces)
            foreach (Button button in buttonList)
                if (availableButton.id == button.name.Substring(12))
                {
                    button.interactable = true;
                    break;
                }
    }

    public void SetPiece(Button button)
    {
        if (selectedPiece != null)
        {
            Vector3 newPosition = button.transform.position;
            selectedPiece.transform.position = newPosition;
            recentMove = button;
            button.interactable = false;

            // if this is true, game is over
            if (gameCore.SetPiece(selectedPiece.name, button.name)) 
                GameOver();
            else
                EndTurn();
        }
    }

    public void SetSelectedPiece(GamePiece gamePiece)
    {
        selectedPiece = gamePiece;
    }

    public List<GameCore.Piece> GetAvailablePieces()
    {
        return gameCore.availablePieces;
    }

    public void ConvertAIPiece(string aiPieceChosen)
    {
        string gamePieceString = "GamePiece " + aiPieceChosen;
        SetSelectedPiece(GameObject.Find(gamePieceString).GetComponent<GamePiece>());
    }

    public Button ConvertBoardSpace(string aiBoardSpaceChosen)
    {
        string boardSpaceString = "Board Space " + aiBoardSpaceChosen;
        return GameObject.Find(boardSpaceString).GetComponent<Button>();
    }

    public void EndTurn()
    {
        selectedPiece = null;
        ChangeSides();
        EasyAIGame();
    }

    void GameOver()
    {
        SetBoardInteractable(false);
    }

    void ChangeSides()
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
