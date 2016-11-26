using UnityEngine;
using System.Collections;

public class ActionAttack : Action {



    public override void DoAction()
    {

        this.GetComponent<DecisionTarget>().IDecided = true;

            Debug.Log("voy a atacar y bajo confianza");

            // atack()
            // decrease friendship 

    }


}
