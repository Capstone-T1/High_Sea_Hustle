using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIv2 : MonoBehaviour
{
    //Four Steps to MCTS:
    //Selection - Use UCT to find best Node
    double UCTvalue (int iWin, double totalVisit, int iVisit)
    {
        if (iVisit == 0)
        {
            return int.MaxValue;
        }

        return ((iWin/iVisit)+2*(System.Math.Sqrt(System.Math.Log(totalVisit)/iVisit)));
    }
    //Expansion - expands the tree to all possible outputs
    //Simulation - Runs simulated game
    //Backpropagation - once simulation is complete, updates scores for all nodes touched







    //Unity Generated Code Below

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
