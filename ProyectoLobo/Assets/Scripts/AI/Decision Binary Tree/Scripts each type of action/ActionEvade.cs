using UnityEngine;
using System.Collections;

public class ActionEvade : Action {
    public override void LateUpdate()
    {
        if (!DecisionTreeReactionAfterInteraction.DecisionCompleted)
        {
            if (!activated) { return; }

            //Code for attack
            //Placeholder
            Debug.Log("voy a huir");

            // atack()
            // decrease friendship 
            DecisionTreeReactionAfterInteraction.DecisionCompleted = true;


        }

    }
}
