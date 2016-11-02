using UnityEngine;
using System.Collections;

public class ActionOffer : Action {

    public override void LateUpdate()
    {
        if (!DecisionTreeReactionAfterInteraction.DecisionCompleted)
        {

            if (!activated) { return; }

            //Code for attack
            //Placeholder
            Debug.Log("voy a ofrecer");
            DecisionTreeReactionAfterInteraction.DecisionCompleted = true;
        }

    }
}
