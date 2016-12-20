using UnityEngine;
using System.Collections;

public class ActionNothing : Action {

    public override void DoAction()
    {

        //Code for attack
        //Placeholder
        Debug.Log("nada: paso de todo, no me interesa");
		if (this.gameObject.tag != "Player") {
			string[] behaviours = { "Wander", "LookWhereYouAreGoing", "AvoidWall" };
			float[] weightedBehavs = { 0.8f, 0.1f, 1 };
            GameObject target = this.GetComponent<DecisionTreeCreator>().target;
            GameObject[] targets = { target, target, target };
            GameObject.FindGameObjectWithTag ("GameController").GetComponent<BehaviourAdder> ().addBehavioursOver (this.gameObject,targets , behaviours, weightedBehavs);

			base.DestroyTrees ();

            if (this.gameObject.GetComponent<GroupScript>().groupLeader == this.gameObject && this.gameObject.GetComponent<GroupScript>().inGroup)
                Invoke ("EnableCone",1f);
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
