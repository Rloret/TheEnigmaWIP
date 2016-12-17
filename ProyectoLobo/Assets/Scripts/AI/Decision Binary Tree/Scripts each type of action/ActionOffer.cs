using UnityEngine;
using System.Collections;

public class ActionOffer : Action {

    public override void DoAction()
    {
        Reaction.spawnReaction(ResponseController.responseEnum.OFFER, ResponseController.responseEnum.QUESTIONMARK, this.gameObject);
        //Code for attack
        //Placeholder
        Debug.Log("voy a ofrecer");
		if (this.gameObject.tag != "Player") {
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
