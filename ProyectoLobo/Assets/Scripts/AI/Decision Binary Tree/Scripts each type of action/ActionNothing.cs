﻿using UnityEngine;
using System.Collections;

public class ActionNothing : Action {

    public override void DoAction()
    {

        //Code for attack
        //Placeholder
        Debug.Log("nada: paso de todo, no me interesa");
		if (this.gameObject.tag != "Player") {
			
			Reaction.spawnReaction (ResponseController.responseEnum.NOTOK, ResponseController.responseEnum.NOTOK, this.gameObject);
			string[] behaviours = { "Wander", "LookWhereYouAreGoing", "AvoidWall" };
			float[] weightedBehavs = { 0.8f, 0.1f, 1 };
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<BehaviourAdder> ().addBehavioursOver (this.gameObject, this.GetComponent<DecisionTreeCreator> ().target, behaviours, weightedBehavs);

			base.DestroyTrees ();

			Invoke ("EnableCone", 10f);
		}
    }

    private void EnableCone()
    {
        GetComponent<VisibilityConeCycleIA>().enabled = true;
        base.visibiCone.IDecided = false;

		foreach (DecisionTreeNode n in this.gameObject.GetComponent<AIPersonality>().oldNodes) {
			DestroyImmediate (n);
		}


    }
}
