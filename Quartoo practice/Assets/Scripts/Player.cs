using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace High_Sea_Hustle
{
    public class Player : MonoBehaviourPun
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
}
