using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIv1 : MonoBehaviour
{
    //Unity's Phase 1 for AI
    public string chooseGamePiece(List<GameCore.Piece> availablePieces)
    {
        int numOfAvailablePieces = availablePieces.Count;
        int option = Random.Range(0, numOfAvailablePieces);
        string chosenMove = availablePieces[option].id;

        return chosenMove;
    }

    // Unity's Phase 2 for AI
    public string choosePosition(List<GameCore.BoardSpace> availableBoardSpaces)
    {
        int numOfAvailablePositions = availableBoardSpaces.Count;
        int option = Random.Range(0, numOfAvailablePositions);
        string chosenPosition = availableBoardSpaces[option].id;

        return chosenPosition;
    }
}