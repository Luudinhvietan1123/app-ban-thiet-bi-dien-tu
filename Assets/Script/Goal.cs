using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // the player who gets a point for this goal, 1 or 2
    public int Player = 1;
    // the Scorekeeper
    public Scorekeeper scorekeeper;
    public void GetPoint()
    {
        scorekeeper.AddScore(Player);
    }
}
