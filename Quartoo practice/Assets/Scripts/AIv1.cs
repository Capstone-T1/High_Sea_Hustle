using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIv1 : MonoBehaviour
{
    private GameController gameController;

    // Unity's Phase 1 for AI
    public void setAIpiece()
    {
        List<GamePiece> availablePieces = gameController.availablePieces;
        int numOfAvailablePieces = availablePieces.Count;

        System.Random rand = new System.Random();
        int option = rand.Next(numOfAvailablePieces);
        GamePiece chosenMove = availablePieces[option];

        gameController.SetSelectedPiece(chosenMove);
    }

    // Unity's Phase 2 for AI
    public void setAIposition()
    {
        Button[] availablePositions = gameController.buttonList;
        int numOfAvailablePositions = availablePositions.Length;


        System.Random rand = new System.Random();
        int option = rand.Next(numOfAvailablePositions);
        Button chosenPosition = availablePositions[option];

        gameController.SetRecentMove(chosenPosition);
    }
}
