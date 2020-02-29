using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<GamePiece> gamePieces;
    private GameCore gameCore = new GameCore();
    AIv1 aiController = new AIv1();
    public Button[] buttonList;
    public GamePiece selectedPiece;
    public Vector3 oldPosition;
    public Button recentMove;
    private int playerTurn;
    private bool placingPiece = false;

    void Awake()
    {
        //WARNING!! THESE ARE SET ONLY FOR TESTING!! DELETE THESE LATER!! ONLY TRISTAN
        //CAN DELETE THEM! DONT DELETE WITHOUT ASKING HIM FIRST PUNKS
        GameInfo.gameType = 'E';
        //GameInfo.selectPieceAtStart = 2;

        DisableAllBoardSpaces();
        SetGameControllerReferenceOnGamePieces();
        playerTurn = GameInfo.selectPieceAtStart;
    }

    void Start()
    {
        if (GameInfo.gameType == 'E' || GameInfo.gameType == 'H')
            StartAIGame();
        else if (GameInfo.gameType == 'N')
            StartNetworkingGame();
        else
            StartStoryModeGame();
    }

    #region Networking functions
    void StartNetworkingGame()
    {

    }

    void NetworkGame()
    {
        //do more stuff

    }

    // called by NetworkController when player has sent a move
    public void receiveMoveFromNetwork(string recvMove, string recvPiece)
    {
        // needs implementing - Tristan:  I could only send a move to the game controller
        // through a public function.  I was unable to put this in the NetworkGame() function
    }
    #endregion

    #region Story Mode Functions
    void StartStoryModeGame()
    {

    }

    void StoryModeGame()
    {
        // do stuff
    }
    #endregion

    #region AI Functions
    // NOTE: Do we want to add a short (three - five seconds) opening at start of an ai gamescreen?
    private void StartAIGame()
    {
        Debug.Log("Start ai game");

        // Player 1 (human) selects first piece
        if (playerTurn == 1)
        {
            Debug.Log("player started");
            // NOTE: Include some UI to inform user to select a piece
        }
        // Player 2 (ai) selects first piece
        else
        {
            Debug.Log("Ai started");
            EasyAIGame();
            // NOTE: Include some UI to inform user that the ai has already selected a piece
        }
    }

    void EasyAIGame()
    {
        // Player's turn
        if (playerTurn == 1)
        {
            // Player is placing a piece selected by the AI
            if (placingPiece == true)
            {
                Debug.Log("User placing a piece");

                //Make gameboard interactable, gamepieces not interactable
                DisableChooseOptions();
                EnableAvailableBoardSpaces();
                DisableAllPieces();
            }
            // Player is choosing a piece for the AI to place
            else
            {
                Debug.Log("User choosing opponents piece");

                DisableAllBoardSpaces();
                EnableAvailablePieces();
                EnableChooseOptions();
            }
        }
        // AI's turn
        else
        {
            DisableChooseOptions();
            DisableAllPieces();

            // AI is placing a piece by the user
            if (placingPiece == true)
            {
                Debug.Log("AI placing a piece");

                string aiBoardSpaceChosen = aiController.choosePosition(gameCore.availableBoardSpaces);
                Button boardSpace = ConvertAIBoardSpace(aiBoardSpaceChosen);
                StartCoroutine("DelayAIMove", boardSpace);
            }
            // AI is choosing a piece for the Player to place
            else
            {
                Debug.Log("AI choosing opponents piece");

                // Have ai pick piece
                string aiPieceChosen = aiController.chooseGamePiece(gameCore.availablePieces);
                ConvertAIPiece(aiPieceChosen);
                EndTurn();
            }
        }
    }

    //WARNING!! THIS IS NOT COMPLETE
    void HardAIGame()
    {
        // Player's turn
        if (playerTurn == 1)
        {
            // Have ai pick piece
            EnableAvailableBoardSpaces();
            //Make everything interactable
        }
        // AI's turn
        else
        {
            DisableAllBoardSpaces();
            string aiPieceChosen = aiController.chooseGamePiece(gameCore.availablePieces);
            ConvertAIPiece(aiPieceChosen);
            string aiBoardSpaceChosen = aiController.choosePosition(gameCore.availableBoardSpaces);
            Button boardSpace = ConvertAIBoardSpace(aiBoardSpaceChosen);
            StartCoroutine("DelayAIMove", boardSpace);
        }

    }

    // NOTE: Remove this delay after Levi gets a legit AI integrated
    IEnumerator DelayAIMove(Button boardSpace)
    {
        yield return new WaitForSeconds(2);
        PlacePieceOnBoard(boardSpace);
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

    #region Turn-Based Functions
    public void PlacePieceOnBoard(Button button)
    {
        string debug = (playerTurn == 1) ? "Player 1 placed a piece" : "Player 2 placed a piece";
        Debug.Log(debug);

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
                EnableChooseOptions();
            }
n

            // if this is true, game is over
            if (gameCore.SetPiece(selectedPiece.id, button.name))
                GameOver();
            else
                PiecePlaced();
        }
    }

    public void SelectOpponentsPiece()
    {
        if (selectedPiece)
            EndTurn();
    }

    private void PiecePlaced()
    {
        placingPiece = false;
        selectedPiece = null;

        if (GameInfo.gameType == 'E')
            EasyAIGame();
        else if (GameInfo.gameType == 'H')
            HardAIGame();
        else if (GameInfo.gameType == 'N')
            NetworkGame();
        else
            StoryModeGame();
    }

    public void SetSelectedPiece(GamePiece gamePiece)
    {
        Button StagePiece = GameObject.Find("StagePiece").GetComponent<Button>();

        if (selectedPiece)
        {
            selectedPiece.transform.position = oldPosition;
            selectedPiece = gamePiece;
            oldPosition = gamePiece.transform.position;
            Vector3 newPosition = StagePiece.transform.position;
            selectedPiece.transform.position = newPosition;
        }
        else      
        {
            selectedPiece = gamePiece;
            oldPosition = gamePiece.transform.position;
            Vector3 newPosition = StagePiece.transform.position;
            selectedPiece.transform.position = newPosition;
        }
    }

    public void ChooseAnotherPiece()
    {
        if (selectedPiece)
        {
            selectedPiece.transform.position = oldPosition;

            selectedPiece = null;
        }
    }

    public List<GameCore.Piece> GetAvailablePieces()
    {
        return gameCore.availablePieces;
    }

    public void EndTurn()
    {
        ChangeSides();
        placingPiece = true;

        if (GameInfo.gameType == 'E')
            EasyAIGame();
        else if (GameInfo.gameType == 'H')
            HardAIGame();
        else if (GameInfo.gameType == 'N')
            NetworkGame();
        else
            StoryModeGame();
    }

    void GameOver()
    {
        Debug.Log("GameOver");
        SceneManager.LoadScene("GameOver");
    }

    void ChangeSides()
    {
        playerTurn = (playerTurn == 1) ? 2 : 1;
    }



    #endregion

    #region 
    public void EnableAvailablePieces()
    {
        foreach (GameCore.Piece availablePiece in gameCore.availablePieces)
            foreach (GamePiece piece in gamePieces)
                if (availablePiece.id == piece.name.Substring(10))
                {
                    piece.GetComponent<BoxCollider2D>().enabled = true;
                    break;
                }
    }

    public void EnableChooseOptions()
    {
        Button ChoosePiece = GameObject.Find("ChoosePiece").GetComponent<Button>();
        Button ChooseAnother = GameObject.Find("ChooseAnother").GetComponent<Button>();

        ChooseAnother.interactable = true;
        ChoosePiece.interactable = true;

    }


    public void EnableAvailableBoardSpaces()
    {
        foreach (GameCore.BoardSpace availableButton in gameCore.availableBoardSpaces)
            foreach (Button button in buttonList)
                if (availableButton.id == button.name.Substring(12))
                {
                    button.interactable = true;
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

    public void DisableChooseOptions()
    {
        Button ChoosePiece = GameObject.Find("ChoosePiece").GetComponent<Button>();
        Button ChooseAnother = GameObject.Find("ChooseAnother").GetComponent<Button>();

        ChooseAnother.interactable = false;
        ChoosePiece.interactable = false;

    }

    public void DisableAllBoardSpaces()
    {
        foreach (Button button in buttonList)
            button.interactable = false;
    }
    #endregion

    #region Miscellaneous Functions
    void SetGameControllerReferenceOnGamePieces()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            gamePieces[i].GetComponent<GamePiece>().SetGameControllerReference(this);
        }
    }
    #endregion
}