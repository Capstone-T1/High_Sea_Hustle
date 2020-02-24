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
    private static bool isNetworkGame = false;

    void Awake()
    {
        //WARNING!! THESE ARE SET ONLY FOR TESTING!! DELETE THESE LATER!! ONLY TRISTAN
        //CAN DELETE THEM! DONT DELETE WITHOUT ASKING HIM FIRST PUNKS
        GameInfo.gameType = 'E';
        GameInfo.selectPieceAtStart = 2;

        SetBoardInteractable(false);
        SetGameControllerReferenceOnGamePieces();
        playerTurn = GameInfo.selectPieceAtStart;
    }

    #region Game Type
    void EasyAIGame()
    {
        // Player's turn
        if (playerTurn == 1)
        {

            // Have ai pick piece
            string aiPieceChosen = aiController.chooseGamePiece(gameCore.availablePieces);
            ConvertAIPiece(aiPieceChosen);

            //Make gameboard interactable, gamepieces not interactable
            EnableUserInput();
        }
        // AI's turn
        else
        {
            DisableUserInput();

            //string aiBoardSpaceChosen = aiController.choosePosition(gameCore.availableBoardSpaces);
            //Button boardSpace = ConvertAIBoardSpace(aiBoardSpaceChosen);
            //StartCoroutine("DelayAIMove", boardSpace);
        }
    }

    //WARNING!! THIS IS NOT FINISHED
    void HardAIGame()
    {
        // Player's turn
        if (playerTurn == 1)
        {
            // Have ai pick piece
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
            Button boardSpace = ConvertAIBoardSpace(aiBoardSpaceChosen);
            StartCoroutine("DelayAIMove", boardSpace);
        }
    }

    void StoryMode()
    {
        // do stuff
    }

    void NetworkGame()
    {
        //do more stuff
    }
    #endregion

    #region AI Functions
    private void StartAIGame()
    {
        DisableUserInput();

        // User picks a piece first
        if (GameInfo.selectPieceAtStart == 1)
        {
            Debug.Log("User pick piece");


        }
        // AI picks a piece first
        else
        {

        }
    }

    public void AISetPiece()
    {
        DisableAllPieces();
        string aiBoardSpaceChosen = aiController.choosePosition(gameCore.availableBoardSpaces);
        Button boardSpace = ConvertAIBoardSpace(aiBoardSpaceChosen);
        StartCoroutine("DelayAIMove", boardSpace);
    }

    // Coroutine that waits a certain amount of time before the ai sets a piece
    IEnumerator DelayAIMove(Button boardSpace)
    {
        yield return new WaitForSeconds(3);
        SetPiece(boardSpace);
    }

    public void ConvertAIPiece(string aiPieceChosen)
    {
        string gamePieceString = "GamePiece " + aiPieceChosen;
        SetSelectedPiece(GameObject.Find(gamePieceString).GetComponent<GamePiece>());
    }

    public Button ConvertAIBoardSpace(string aiBoardSpaceChosen)
    {
        string boardSpaceString = "Board Space " + aiBoardSpaceChosen;
        return GameObject.Find(boardSpaceString).GetComponent<Button>();
    }
    #endregion

    #region Standard Functions
    public void SetPiece(Button button)
    {
        if (selectedPiece != null)
        {
            Vector3 newPosition = button.transform.position;
            selectedPiece.transform.position = newPosition;
            recentMove = button;
            button.interactable = false;
            selectedPiece.GetComponent<BoxCollider2D>().enabled = false;
            if(playerTurn == 1)
            {
                EnableAvailablePieces();
            }

            // if this is true, game is over
            if (gameCore.SetPiece(selectedPiece.name, button.name))
                GameOver();
            else
                EndTurn();
        }
    }

    public void SetSelectedPiece(GamePiece gamePiece)
    {
        Button chooseButton = GameObject.Find("ChoosePiece").GetComponent<Button>();
        selectedPiece = gamePiece;
        Vector3 newPosition = chooseButton.transform.position;
        selectedPiece.transform.position = newPosition;
    }

    public List<GameCore.Piece> GetAvailablePieces()
    {
        return gameCore.availablePieces;
    }

    public void EndTurn()
    {
        selectedPiece = null;
        ChangeSides();

        if (GameInfo.gameType == 'E')
            EasyAIGame();
        else if (GameInfo.gameType == 'H')
            HardAIGame();
        else if (GameInfo.gameType == 'N')
            NetworkGame();
        else
            StoryMode();
    }

    void GameOver()
    {
        Debug.Log("GameOver");
        SetBoardInteractable(false);
    }

    void SetGameControllerReferenceOnGamePieces()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            gamePieces[i].GetComponent<GamePiece>().SetGameControllerReference(this);
        }
    }

    void ChangeSides()
    {
        playerTurn = (playerTurn == 1) ? 2 : 1;
    }

    public void DisableUserInput()
    {
        foreach (Button button in buttonList)
            button.interactable = false;
    }

    public void EnableAvailablePieces()
    {
        foreach (GameCore.Piece availablePiece in gameCore.availablePieces)
            foreach (GamePiece piece in gamePieces)
                if (availablePiece.id == piece.name.Substring(10))
                {
                    Debug.Log(piece.name);
                    piece.GetComponent<BoxCollider2D>().enabled = true;
                    break;
                }
    }

    public void DisableAllPieces()
    {
        foreach (GamePiece piece in gamePieces)
        {
            piece.GetComponent<BoxCollider2D>().enabled = false;
        }
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

    public void RestartGame()
    {
        playerTurn = 1;
        SetBoardInteractable(true);
    }

    public void SetBoardInteractable(bool toggle)
    {
        foreach (Button button in buttonList)
            button.interactable = toggle;
    }
    #endregion
}