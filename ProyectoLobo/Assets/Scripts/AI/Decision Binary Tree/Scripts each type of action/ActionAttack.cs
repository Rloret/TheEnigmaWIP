using UnityEngine;
using System.Collections;

public class ActionAttack : Action {

    public override void LateUpdate()
    {
        if (!activated) { return; }

        //Code for attack
        //Placeholder
        Debug.Log("voy a atacar");
    }
}
