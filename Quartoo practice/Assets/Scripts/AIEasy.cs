using System.Collections.Generic;
using UnityEngine;

public class AIEasy
{
    private GameCore gameCore = new GameCore();
    private GameCore.Piece pieceAIPlaced;

    void Awake()
    {
        pieceAIPlaced = ConvertGamePiece("AI", gameCore.availablePieces);
    }

    public string ChooseLocation(GameCore.Piece[][] newGameBoard, List<GameCore.BoardSpace> availableBoardSpaces, List<GameCore.BoardSpace> usedBoardSpaces, List<GameCore.Piece> availablePieces, string pieceGivenToAIID, string recentMoveID)
    {
        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();
        Debug.Log("Debug: ChooseLocation() starts");

        GameCore.BoardSpace chosenLocation = ConvertPosition(recentMoveID, usedBoardSpaces);
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

                stopWatch.Stop();
                Debug.Log("Debug: ChooseLocation - " + stopWatch.Elapsed);

                return chosenLocationString;
            }
        }

        //When AI goes second, choosing very first location
        if (numOfAvailablePositions == 16)
        {
            int rand = Random.Range(0, numOfAvailablePositions);
            chosenLocationString = availableBoardSpaces[rand].id;

            stopWatch.Stop();
            Debug.Log("Debug: ChooseLocation - " + stopWatch.Elapsed);

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

                stopWatch.Stop();
                Debug.Log("Debug: ChooseLocation - " + stopWatch.Elapsed);

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

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseLocation - " + stopWatch.Elapsed);

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

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseLocation - " + stopWatch.Elapsed);

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

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseLocation - " + stopWatch.Elapsed);

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

        stopWatch.Stop();
        Debug.Log("Debug: ChooseLocation - " + stopWatch.Elapsed);
        return chosenLocationString;
    }

    public string ChooseGamePiece(List<GameCore.Piece> availablePieces)
    {
        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();
        Debug.Log("Debug: ChooseGamePiece() starts");

        GameCore.Piece chosenPiece = pieceAIPlaced;
        List<GameCore.Piece> viablePieces = new List<GameCore.Piece>();
        GameCore.Piece[][] AITempBoard = gameCore.GetGameBoard();

        foreach (GameCore.Piece a in availablePieces)
            viablePieces.Add(a);

        string chosenPieceString;

        //Passes off Losing Piece to Player
        int randomTurnDecision = Random.Range(4, 10);
        if (availablePieces.Count <= randomTurnDecision)
        {
            int[] lossCondition = new int[4];

            for (int i = 0; i < AITempBoard.Length; i++)
            {
                GameCore.Piece[] result = AITempBoard[i];
                lossCondition = CheckLossConditions(result[0], result[1], result[2], result[3]);
                if (lossCondition[0] != 2 || lossCondition[1] != 2 || lossCondition[2] != 2 || lossCondition[3] != 2)
                {
                    if (lossCondition[0] == 0)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].color == 0)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                    else if (lossCondition[0] == 1)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].color == 1)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                    if (lossCondition[1] == 0)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].height == 0)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                    else if (lossCondition[1] == 1)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].height == 1)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                    if (lossCondition[2] == 0)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].shape == 0)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                    else if (lossCondition[2] == 1)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].shape == 1)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                    if (lossCondition[3] == 0)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].emblem == 0)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                    else if (lossCondition[3] == 1)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].emblem == 1)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                }
            }

            for (int i = 0; i < AITempBoard.Length; i++)
            {
                GameCore.Piece[] result = new GameCore.Piece[4];
                for (int j = 0; j < 4; j++)
                    result[j] = AITempBoard[j][i];
                lossCondition = CheckLossConditions(result[0], result[1], result[2], result[3]);
                if (lossCondition[0] != 2 || lossCondition[1] != 2 || lossCondition[2] != 2 || lossCondition[3] != 2)
                {
                    if (lossCondition[0] == 0)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].color == 0)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                    else if (lossCondition[0] == 1)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].color == 1)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                    if (lossCondition[1] == 0)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].height == 0)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                    else if (lossCondition[1] == 1)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].height == 1)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                    if (lossCondition[2] == 0)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].shape == 0)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                    else if (lossCondition[2] == 1)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].shape == 1)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                    if (lossCondition[3] == 0)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].emblem == 0)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                    else if (lossCondition[3] == 1)
                    {
                        for (int a = 0; a < viablePieces.Count; a++)
                            if (viablePieces[a].emblem == 1)
                            {
                                chosenPieceString = viablePieces[a].id;

                                stopWatch.Stop();
                                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                                return chosenPieceString;
                            }
                    }
                }
            }

            lossCondition = CheckLossConditions(AITempBoard[0][0], AITempBoard[1][1], AITempBoard[2][2], AITempBoard[3][3]);
            if (lossCondition[0] != 2 || lossCondition[1] != 2 || lossCondition[2] != 2 || lossCondition[3] != 2)
            {
                if (lossCondition[0] == 0)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].color == 0)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
                else if (lossCondition[0] == 1)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].color == 1)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
                if (lossCondition[1] == 0)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].height == 0)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
                else if (lossCondition[1] == 1)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].height == 1)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
                if (lossCondition[2] == 0)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].shape == 0)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
                else if (lossCondition[2] == 1)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].shape == 1)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
                if (lossCondition[3] == 0)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].emblem == 0)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
                else if (lossCondition[3] == 1)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].emblem == 1)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
            }

            lossCondition = CheckLossConditions(AITempBoard[0][3], AITempBoard[1][2], AITempBoard[2][1], AITempBoard[3][0]);
            if (lossCondition[0] != 2 || lossCondition[1] != 2 || lossCondition[2] != 2 || lossCondition[3] != 2)
            {
                if (lossCondition[0] == 0)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].color == 0)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
                else if (lossCondition[0] == 1)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].color == 1)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
                if (lossCondition[1] == 0)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].height == 0)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
                else if (lossCondition[1] == 1)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].height == 1)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
                if (lossCondition[2] == 0)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].shape == 0)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
                else if (lossCondition[2] == 1)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].shape == 1)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
                if (lossCondition[3] == 0)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].emblem == 0)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
                else if (lossCondition[3] == 1)
                {
                    for (int a = 0; a < viablePieces.Count; a++)
                        if (viablePieces[a].emblem == 1)
                        {
                            chosenPieceString = viablePieces[a].id;

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                            return chosenPieceString;
                        }
                }
            }
        }

            if (viablePieces.Count == 0 || availablePieces.Count == 16)
        {
            int option = Random.Range(0, availablePieces.Count);
            chosenPieceString = availablePieces[option].id;

            stopWatch.Stop();
            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

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

                    stopWatch.Stop();
                    Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                    return chosenPieceString;
                }
            }

            chosenPiece = pieceAIPlaced;
            tempFirstOption = (tempFirstOption + 1) % 4;

        } while (tempFirstOption != firstOption);

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

                        stopWatch.Stop();
                        Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

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

                            stopWatch.Stop();
                            Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

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

                stopWatch.Stop();
                Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

                return chosenPieceString;
            }
        }

        //Catch-All Default
        int rand = Random.Range(0, availablePieces.Count);
        chosenPieceString = availablePieces[rand].id;

        stopWatch.Stop();
        Debug.Log("Debug: ChooseGamePiece() - " + stopWatch.Elapsed);

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

        for (int i = 0; i < (tempList.Count - 1); i++)
        {
            AITempBoard[tempList[i].row][tempList[i].col] = givenPiece;

            //check the rows
            switch (tempList[i].row)
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

    private int[] CheckLossConditions(GameCore.Piece a, GameCore.Piece b, GameCore.Piece c, GameCore.Piece d)
    {
        int[] lossCondition = new int[4] { 2, 2, 2, 2 };
        /*
          Possible Results:
          None = lossCondition {2,2,2,2}
          Gold - lossCondition[0] = 0
          Silver - lossCondition[0] = 1
          Short - lossCondition[1] = 0
          Tall - lossCondition[1] = 1
          Round - lossCondition[2] = 0
          Triangle - lossCondition[2] = 1
          No Emblem - lossCondition[3] = 0
          Emblem - lossCondition[3] = 1
        */

        //Checks if at least three of them have a piece at location
        if ((a.color != 2 && b.color != 2 && c.color != 2) ||
            (d.color != 2 && b.color != 2 && c.color != 2) ||
            (a.color != 2 && b.color != 2 && d.color != 2) ||
            (a.color != 2 && d.color != 2 && c.color != 2))
        {
            //Checks Color (Checks 3/4 share condition && 1 of those 3 is not empty && at least 1/4 is empty)
            if (((a.color == b.color && a.color == c.color) && (a.color != 2 || b.color != 2 || c.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)) ||
                ((b.color == c.color && b.color == d.color) && (d.color != 2 || b.color != 2 || c.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)) ||
                ((a.color == b.color && a.color == d.color) && (a.color != 2 || b.color != 2 || d.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)) ||
                ((a.color == c.color && a.color == d.color) && (a.color != 2 || d.color != 2 || c.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)))
            {
                //if Gold
                if ((a.color == 0 && b.color == 0 && c.color == 0) ||
                   (b.color == 0 && c.color == 0 && d.color == 0) ||
                   (a.color == 0 && b.color == 0 && d.color == 0) ||
                   (a.color == 0 && d.color == 0 && c.color == 0))
                {
                    Debug.Log("Possible loss by Gold on Board");
                    lossCondition[0] = 0;
                }
                //If Silver
                else if ((a.color == 1 && b.color == 1 && c.color == 1) ||
                         (b.color == 1 && c.color == 1 && d.color == 1) ||
                         (a.color == 1 && b.color == 1 && d.color == 1) ||
                         (a.color == 1 && d.color == 1 && c.color == 1))
                {
                    Debug.Log("Possible loss by Silver on Board");
                    lossCondition[0] = 1;
                }
            }

            //Checks Height
            if (((a.height == b.height && a.height == c.height) && (a.color != 2 || b.color != 2 || c.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)) ||
                ((b.height == c.height && b.height == d.height) && (d.color != 2 || b.color != 2 || c.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)) ||
                ((a.height == b.height && a.height == d.height) && (a.color != 2 || b.color != 2 || d.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)) ||
                ((a.height == c.height && a.height == d.height) && (a.color != 2 || d.color != 2 || c.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)))

            {
                //If Short
                if ((a.height == 0 && b.height == 0 && c.height == 0) ||
                   (b.height == 0 && c.height == 0 && d.height == 0) ||
                   (a.height == 0 && b.height == 0 && d.height == 0) ||
                   (a.height == 0 && d.height == 0 && c.height == 0))
                {
                    Debug.Log("Possible loss by Short on Board");
                    lossCondition[1] = 0;
                }
                //If Tall
                else if ((a.height == 1 && b.height == 1 && c.height == 1) ||
                         (b.height == 1 && c.height == 1 && d.height == 1) ||
                         (a.height == 1 && b.height == 1 && d.height == 1) ||
                         (a.height == 1 && d.height == 1 && c.height == 1))
                {
                    Debug.Log("Possible loss by Tall on Board");
                    lossCondition[1] = 1;
                }
            }

            //Checks Shape
            if (((a.shape == b.shape && a.shape == c.shape) && (a.color != 2 || b.color != 2 || c.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)) ||
                ((b.shape == c.shape && b.shape == d.shape) && (d.color != 2 || b.color != 2 || c.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)) ||
                ((a.shape == b.shape && a.shape == d.shape) && (a.color != 2 || b.color != 2 || d.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)) ||
                ((a.shape == c.shape && a.shape == d.shape) && (a.color != 2 || d.color != 2 || c.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)))
            {
                //If Round
                if ((a.shape == 0 && b.shape == 0 && c.shape == 0) ||
                   (b.shape == 0 && c.shape == 0 && d.shape == 0) ||
                   (a.shape == 0 && b.shape == 0 && d.shape == 0) ||
                   (a.shape == 0 && d.shape == 0 && c.shape == 0))
                {
                    Debug.Log("Possible loss by Round on Board");
                    lossCondition[2] = 0;
                }
                //If Triangle
                else if ((a.shape == 1 && b.shape == 1 && c.shape == 1) ||
                         (b.shape == 1 && c.shape == 1 && d.shape == 1) ||
                         (a.shape == 1 && b.shape == 1 && d.shape == 1) ||
                         (a.shape == 1 && d.shape == 1 && c.shape == 1))
                {
                    Debug.Log("Possible loss by Triangle on Board");
                    lossCondition[2] = 1;
                }
            }

            //Checks Emblem
            if (((a.emblem == b.emblem && a.emblem == c.emblem) && (a.color != 2 || b.color != 2 || c.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)) ||
                ((b.emblem == c.emblem && b.emblem == d.emblem) && (d.color != 2 || b.color != 2 || c.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)) ||
                ((a.emblem == b.emblem && a.emblem == d.emblem) && (a.color != 2 || b.color != 2 || d.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)) ||
                ((a.emblem == c.emblem && a.emblem == d.emblem) && (a.color != 2 || d.color != 2 || c.color != 2) && !(a.color != 2 && b.color != 2 && c.color != 2 && d.color != 2)))
            {
                //If No Emblem
                if ((a.emblem == 0 && b.emblem == 0 && c.emblem == 0) ||
                   (b.emblem == 0 && c.emblem == 0 && d.emblem == 0) ||
                   (a.emblem == 0 && b.emblem == 0 && d.emblem == 0) ||
                   (a.emblem == 0 && d.emblem == 0 && c.emblem == 0))
                {
                    Debug.Log("Possible loss by No Emblem on Board");
                    lossCondition[3] = 0;
                }
                //If Emblem
                else if ((a.emblem == 1 && b.emblem == 1 && c.emblem == 1) ||
                         (b.emblem == 1 && c.emblem == 1 && d.emblem == 1) ||
                         (a.emblem == 1 && b.emblem == 1 && d.emblem == 1) ||
                         (a.emblem == 1 && d.emblem == 1 && c.emblem == 1))
                {
                    Debug.Log("Possible loss by Emblem on Board");
                    lossCondition[3] = 1;
                }
            }
        }
        // if there arent any conditions met, that means that there isn't a winner
        return lossCondition;
    }

    private bool CheckWinConditions(GameCore.Piece a, GameCore.Piece b, GameCore.Piece c, GameCore.Piece d)
    {
        if (a.color == 2 || b.color == 2 || c.color == 2 || d.color == 2 ||
            a.id == null || b.id == null || c.id == null || d.id == null ||
            a.id == "" || b.id == "" || c.id == "" || d.id == "")
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
