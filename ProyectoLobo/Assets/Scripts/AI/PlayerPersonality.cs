using UnityEngine;
using System.Collections;

public class PlayerPersonality : PersonalityBase {

    public void configurePlayer()
    {
        int numberOfAgents = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().numberOfIAs;
        TrustInOthers = new int[numberOfAgents];
        initializeTrustInOthers(numberOfAgents);
        health = 100;
        attack = 10;

        MyOwnIndex = numberOfAgents - 1;
    }
}
