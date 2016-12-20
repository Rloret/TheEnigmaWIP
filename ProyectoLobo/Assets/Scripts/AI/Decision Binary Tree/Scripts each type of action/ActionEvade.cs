using UnityEngine;
using System.Collections;

public class ActionEvade : Action {
  

    public override void DoAction()
    {

       // Debug.Log("voy a huir y bajo confianza. Soy "+this.gameObject.name);

        Reaction.spawnReaction(ResponseController.responseEnum.EVADE, ResponseController.responseEnum.EVADE, this.gameObject);
        GameObject target = this.GetComponent<DecisionTreeCreator>().target;
        string[] behaviours = { "Evade", "AvoidWall", "LookWhereYouAreGoing" };
        float[] weightedBehavs = { 0.7f, 1, 1 };
        GameObject[] targets = { target, target, target };
        GetComponent<VisibilityConeCycleIA>().enabled = false;
        GetComponent<GroupScript>().leaveGroup(this.gameObject);
        base.visibiCone.IDecided = false;

        base.DestroyTrees();
      
        GameObject.FindGameObjectWithTag("GameController").GetComponent<BehaviourAdder>().addBehavioursOver(this.gameObject,targets , behaviours, weightedBehavs);
        this.GetComponent<GroupScript>().ExitGroup();
            Invoke("EnableCone", 10f);
    }

    private void EnableCone()
    {
        GetComponent<VisibilityConeCycleIA>().enabled = true;

		foreach (DecisionTreeNode n in this.gameObject.GetComponent<AIPersonality>().oldNodes) {
			DestroyImmediate (n);
		}
    }
}
