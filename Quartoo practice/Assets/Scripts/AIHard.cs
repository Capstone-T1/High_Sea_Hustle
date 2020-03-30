using UnityEngine;
using System.Collections.Generic;

public class AIHard
{
    private GameCore gameCore = new GameCore();

/*********************************************
        Phase 1 (Pick a place for piece given)
**********************************************
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
    public string ChoosePosition(List<GameCore.BoardSpace> availableBoardSpaces, GameCore.Piece pieceGivenToAI)
    {
        string chosenLocation = null;
        int numOfAvailablePositions = availableBoardSpaces.Count;

        //Check for Win
        if (numOfAvailablePositions <= 13)
        {
            //Check to see if peice given leads to win
            string winningLocation = CheckForWinPosition(pieceGivenToAI, availableBoardSpaces);
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


/******************************************
        Phase 2 (Pick a piece for opponent)
*******************************************
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

    public string ChooseGamePiece(List<GameCore.Piece> availablePieces, GameCore.Piece pieceAIPlaced)
    {
        string chosenPiece = null;
        List<GameCore.Piece> viablePieces = new List<GameCore.Piece>();
        GameCore.Piece[][] AITempBoard = gameCore.GetGameBoard();
        int numOfAvailablePieces = availablePieces.Count;
        int numOfViablePieces = viablePieces.Count;

        //Check for check for possible loss
        if (numOfAvailablePieces > 14)
        {
            int loss;
            // checks the rows
            for (int i = 0; i < AITempBoard.Length; i++)
            {
                GameCore.Piece[] result = AITempBoard[i];
                loss = CheckLossConditions(result[0], result[1], result[2], result[3]);
                if (loss != 0)
                    switch (loss)
                    {
                        case 1:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].color == 1)
                                    viablePieces.Add(availablePieces[j]);
                            break;
                        case 2:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].color == 0)
                                    viablePieces.Add(availablePieces[j]);
                            break;
                        case 3:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].height == 1)
                                    viablePieces.Add(availablePieces[j]);
                            break;
                        case 4:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].height == 0)
                                    viablePieces.Add(availablePieces[j]);
                            break;
                        case 5:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].shape == 1)
                                    viablePieces.Add(availablePieces[j]);
                            break;
                        case 6:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].shape == 0)
                                    viablePieces.Add(availablePieces[j]);
                            break;
                        case 7:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].emblem == 1)
                                    viablePieces.Add(availablePieces[j]);
                            break;
                        case 8:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].emblem == 0)
                                    viablePieces.Add(availablePieces[j]);
                            break;

                    }
            }

            for (int i = 0; i < AITempBoard.Length; i++)
            {
                GameCore.Piece[] result = new GameCore.Piece[4];
                for (int j = 0; j < 4; j++)
                    result[j] = AITempBoard[j][i];
                loss = CheckLossConditions(result[0], result[1], result[2], result[3]);
                if (loss != 0)
                    switch (loss)
                    {
                        case 1:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].color == 1)
                                    viablePieces.Add(availablePieces[j]);
                            break;
                        case 2:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].color == 0)
                                    viablePieces.Add(availablePieces[j]);
                            break;
                        case 3:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].height == 1)
                                    viablePieces.Add(availablePieces[j]);
                            break;
                        case 4:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].height == 0)
                                    viablePieces.Add(availablePieces[j]);
                            break;
                        case 5:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].shape == 1)
                                    viablePieces.Add(availablePieces[j]);
                            break;
                        case 6:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].shape == 0)
                                    viablePieces.Add(availablePieces[j]);
                            break;
                        case 7:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].emblem == 1)
                                    viablePieces.Add(availablePieces[j]);
                            break;
                        case 8:
                            for (int j = 0; j < numOfAvailablePieces; j++)
                                if (availablePieces[j].emblem == 0)
                                    viablePieces.Add(availablePieces[j]);
                            break;

                    }
            }

            loss = CheckLossConditions(AITempBoard[0][0], AITempBoard[1][1], AITempBoard[2][2], AITempBoard[3][3]);
            if (loss != 0)
            {
                switch (loss)
                {
                    case 1:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].color == 1)
                                viablePieces.Add(availablePieces[j]);
                        break;
                    case 2:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].color == 0)
                                viablePieces.Add(availablePieces[j]);
                        break;
                    case 3:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].height == 1)
                                viablePieces.Add(availablePieces[j]);
                        break;
                    case 4:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].height == 0)
                                viablePieces.Add(availablePieces[j]);
                        break;
                    case 5:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].shape == 1)
                                viablePieces.Add(availablePieces[j]);
                        break;
                    case 6:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].shape == 0)
                                viablePieces.Add(availablePieces[j]);
                        break;
                    case 7:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].emblem == 1)
                                viablePieces.Add(availablePieces[j]);
                        break;
                    case 8:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].emblem == 0)
                                viablePieces.Add(availablePieces[j]);
                        break;

                }
            }

            loss = CheckLossConditions(AITempBoard[0][3], AITempBoard[1][2], AITempBoard[2][1], AITempBoard[3][0]);
           if (loss != 0)
            {
                switch (loss)
                {
                    case 1:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].color == 1)
                                viablePieces.Add(availablePieces[j]);
                        break;
                    case 2:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].color == 0)
                                viablePieces.Add(availablePieces[j]);
                        break;
                    case 3:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].height == 1)
                                viablePieces.Add(availablePieces[j]);
                        break;
                    case 4:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].height == 0)
                                viablePieces.Add(availablePieces[j]);
                        break;
                    case 5:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].shape == 1)
                                viablePieces.Add(availablePieces[j]);
                        break;
                    case 6:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].shape == 0)
                                viablePieces.Add(availablePieces[j]);
                        break;
                    case 7:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].emblem == 1)
                                viablePieces.Add(availablePieces[j]);
                        break;
                    case 8:
                        for (int j = 0; j < numOfAvailablePieces; j++)
                            if (availablePieces[j].emblem == 0)
                                viablePieces.Add(availablePieces[j]);
                        break;

                }
            }
        }

        if (numOfViablePieces == 0)
        {
            int option = Random.Range(0, numOfAvailablePieces);
            chosenPiece = availablePieces[option].id;

            return chosenPiece;
        }

        //chosenPiece = viable piece one bit off from pieceAIPlaced that is in viablePieces

        viablePieces.Clear();
        return chosenPiece;
    }


/*******************************************
Extra Necessary Functions
*******************************************/
    public string CheckForWinPosition(GameCore.Piece givenPiece, List<GameCore.BoardSpace> availableSpaces)
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
                if (CheckWinConditions(result[0], result[1], result[2], result[3]))
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
                if (CheckWinConditions(result[0], result[1], result[2], result[3]))
                {
                    winningPosition = tempList[i].id;
                    return winningPosition;
                }
            }

            // checks the main diagonal (left to right)
            if (CheckWinConditions(AITempBoard[0][0], AITempBoard[1][1], AITempBoard[2][2], AITempBoard[3][3]))
            {
                winningPosition = tempList[i].id;
                return winningPosition;
            }

            //// checks the secondary diagonal (right to left)
            if (CheckWinConditions(AITempBoard[0][3], AITempBoard[1][2], AITempBoard[2][1], AITempBoard[3][0]))
            {
                winningPosition = tempList[i].id;
                return winningPosition;
            }

            //Resetting Location
            AITempBoard[tempList[i].row][tempList[i].col] = new GameCore.Piece(2, 0, 0, 0, "");
        }

        return winningPosition;
    }

    private int CheckLossConditions(GameCore.Piece a, GameCore.Piece b, GameCore.Piece c, GameCore.Piece d)
    {
        /*
          Possible Results:
          0 - None
          1 - Gold
          2 - Silver
          3 - Short
          4 - Tall
          5 - Round
          6 - Triangle
          7 - No Emblem
          8 - Emblem
        */

        // checks if the other gameBoard of the game board are empty (no GamePieces on them)          
        if (a.color == 2 || b.color == 2 || c.color == 2 || d.color == 2)
            return 0;

        // checks if there are 4 GamePieces next to each other with similiar stats
        //Checks Color
        if ((a.color == b.color && a.color == c.color) ||
            (b.color == c.color && b.color == d.color) ||
            (a.color == b.color && a.color == d.color) ||
            (a.color == c.color && a.color == d.color))
        {
            //if Gold
            if((a.color == 0 && b.color == 0 && c.color == 0) ||
               (b.color == 0 && c.color == 0 && d.color == 0) ||
               (a.color == 0 && b.color == 0 && d.color == 0) ||
               (a.color == 0 && b.color == 0 && c.color == 0))
            {
                Debug.Log("Possible loss by Gold on Board");
                return 1;
            }
            //If Silver
            else if ((a.color == 1 && b.color == 1 && c.color == 1) ||
                     (b.color == 1 && c.color == 1 && d.color == 1) ||
                     (a.color == 1 && b.color == 1 && d.color == 1) ||
                     (a.color == 1 && b.color == 1 && c.color == 1))
            {
                Debug.Log("Possible loss by Silver on Board");
                return 2;
            }
        }

        //Checks Height
        else if ((a.height == b.height && a.height == c.height) ||
                 (b.height == c.height && b.height == d.height) ||
                 (a.height == b.height && a.height == d.height) ||
                 (a.height == c.height && a.height == d.height))

        {
            //If Short
            if ((a.height == 0 && b.height == 0 && c.height == 0) ||
               (b.height == 0 && c.height == 0 && d.height == 0) ||
               (a.height == 0 && b.height == 0 && d.height == 0) ||
               (a.height == 0 && b.height == 0 && c.height == 0))
            {
                Debug.Log("Possible loss by Short on Board");
                return 3;
            }
            //If Tall
            else if ((a.height == 1 && b.height == 1 && c.height == 1) ||
                     (b.height == 1 && c.height == 1 && d.height == 1) ||
                     (a.height == 1 && b.height == 1 && d.height == 1) ||
                     (a.height == 1 && b.height == 1 && c.height == 1))
            {
                Debug.Log("Possible loss by Tall on Board");
                return 4;
            }
        }

        //Checks Shape
        else if ((a.shape == b.shape && a.shape == c.shape) ||
                 (b.shape == c.shape && b.shape == d.shape) ||
                 (a.shape == b.shape && a.shape == d.shape) ||
                 (a.shape == c.shape && a.shape == d.shape))
        {
            //If Round
            if ((a.shape == 0 && b.shape == 0 && c.shape == 0) ||
               (b.shape == 0 && c.shape == 0 && d.shape == 0) ||
               (a.shape == 0 && b.shape == 0 && d.shape == 0) ||
               (a.shape == 0 && b.shape == 0 && c.shape == 0))
            {
                Debug.Log("Possible loss by Round on Board");
                return 5;
            }
            //If Triangle
            else if ((a.shape == 1 && b.shape == 1 && c.shape == 1) ||
                     (b.shape == 1 && c.shape == 1 && d.shape == 1) ||
                     (a.shape == 1 && b.shape == 1 && d.shape == 1) ||
                     (a.shape == 1 && b.shape == 1 && c.shape == 1))
            {
                Debug.Log("Possible loss by Triangle on Board");
                return 6;
            }
        }

        //Checks Emblem
        else if ((a.emblem == b.emblem && a.emblem == c.emblem) ||
                 (b.emblem == c.emblem && b.emblem == d.emblem) ||
                 (a.emblem == b.emblem && a.emblem == d.emblem) ||
                 (a.emblem == c.emblem && a.emblem == d.emblem))
        {
            //If No Emblem
            if ((a.emblem == 0 && b.emblem == 0 && c.emblem == 0) ||
               (b.emblem == 0 && c.emblem == 0 && d.emblem == 0) ||
               (a.emblem == 0 && b.emblem == 0 && d.emblem == 0) ||
               (a.emblem == 0 && b.emblem == 0 && c.emblem == 0))
            {
                Debug.Log("Possible loss by No Emblem on Board");
                return 7;
            }
            //If Emblem
            else if ((a.emblem == 1 && b.emblem == 1 && c.emblem == 1) ||
                     (b.emblem == 1 && c.emblem == 1 && d.emblem == 1) ||
                     (a.emblem == 1 && b.emblem == 1 && d.emblem == 1) ||
                     (a.emblem == 1 && b.emblem == 1 && c.emblem == 1))
            {
                Debug.Log("Possible loss by Emblem on Board");
                return 8;
            }
        }
        // if there arent any conditions met, that means that there isn't a winner
        return 0;
    }

    private bool CheckWinConditions(GameCore.Piece a, GameCore.Piece b, GameCore.Piece c, GameCore.Piece d)
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