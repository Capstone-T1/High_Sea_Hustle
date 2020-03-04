using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public struct Piece
    {
        public int color;
        public int height;
        public int shape;
        public int emblem;
        public string id;

        public Piece(int color, int height, int shape, int emblem, string id)
        {
            this.color = color;
            this.height = height;
            this.shape = shape;
            this.emblem = emblem;
            this.id = id;
        }
    }
    public struct BoardSpace
    {
        public string id;
        public int row;
        public int col;

        public BoardSpace(string id, int row, int col)
        {
            this.id = id;
            this.row = row;
            this.col = col;
        }
    }

    // Initialize GameBoard with empty slots
    public Piece[][] gameBoard = new Piece[4][] {
            new Piece[] {new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, "")},
            new Piece[] {new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, "")},
            new Piece[] {new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, "")},
            new Piece[] {new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, "") }
    };

    // NOTE: may not need this list
    // UsedPieces is empty until a game piece is set
    public List<Piece> usedPieces = new List<Piece>();

    // Initialize availablePieces (it will include all game pieces until the first move)
    public List<Piece> availablePieces = new List<Piece>()
    {
        // Gold Pieces
        new Piece(0, 0, 0, 0, "A1"), // gold, short, round, no emblem
        new Piece(0, 0, 0, 1, "A2"), // gold, short, round, emblem
        new Piece(0, 0, 1, 0, "A3"), // gold, short, triangle, no emblem
        new Piece(0, 0, 1, 1, "A4"), // gold, short, triangle, emblem
        new Piece(0, 1, 0, 0, "B1"), // gold, tall, round, no emblem
        new Piece(0, 1, 0, 1, "B2"), // gold, tall, round, emblem
        new Piece(0, 1, 1, 0, "B3"), // gold, tall, triangle, no emblem
        new Piece(0, 1, 1, 1, "B4"), // gold, tall, tirangle, emblem

        // Silver Pieces
        new Piece(1, 0, 0, 0, "C1"), // silver, short, round, no emblem
        new Piece(1, 0, 0, 1, "C2"), // silver, short, round, emblem
        new Piece(1, 0, 1, 0, "C3"), // silver, short, triangle, no emblem
        new Piece(1, 0, 1, 1, "C4"), // silver, short, triangle, emblem
        new Piece(1, 1, 0, 0, "D1"), // silver, tall, round, no emblem
        new Piece(1, 1, 0, 1, "D2"), // silver, tall, round, emblem
        new Piece(1, 1, 1, 0, "D3"), // silver, tall, triangle, no emblem
        new Piece(1, 1, 1, 1, "D4")  // silver, tall, triangle, emblem
    };

    // Initialize availableBoardSpaces for the ai to use and calculate a move
    public List<BoardSpace> availableBoardSpaces = new List<BoardSpace>()
    {
        new BoardSpace("A1", 0, 0),
        new BoardSpace("A2", 0, 1),
        new BoardSpace("A3", 0, 2),
        new BoardSpace("A4", 0, 3),
        new BoardSpace("B1", 1, 0),
        new BoardSpace("B2", 1, 1),
        new BoardSpace("B3", 1, 2),
        new BoardSpace("B4", 1, 3),
        new BoardSpace("C1", 2, 0),
        new BoardSpace("C2", 2, 1),
        new BoardSpace("C3", 2, 2),
        new BoardSpace("C4", 2, 3),
        new BoardSpace("D1", 3, 0),
        new BoardSpace("D2", 3, 1),
        new BoardSpace("D3", 3, 2),
        new BoardSpace("D4", 3, 3),
    };

    public bool SetPiece(string gamePieceID, string position)
    {
        BoardSpace convertedBoardSpace = ConvertPosition(position);
        Piece convertedGamepiece = ConvertGamePiece(gamePieceID);
        usedPieces.Add(convertedGamepiece);
        availablePieces.Remove(convertedGamepiece);
        availableBoardSpaces.Remove(convertedBoardSpace);
        gameBoard[convertedBoardSpace.row][convertedBoardSpace.col] = convertedGamepiece;
        return (EndTurn() ? true : false);
    }

    // Check a substring of position (the id of the button in unity), and return 
    // the corresponding BoardSpace in availableBoardSpaces
    private BoardSpace ConvertPosition(string position)
    {
        BoardSpace convertedBoardSpace = new BoardSpace();
        string subStringPosition = position.Substring(12);

        foreach (BoardSpace space in availableBoardSpaces)
            if (subStringPosition == space.id)
                convertedBoardSpace = space;

        return convertedBoardSpace;
    }

    // Check the id of a gamePiece sent from unity, and return the corresponding Piece in availablePieces
    private Piece ConvertGamePiece(string gamePieceID)
    {
        Piece convertedGamePiece = new Piece();

        foreach (Piece piece in availablePieces)
            if (gamePieceID == piece.id)
                convertedGamePiece = piece;

        return convertedGamePiece;
    }

    // NOTE: not sure if we need these next two functions
    public Piece[][] GetGameBoard()
    {
        return gameBoard;
    }

    public void setGameBoard()
    {

    }

    private bool EndTurn()
    {
        // checks the rows
        for (int i = 0; i < gameBoard.Length; i++)
        {
            Piece[] result = gameBoard[i];
            if (checkWinConditions(result[0], result[1], result[2], result[3]))
                return true;
        }

        // checks the cols
        for (int i = 0; i < gameBoard.Length; i++)
        {
            Piece[] result = new Piece[4];
            for (int j = 0; j < 4; j++)
                result[j] = gameBoard[j][i];
            if (checkWinConditions(result[0], result[1], result[2], result[3]))
                return true;
        }

        // checks the main diagonal (left to right)
        if (checkWinConditions(gameBoard[0][0], gameBoard[1][1], gameBoard[2][2], gameBoard[3][3]))
            return true;

        //// checks the secondary diagonal (right to left)
        if (checkWinConditions(gameBoard[0][3], gameBoard[1][2], gameBoard[2][1], gameBoard[3][0]))
            return true;

        // Checks for a tie
        if (usedPieces.Count >= 16)
            return true;

        return false;
    }

    // Checks all possible conditions of a winning move
    private bool checkWinConditions(Piece a, Piece b, Piece c, Piece d)
    {
        // checks if the other gameBoard of the game board are empty (no GamePieces on them)          
        if (a.color == 2 || b.color == 2 || c.color == 2 || d.color == 2)
            return false;

        // checks if there are 4 GamePieces next to each other with similiar stats
        if (a.color == b.color && a.color == c.color && a.color == d.color)
        {
            Debug.Log("won by color");
            return true;
        }

        else if (a.height == b.height && a.height == c.height && a.height == d.height)
        {
            Debug.Log("won by height");
            return true;
        }
        else if (a.shape == b.shape && a.shape == c.shape && a.shape == d.shape)
        {
            Debug.Log("won by shape");
            return true;
        }
        else if (a.emblem == b.emblem && a.emblem == c.emblem && a.emblem == d.emblem)
        {
            Debug.Log("won by emblem");
            return true;
        }
        // if there arent any conditions met, that means that there isn't a winner
        return false;
    }
}
