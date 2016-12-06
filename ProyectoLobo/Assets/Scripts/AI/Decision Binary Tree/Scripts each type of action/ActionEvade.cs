using UnityEngine;
using System.Collections;

public class ActionEvade : Action {
  

    public override void DoAction()
    {
        //Code for attack
        //Placeholder
        Debug.Log("voy a huir y bajo confianza");

        // atack()
        // decrease friendship 
        string[] behaviours = { "Flee", "AvoidWall", "LookWhereYouAreGoing" };
        float[] weightedBehavs = { 0.7f, 1, 1 };
        GameObject.FindGameObjectWithTag("GameController").GetComponent<BehaviourAdder>().addBehavioursOver(this.gameObject, this.GetComponent<DecisionTreeCreator>().target, behaviours, weightedBehavs);

    }
}
