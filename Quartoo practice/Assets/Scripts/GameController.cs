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
    private int moveCount;

    void Awake()
    {
        SetGameControllerReferenceOnGamePieces();
        playerTurn = 1;
        moveCount = 0;
    }

    void SetGameControllerReferenceOnGamePieces()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            gamePieces[i].GetComponent<GamePiece>().SetGameControllerReference(this);
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
            changeSides();
            // if this is true, game is over
            if (gameCore.SetPiece(selectedPiece.name, button.name))
            {
                GameOver();
            }
            selectedPiece = null;

            //AI stuff currently fills the entire board after a human move
            //string aiPieceChosen = aiController.chooseGamePiece(gameCore.availablePieces);
            //ConvertAIPiece(aiPieceChosen);
            //string aiBoardSpaceChosen = aiController.choosePosition(gameCore.availableBoardSpaces);
            //Button boardSpace = ConvertBoardSpace(aiBoardSpaceChosen);
            //SetPiece(boardSpace);
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
