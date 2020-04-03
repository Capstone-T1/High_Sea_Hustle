using UnityEngine;
using System.Collections.Generic;

public class AIHard
{
    private GameCore gameCore = new GameCore();
    private GameCore.Piece pieceAIPlaced;

    /*********************************************
            Phase 1 (Pick a place for piece given)
    **********************************************
           //Question: If the AI notices a possible win, but the piece it was given does not satisfy
                        condition, should the AI place the piece there as to prevent a win for the 
                        opponent?
                        It is the question of should the AI be aggressive or defensive? Should it leave
                        the possibility for a win or prevent as many possible losses?
                        I play aggressive, leaving those areas open, but I am open to suggestions on this.
    */
    void Awake()
    {
        pieceAIPlaced = ConvertGamePiece("AI", gameCore.availablePieces);
    }

    public string ChooseLocation(GameCore.Piece[][] newGameBoard, List<GameCore.BoardSpace> availableBoardSpaces, List<GameCore.Piece> availablePieces, string pieceGivenToAIID, string recentMoveID)
    {
        GameCore.BoardSpace chosenLocation = ConvertPosition(recentMoveID, availableBoardSpaces);
        GameCore.Piece pieceGivenToAI = ConvertGamePiece(pieceGivenToAIID, availablePieces);
        gameCore.setGameBoard(newGameBoard);
        pieceAIPlaced = pieceGivenToAI;
        string chosenLocationString;
        int numOfAvailablePositions = availableBoardSpaces.Count;

        //Check for Win
        if (numOfAvailablePositions <= 13)
        {
            //Check to see if peice given leads to win
            string winningLocation = CheckForWinPosition(pieceGivenToAI, availableBoardSpaces);
            if (winningLocation != null)
            {
                chosenLocationString = winningLocation;
                return chosenLocationString;
            }
        }

        //When AI goes second, choosing very first location
        if (numOfAvailablePositions == 16)
        {
            int rand = Random.Range(0, numOfAvailablePositions);
            chosenLocationString = availableBoardSpaces[rand].id;

            return chosenLocationString;
        }


        int numericLocationofRecentMove = (4 * chosenLocation.row) + chosenLocation.col;
        int numericLocationofChosenLocation = 15 - numericLocationofRecentMove;
        chosenLocation.row = numericLocationofChosenLocation / 4;
        chosenLocation.col = numericLocationofChosenLocation % 4;

        foreach (GameCore.BoardSpace a in availableBoardSpaces)
        {
            if (chosenLocation.row == a.row &&
                chosenLocation.col == a.col)
            {
                chosenLocation.id = a.id;
                chosenLocationString = chosenLocation.id;
                return chosenLocationString;
            }
        }

        int decision = Random.Range(0, 2);
        int tempDecision = decision;
        do
        {
            if (tempDecision == 0)
            {
                int cornerDecision = Random.Range(0, 3);
                int tempCornerDecision = cornerDecision;
                do
                {
                    switch (tempCornerDecision)
                    {
                        case 1:
                            chosenLocation.row = 0;
                            chosenLocation.col = 0;
                            break;
                        case 2:
                            chosenLocation.row = 0;
                            chosenLocation.col = 3;
                            break;
                        case 3:
                            chosenLocation.row = 3;
                            chosenLocation.col = 0;
                            break;
                        case 0:
                            chosenLocation.row = 3;
                            chosenLocation.col = 3;
                            break;
                    }

                    foreach (GameCore.BoardSpace a in availableBoardSpaces)
                    {
                        if (chosenLocation.row == a.row &&
                            chosenLocation.col == a.col)
                        {
                            chosenLocation.id = a.id;
                            chosenLocationString = chosenLocation.id;
                            return chosenLocationString;
                        }
                    }

                    tempCornerDecision = (tempCornerDecision + 3) % 4;

                } while (tempCornerDecision != cornerDecision);
            }
            else if (tempDecision == 1)
            {
                int internalDecision = Random.Range(0, 3);
                int tempInternalDecision = internalDecision;
                do
                {
                    switch (tempInternalDecision)
                    {
                        case 1:
                            chosenLocation.row = 1;
                            chosenLocation.col = 1;
                            break;
                        case 2:
                            chosenLocation.row = 1;
                            chosenLocation.col = 2;
                            break;
                        case 3:
                            chosenLocation.row = 2;
                            chosenLocation.col = 1;
                            break;
                        case 4:
                            chosenLocation.row = 2;
                            chosenLocation.col = 2;
                            break;
                    }

                    foreach (GameCore.BoardSpace a in availableBoardSpaces)
                    {
                        if (chosenLocation.row == a.row &&
                            chosenLocation.col == a.col)
                        {
                            chosenLocation.id = a.id;
                            chosenLocationString = chosenLocation.id;
                            return chosenLocationString;
                        }
                    }

                    tempInternalDecision = (tempInternalDecision + 3) % 4;

                } while (tempInternalDecision != internalDecision);
            }
            else
            {
                int externalDecision = Random.Range(0, 8);
                int tempExternalDecision = externalDecision;
                do
                {
                    switch (tempExternalDecision)
                    {
                        case 1:
                            chosenLocation.row = 0;
                            chosenLocation.col = 1;
                            break;
                        case 2:
                            chosenLocation.row = 0;
                            chosenLocation.col = 2;
                            break;
                        case 3:
                            chosenLocation.row = 1;
                            chosenLocation.col = 0;
                            break;
                        case 4:
                            chosenLocation.row = 2;
                            chosenLocation.col = 0;
                            break;
                        case 5:
                            chosenLocation.row = 1;
                            chosenLocation.col = 3;
                            break;
                        case 6:
                            chosenLocation.row = 2;
                            chosenLocation.col = 3;
                            break;
                        case 7:
                            chosenLocation.row = 3;
                            chosenLocation.col = 1;
                            break;
                        case 8:
                            chosenLocation.row = 3;
                            chosenLocation.col = 2;
                            break;
                    }

                    foreach (GameCore.BoardSpace a in availableBoardSpaces)
                    {
                        if (chosenLocation.row == a.row &&
                            chosenLocation.col == a.col)
                        {
                            chosenLocation.id = a.id;
                            chosenLocationString = chosenLocation.id;
                            return chosenLocationString;
                        }
                    }

                    tempExternalDecision = (tempExternalDecision + 3) % 8;

                } while (tempExternalDecision != externalDecision);
            }

            tempDecision = (tempDecision + 2) % 3;

        } while (tempDecision != decision);

        //Catch-All Default
        int option = Random.Range(0, numOfAvailablePositions);
        chosenLocationString = availableBoardSpaces[option].id;

        return chosenLocationString;
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

    public string ChooseGamePiece(List<GameCore.Piece> availablePieces)
    {
        GameCore.Piece chosenPiece = pieceAIPlaced;
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

        string chosenPieceString;

        //Chooses a losing piece at random if there is no non-losing piece
        if (numOfViablePieces == 0 || numOfAvailablePieces == 16)
        {
            int option = Random.Range(0, numOfAvailablePieces);
            chosenPieceString = availablePieces[option].id;

            return chosenPieceString;
        }

        //************************************
        //Checks (and chooses) piece 1-bit off
        int firstOption = Random.Range(0, 3);
        int tempFirstOption = firstOption;

        do
        {
            switch (tempFirstOption)
            {
                case 1:
                    if (chosenPiece.color == 0)
                        chosenPiece.color = 1;
                    else
                        chosenPiece.color = 0;
                    break;
                case 2:
                    if (chosenPiece.height == 0)
                        chosenPiece.height = 1;
                    else
                        chosenPiece.height = 0;
                    break;
                case 3:
                    if (chosenPiece.shape == 0)
                        chosenPiece.shape = 1;
                    else
                        chosenPiece.shape = 0;
                    break;
                case 0:
                    if (chosenPiece.emblem == 0)
                        chosenPiece.emblem = 1;
                    else
                        chosenPiece.emblem = 0;
                    break;
            }

            foreach (GameCore.Piece a in viablePieces)
            {
                if (chosenPiece.color == a.color &&
                   chosenPiece.height == a.height &&
                   chosenPiece.shape == a.shape &&
                   chosenPiece.emblem == a.emblem)
                {
                    chosenPiece.id = a.id;
                    chosenPieceString = chosenPiece.id;
                    return chosenPieceString;
                }
            }

            chosenPiece = pieceAIPlaced;
            tempFirstOption = (tempFirstOption + 1) % 4;

        }while (tempFirstOption != firstOption);

        //************************************
        //Checks (and chooses) piece 2-bit off
        int secondOption = Random.Range(0, 3);
        while (secondOption != firstOption)
            secondOption = Random.Range(0, 3);
        int tempSecondOption = secondOption;

        do
        {
            switch (tempFirstOption)
            {
                case 1:
                    if (chosenPiece.color == 0)
                        chosenPiece.color = 1;
                    else
                        chosenPiece.color = 0;
                    break;
                case 2:
                    if (chosenPiece.height == 0)
                        chosenPiece.height = 1;
                    else
                        chosenPiece.height = 0;
                    break;
                case 3:
                    if (chosenPiece.shape == 0)
                        chosenPiece.shape = 1;
                    else
                        chosenPiece.shape = 0;
                    break;
                case 0:
                    if (chosenPiece.emblem == 0)
                        chosenPiece.emblem = 1;
                    else
                        chosenPiece.emblem = 0;
                    break;
            }

            do
            {
                switch (tempSecondOption)
                {
                    case 1:
                        if (chosenPiece.color == 0)
                            chosenPiece.color = 1;
                        else
                            chosenPiece.color = 0;
                        break;
                    case 2:
                        if (chosenPiece.height == 0)
                            chosenPiece.height = 1;
                        else
                            chosenPiece.height = 0;
                        break;
                    case 3:
                        if (chosenPiece.shape == 0)
                            chosenPiece.shape = 1;
                        else
                            chosenPiece.shape = 0;
                        break;
                    case 0:
                        if (chosenPiece.emblem == 0)
                            chosenPiece.emblem = 1;
                        else
                            chosenPiece.emblem = 0;
                        break;
                }

                foreach (GameCore.Piece a in viablePieces)
                {
                    if (chosenPiece.color == a.color &&
                       chosenPiece.height == a.height &&
                       chosenPiece.shape == a.shape &&
                       chosenPiece.emblem == a.emblem)
                    {
                        chosenPiece.id = a.id;
                        chosenPieceString = chosenPiece.id;
                        return chosenPieceString;
                    }
                }

                switch (tempSecondOption)
                {
                    case 1:
                        if (chosenPiece.color == 0)
                            chosenPiece.color = 1;
                        else
                            chosenPiece.color = 0;
                        break;
                    case 2:
                        if (chosenPiece.height == 0)
                            chosenPiece.height = 1;
                        else
                            chosenPiece.height = 0;
                        break;
                    case 3:
                        if (chosenPiece.shape == 0)
                            chosenPiece.shape = 1;
                        else
                            chosenPiece.shape = 0;
                        break;
                    case 0:
                        if (chosenPiece.emblem == 0)
                            chosenPiece.emblem = 1;
                        else
                            chosenPiece.emblem = 0;
                        break;
                }

                tempSecondOption = (tempSecondOption + 1) % 4;

            } while (tempSecondOption != secondOption);

            chosenPiece = pieceAIPlaced;
            tempFirstOption = (tempFirstOption + 1) % 4;

        } while (tempFirstOption != firstOption);

        //************************************
        //Checks (and chooses) piece 3-bit off
        int thirdOption = Random.Range(0, 3);
        while (thirdOption != firstOption && thirdOption != secondOption)
            thirdOption = Random.Range(0, 3);
        int tempThirdOption = thirdOption;

        do
        {
            switch (tempFirstOption)
            {
                case 1:
                    if (chosenPiece.color == 0)
                        chosenPiece.color = 1;
                    else
                        chosenPiece.color = 0;
                    break;
                case 2:
                    if (chosenPiece.height == 0)
                        chosenPiece.height = 1;
                    else
                        chosenPiece.height = 0;
                    break;
                case 3:
                    if (chosenPiece.shape == 0)
                        chosenPiece.shape = 1;
                    else
                        chosenPiece.shape = 0;
                    break;
                case 0:
                    if (chosenPiece.emblem == 0)
                        chosenPiece.emblem = 1;
                    else
                        chosenPiece.emblem = 0;
                    break;
            }

            do
            {
                switch (tempSecondOption)
                {
                    case 1:
                        if (chosenPiece.color == 0)
                            chosenPiece.color = 1;
                        else
                            chosenPiece.color = 0;
                        break;
                    case 2:
                        if (chosenPiece.height == 0)
                            chosenPiece.height = 1;
                        else
                            chosenPiece.height = 0;
                        break;
                    case 3:
                        if (chosenPiece.shape == 0)
                            chosenPiece.shape = 1;
                        else
                            chosenPiece.shape = 0;
                        break;
                    case 0:
                        if (chosenPiece.emblem == 0)
                            chosenPiece.emblem = 1;
                        else
                            chosenPiece.emblem = 0;
                        break;
                }

                do
                {
                    switch (tempThirdOption)
                    {
                        case 1:
                            if (chosenPiece.color == 0)
                                chosenPiece.color = 1;
                            else
                                chosenPiece.color = 0;
                            break;
                        case 2:
                            if (chosenPiece.height == 0)
                                chosenPiece.height = 1;
                            else
                                chosenPiece.height = 0;
                            break;
                        case 3:
                            if (chosenPiece.shape == 0)
                                chosenPiece.shape = 1;
                            else
                                chosenPiece.shape = 0;
                            break;
                        case 0:
                            if (chosenPiece.emblem == 0)
                                chosenPiece.emblem = 1;
                            else
                                chosenPiece.emblem = 0;
                            break;
                    }

                    foreach (GameCore.Piece a in viablePieces)
                    {
                        if (chosenPiece.color == a.color &&
                           chosenPiece.height == a.height &&
                           chosenPiece.shape == a.shape &&
                           chosenPiece.emblem == a.emblem)
                        {
                            chosenPiece.id = a.id;
                            chosenPieceString = chosenPiece.id;
                            return chosenPieceString;
                        }
                    }

                    switch (tempThirdOption)
                    {
                        case 1:
                            if (chosenPiece.color == 0)
                                chosenPiece.color = 1;
                            else
                                chosenPiece.color = 0;
                            break;
                        case 2:
                            if (chosenPiece.height == 0)
                                chosenPiece.height = 1;
                            else
                                chosenPiece.height = 0;
                            break;
                        case 3:
                            if (chosenPiece.shape == 0)
                                chosenPiece.shape = 1;
                            else
                                chosenPiece.shape = 0;
                            break;
                        case 0:
                            if (chosenPiece.emblem == 0)
                                chosenPiece.emblem = 1;
                            else
                                chosenPiece.emblem = 0;
                            break;
                    }
                    tempThirdOption = (tempThirdOption + 1) % 4;

                } while (tempThirdOption != thirdOption);

                switch (tempSecondOption)
                {
                    case 1:
                        if (chosenPiece.color == 0)
                            chosenPiece.color = 1;
                        else
                            chosenPiece.color = 0;
                        break;
                    case 2:
                        if (chosenPiece.height == 0)
                            chosenPiece.height = 1;
                        else
                            chosenPiece.height = 0;
                        break;
                    case 3:
                        if (chosenPiece.shape == 0)
                            chosenPiece.shape = 1;
                        else
                            chosenPiece.shape = 0;
                        break;
                    case 0:
                        if (chosenPiece.emblem == 0)
                            chosenPiece.emblem = 1;
                        else
                            chosenPiece.emblem = 0;
                        break;
                }

                tempSecondOption = (tempSecondOption + 1) % 4;

            } while (tempSecondOption != secondOption);

            chosenPiece = pieceAIPlaced;
            tempFirstOption = (tempFirstOption + 1) % 4;

        } while (tempFirstOption != firstOption);

        //*****************************************************
        //chooses piece completely opposite from what AI placed

        if (chosenPiece.color == 0)
            chosenPiece.color = 1;
        else
            chosenPiece.color = 0;

        if (chosenPiece.height == 0)
            chosenPiece.height = 1;
        else
            chosenPiece.height = 0;

        if (chosenPiece.shape == 0)
            chosenPiece.shape = 1;
        else
            chosenPiece.shape = 0;

        if (chosenPiece.emblem == 0)
            chosenPiece.emblem = 1;
        else
            chosenPiece.emblem = 0;

        foreach (GameCore.Piece a in viablePieces)
        {
            if (chosenPiece.color == a.color &&
               chosenPiece.height == a.height &&
               chosenPiece.shape == a.shape &&
               chosenPiece.emblem == a.emblem)
            {
                chosenPiece.id = a.id;
                chosenPieceString = chosenPiece.id;
                return chosenPieceString;
            }
        }

        //Catch-All Default
        int rand = Random.Range(0, numOfAvailablePieces);
        chosenPieceString = availablePieces[rand].id;

        return chosenPieceString;
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

            //check the rows
            switch(tempList[i].row)
            {
                case 0:
                    if (CheckWinConditions(AITempBoard[0][0], AITempBoard[0][1], AITempBoard[0][2], AITempBoard[0][3]))
                    {
                        winningPosition = tempList[i].id;
                        return winningPosition;
                    }
                    break;
                case 1:
                    if (CheckWinConditions(AITempBoard[1][0], AITempBoard[1][1], AITempBoard[1][2], AITempBoard[1][3]))
                    {
                        winningPosition = tempList[i].id;
                        return winningPosition;
                    }
                    break;
                case 2:
                    if (CheckWinConditions(AITempBoard[2][0], AITempBoard[2][1], AITempBoard[2][2], AITempBoard[2][3]))
                    {
                        winningPosition = tempList[i].id;
                        return winningPosition;
                    }
                    break;
                case 3:
                    if (CheckWinConditions(AITempBoard[3][0], AITempBoard[3][1], AITempBoard[3][2], AITempBoard[3][3]))
                    {
                        winningPosition = tempList[i].id;
                        return winningPosition;
                    }
                    break;
            }

            // checks the rows
            switch (tempList[i].col)
            {
                case 0:
                    if (CheckWinConditions(AITempBoard[0][0], AITempBoard[1][0], AITempBoard[2][0], AITempBoard[3][0]))
                    {
                        winningPosition = tempList[i].id;
                        return winningPosition;
                    }
                    break;
                case 1:
                    if (CheckWinConditions(AITempBoard[0][1], AITempBoard[1][1], AITempBoard[2][1], AITempBoard[3][1]))
                    {
                        winningPosition = tempList[i].id;
                        return winningPosition;
                    }
                    break;
                case 2:
                    if (CheckWinConditions(AITempBoard[0][2], AITempBoard[1][2], AITempBoard[2][2], AITempBoard[3][2]))
                    {
                        winningPosition = tempList[i].id;
                        return winningPosition;
                    }
                    break;
                case 3:
                    if (CheckWinConditions(AITempBoard[0][3], AITempBoard[1][3], AITempBoard[2][3], AITempBoard[3][3]))
                    {
                        winningPosition = tempList[i].id;
                        return winningPosition;
                    }
                    break;
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
        if (a.color == 2 || b.color == 2 || c.color == 2 || d.color == 2 ||
            a.id == null || b.id == null || c.id == null || d.id == null ||
            a.id == ""   || b.id == ""   || c.id == ""   || d.id == "")
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
        else// if there arent any conditions met, that means that there isn't a winner
            return false;
    }

    private GameCore.BoardSpace ConvertPosition(string position, List<GameCore.BoardSpace> availableBoardSpaces)
    {
        GameCore.BoardSpace convertedBoardSpace = new GameCore.BoardSpace();
        string subStringPosition = position.Substring(12);

        foreach (GameCore.BoardSpace space in availableBoardSpaces)
            if (subStringPosition == space.id)
                convertedBoardSpace = space;

        return convertedBoardSpace;
    }

    private GameCore.Piece ConvertGamePiece(string gamePieceID, List<GameCore.Piece> availablePieces)
    {
        GameCore.Piece convertedGamePiece = new GameCore.Piece();

        foreach (GameCore.Piece piece in availablePieces)
            if (gamePieceID == piece.id)
                convertedGamePiece = piece;

        return convertedGamePiece;
    }
}