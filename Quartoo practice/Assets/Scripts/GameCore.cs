using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    //private GameController gameController;
    public struct Piece
    {
        public int height;
        public int color;
        public int type;
        public int emblem;
        public string id;

        public Piece (int height, int color, int type, int emblem, string id)
        {
            this.height = height;
            this.color = color;
            this.type = type;
            this.emblem = emblem;
            this.id = id;
        }
    }
    public struct BoardSpace
    {
        public string id;
        public int row;
        public int col;

        public BoardSpace (string id, int row, int col)
        {
            this.id = id;
            this.row = row;
            this.col = col;
        }
    }

    // initialize GameBoard with empty slots
    public Piece[][] gameBoard = new Piece[4][] {
            new Piece[] {new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, "")},
            new Piece[] {new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, "")},
            new Piece[] {new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, "")},
            new Piece[] {new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, ""), new Piece(2, 0, 0, 0, "") }
    };

    // usedPieces is empty until a game piece is set
    public List<Piece> usedPieces = new List<Piece>();
  
    // initialize availablePieces (it will include all game pieces until the first move)
    public List<Piece> availablePieces = new List<Piece>()
    {
        // Gold Pieces
        new Piece(0, 0, 0, 0, "A1"),
        new Piece(0, 0, 0, 1, "A2"),
        new Piece(0, 0, 1, 0, "A3"),
        new Piece(0, 0, 1, 1, "A4"),
        new Piece(0, 1, 0, 0, "B1"),
        new Piece(0, 1, 0, 1, "B2"),
        new Piece(0, 1, 1, 0, "B3"),
        new Piece(0, 1, 1, 1, "B4"),

        // Silver Pieces
        new Piece(1, 0, 0, 0, "C1"),
        new Piece(1, 0, 0, 1, "C2"),
        new Piece(1, 0, 1, 0, "C3"),
        new Piece(1, 0, 1, 1, "C4"),
        new Piece(1, 1, 0, 0, "D1"),
        new Piece(1, 1, 0, 1, "D2"),
        new Piece(1, 1, 1, 0, "D3"),
        new Piece(1, 1, 1, 1, "D4")
    };

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

    // Check a substring of position, and set row/col to the correct values that correspond
    // with the gridspaces in unity
    private BoardSpace ConvertPosition(string position)
    {
        BoardSpace convertedBoardSpace = new BoardSpace();
        string subStringPosition = position.Substring(12);

        foreach (BoardSpace space in availableBoardSpaces)
        {
            if (subStringPosition == space.id)
                convertedBoardSpace = space;
        }
        // remove col and rows
        return convertedBoardSpace;
    }

    private Piece ConvertGamePiece(string gamePiece)
    {
        Piece convertedGamePiece = new Piece();
        string subStringPiece = gamePiece.Substring(10);

        foreach (Piece piece in availablePieces)
            if (subStringPiece == piece.id)
                convertedGamePiece = piece;

        return convertedGamePiece;
    }

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
        if (a.height == 2 || b.height == 2 || c.height == 2 || d.height == 2)
            return false;

        // checks if there are 4 GamePieces next to each other with similiar stats
        if (a.height == b.height && a.height == c.height && a.height == d.height)
            return true;

        else if (a.color == b.color && a.color == c.color && a.color == d.color)
            return true;

        else if (a.emblem == b.emblem && a.emblem == c.emblem && a.emblem == d.emblem)
            return true;

        else if (a.type == b.type && a.type == c.type && a.type == d.type)
            return true;

        // if there arent any conditions met, that means that there isn't a winner
        return false;
    }
}
