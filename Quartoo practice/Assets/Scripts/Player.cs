using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName = "";
    public bool isFirstPlayer = false;
    public enum Avatar
    {
        malePirate,
        femalePirate,
        maleNavy,
        femaleNavy,
    }
}
