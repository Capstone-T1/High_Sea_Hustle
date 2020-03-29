using UnityEngine;
using System.Collections.Generic;

public class AIHard
{
    private GameCore gameCore = new GameCore();
    /*
        *************************
        Phase 1 (Pick a place for piece given)
        *************************
        1. Check for Win, if there is, place piece in winning location
        //1 is Unnecessary while available pieces > 12
        2. Picking Piece:
            A. If AI goes second (places piece first), pick location randomly, then follow 2.B
            B. If AI goes first (picks piece first), place opposite of opponent
                I. If location is occupied, check list of available spaces for location one bit off
                    (example: if opp piece is in 1111, and location 0000 is occupied, 
                                choose available location 0111,1011,1101,or 1110 if available)
                II. If locations from 2.B.I is occupied, check list of available spaces for location 
                    two bits off (1100,1001,0011,0110,0101,1010)
                III. If locations from 2.B.II is occupied, check list of available spaces for location 
                    three bits off (1000,0100,0010,0001)
        3. Else, place piece
                //Valid placement and covers Ties

        //Question: If the AI notices a possible win, but the piece it was given does not satisfy
                    condition, should the AI place the piece there as to prevent a win for the 
                    opponent?
                    It is the question of should the AI be aggressive or defensive? Should it leave
                    the possibility for a win or prevent as many possible losses?
                    I play aggressive, leaving those areas open, but I am open to suggestions on this.
    */
    public string choosePosition(List<GameCore.BoardSpace> availableBoardSpaces, GameCore.Piece pieceGivenToAI)
    {
        string chosenLocation = null;
        int numOfAvailablePositions = availableBoardSpaces.Count;

        //Check for Win
        if (numOfAvailablePositions <= 13)
        {
            //Check to see if peice given leads to win
            string winningLocation = checkForWinPosition(pieceGivenToAI, availableBoardSpaces);
            if(winningLocation != null)
            {
                chosenLocation = winningLocation;
                return chosenLocation;
            }
        }

        //When AI goes second, choosing very first location
        if (numOfAvailablePositions == 16)
        {
            int option = Random.Range(0, numOfAvailablePositions);
            chosenLocation = availableBoardSpaces[option].id;

            return chosenLocation;
        }

        //Choose Location for other conditions

        return chosenLocation;
    }


    /*
        *************************
        Phase 2 (Pick a piece for opponent)
        *************************
        //Thing to consider is AIs should be playing optimally, should give no chance of winning
        //no matter what; however, humans do not play optimally and may not notice winning 
        //conditions

        1. Check for loss in available pieces, avoid giving that piece[s]
        2. If no pieces match Step 1, give piece 1 bit off what was just given while there is not
            a row of three sharing condition[s]
            (example: AI was given 1111, AI will give 1101 unless there is row of 1111,1001,and 1011)
        3. When shared condition on the board is found (like in example just mentioned), give piece
            1 bit off what was given by player from available pieces that do not have said condition
        4. If available pieces != 1 and said pieces have at least one condition leading to loss,
            give piece with least possible of loss combinations (I hope this makes sense)
        5. If all pieces lead to loss, give piece at random (if human, the game may continue as
            humans do not play optimally)

        //I'm not sure where to put this in Phase 2, but I normally try to give a piece that forces
        //an opponent to play it in a specific location or gives them fewer options to win. I 
        //believe this is the minimax algorithm that I described.
    */

    public string chooseGamePiece(List<GameCore.Piece> availablePieces, GameCore.Piece pieceAIPlaced)
    {
        string chosenPiece = null;
        List<GameCore.Piece> viablePieces = new List<GameCore.Piece>();
        int numOfAvailablePieces = availablePieces.Count;
        int numOfViablePieces = viablePieces.Count;

        //Check for check for possible loss
        if (numOfAvailablePieces > 14)
        {
            //Find possible condition(s) on board that leads to Opponent winning
            //Add all pieces without condition to viablePieces
        }

        //chosenPiece = viable piece one bit off from pieceAIPlaced that is in viablePieces

        if (numOfViablePieces == 0)
        {
            //Find Piece in AvailablePieces that give the minimum amount of loss conditions
            //chosenPiece = said piece from above


            //OR

            //int option = Random.Range(0, numOfAvailablePieces);
            //string chosenPiece = availablePieces[option].id;

            return chosenPiece;
        }

        viablePieces.Clear();
        return chosenPiece;
    }



























    public string checkForWinPosition(GameCore.Piece givenPiece, List<GameCore.BoardSpace> availableSpaces)
    {
        string winningPosition = null;
        GameCore.Piece[][] AITempBoard = gameCore.GetGameBoard();
        List<GameCore.BoardSpace> tempList = availableSpaces;

        for (int i = 0; i < (tempList.Count-1); i++)
        {
            AITempBoard[tempList[i].row][tempList[i].col] = givenPiece;

            // checks the rows
            for (int j = 0; j < AITempBoard.Length; j++)
            {
                GameCore.Piece[] result = AITempBoard[j];
                if (checkWinConditions(result[0], result[1], result[2], result[3]))
                {
                    winningPosition = tempList[i].id;
                    return winningPosition;
                }
            }

            // checks the cols
            for (int j = 0; j < AITempBoard.Length; j++)
            {
                GameCore.Piece[] result = new GameCore.Piece[4];
                for (int k = 0; k < 4; k++)
                    result[j] = AITempBoard[k][j];
                if (checkWinConditions(result[0], result[1], result[2], result[3]))
                {
                    winningPosition = tempList[i].id;
                    return winningPosition;
                }
            }

            // checks the main diagonal (left to right)
            if (checkWinConditions(AITempBoard[0][0], AITempBoard[1][1], AITempBoard[2][2], AITempBoard[3][3]))
            {
                winningPosition = tempList[i].id;
                return winningPosition;
            }

            //// checks the secondary diagonal (right to left)
            if (checkWinConditions(AITempBoard[0][3], AITempBoard[1][2], AITempBoard[2][1], AITempBoard[3][0]))
            {
                winningPosition = tempList[i].id;
                return winningPosition;
            }

            //Resetting Location
            AITempBoard[tempList[i].row][tempList[i].col] = new GameCore.Piece(2, 0, 0, 0, "");
        }

        return winningPosition;
    }

    //*****************************************************************************************************
    //Taken from GameCore.cs
    private bool checkWinConditions(GameCore.Piece a, GameCore.Piece b, GameCore.Piece c, GameCore.Piece d)
    {
        // checks if the other gameBoard of the game board are empty (no GamePieces on them)          
        if (a.color == 2 || b.color == 2 || c.color == 2 || d.color == 2)
            return false;

        // checks if there are 4 GamePieces next to each other with similiar stats
        if (a.color == b.color && a.color == c.color && a.color == d.color)
        {
            Debug.Log("Possible win by color");
            return true;
        }

        else if (a.height == b.height && a.height == c.height && a.height == d.height)
        {
            Debug.Log("Possible win by height");
            return true;
        }
        else if (a.shape == b.shape && a.shape == c.shape && a.shape == d.shape)
        {
            Debug.Log("Possible win by shape");
            return true;
        }
        else if (a.emblem == b.emblem && a.emblem == c.emblem && a.emblem == d.emblem)
        {
            Debug.Log("Possible win by emblem");
            return true;
        }
        // if there arent any conditions met, that means that there isn't a winner
        return false;
    }
}