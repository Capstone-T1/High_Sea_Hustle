using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{
    public Button button;
    public GamePiece gamePiece;
    private GameController gameController;
    bool isTie = false;

    // initialize GameBoard with empty slots
    GamePiece[][] positions = new GamePiece[4][] {
            new GamePiece[] {null, null, null, null },
            new GamePiece[] {null, null, null, null },
            new GamePiece[] {null, null, null, null },
            new GamePiece[] {null, null, null, null } };

    // NOTE: May have the game controller keep track of this
    //GamePiece[] gamePieces = new GamePiece[16];
    //List<GamePiece> availablePieces = new List<GamePiece>();
    //List<GamePiece> usedPieces = new List<GamePiece>();

    public void SetSpace()
    {
        GamePiece selectedPiece = gameController.GetSelectedPiece();

        if (selectedPiece != null)
        {
            Vector3 newPosition = button.transform.position;
            newPosition.z += 15;
            newPosition.y += 5;
            Debug.Log(newPosition);
            selectedPiece.transform.position = newPosition;
            SetGamePieceAtrributes(selectedPiece);
            gameController.AddToUsedPieces(selectedPiece);
            gameController.RemoveFromAvailablePieces(selectedPiece);
            gameController.SetSelectedPiece(null);
            gameController.SetRecentMove(this.button);
            button.interactable = false;
            gameController.EndTurn();
        }
    }

    public void MovePiece()
    {

    }

    public void SetGamePieceAtrributes(GamePiece selectedGamePiece)
    {
        gamePiece.height = selectedGamePiece.height;
        gamePiece.emblem = selectedGamePiece.emblem;
        gamePiece.color = selectedGamePiece.color;
        gamePiece.type = selectedGamePiece.type;

    }

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Returns true if a piece was successfully placed
    //public bool placeGamePiece()
    //{
    //    // If position already taken returns 16; if invalid, returns 17
    //    int boardPosition = getPosition();

    //    // Since are board only goes up to 16 positions (0 - 15), 16 and 17 will never be a viable position so 
    //    // we can use it for an error check
    //    if (boardPosition > 15)
    //    {
    //        string error = (boardPosition == 16) ? "A piece was already placed there. Choose another position" :
    //            "Please choose a valid position (0-15).";

    //        return false;
    //    }

    //    // If piece is already used returns 16; if invalid, returns 17
    //    int gamePiece = getGamePiece();

    //    // Since we only have 16 pieces (0 - 15), 16 and 17 will never be a viable position so 
    //    // we can use it for an error check
    //    if (gamePiece > 15)
    //    {
    //        string error = (gamePiece == 16) ? "This GamePiece is already on the game board, choose another one." :
    //             "Please choose a valid GamePiece (0-15).";

    //        return false;
    //    }

    //    // Set row and col values
    //    int row = boardPosition / 4;
    //    int col = boardPosition % 4;

    //    positions[row][col] = gamePieces[gamePiece];
    //    //updateGameBoard(boardPosition, gamePieces[gamePiece]);
    //    gamePieces[gamePiece] = null;
    //    //availablePieces--;

    //    return true;
    //}

    public int getPosition()
    {
        int currentPosition = 5;

        // Check for out of range input
        if (currentPosition < 0 || currentPosition > 15)
            return 17;

        int row = currentPosition / 4;
        int col = currentPosition % 4;

        // Checks if there is a GamePiece on the current position
        if (positions[row][col] != null)
            return 16;

        return currentPosition;
    }

    //public int getGamePiece()
    //{
    //    int currentGamePiece = 5;

    //    // Check for out of range input
    //    if (currentGamePiece < 0 || currentGamePiece > 15)
    //        return 17;

    //    // Checks if the GamePiece is already used
    //    if (gamePieces[currentGamePiece] == null)
    //        return 16;

    //    return currentGamePiece;
    //}

    // Stars a new game if the user chooses to
    public bool startNewGame()
    {
        string result = (isTie == false) ? "Winner!" : "It's a tie!";
        string playAgain = "y";

        while (true)
        {
            if (playAgain.ToLower() == "no" || playAgain.ToLower() == "n")
            {
                return false;
            }
            else if (playAgain.ToLower() == "yes" || playAgain.ToLower() == "y")
                return true;
        }
    }

    //// Adds the game piece selected on the game board
    //public void updateGameBoard(int position, GamePiece gamePiece)
    //{
    //    positionsDrawn[position] = gamePiece.bitValue;
    //}

    // Returns a string of game pieces not played
    //public List<GamePiece> getAvailablePieces()
    //{
    //    List<GamePiece> availablePieces = new List<GamePiece>();

    //    foreach (GamePiece piece in gamePieces)
    //        if (piece != null)
    //            availablePieces.Add(piece);

    //    return availablePieces;
    //}
}
