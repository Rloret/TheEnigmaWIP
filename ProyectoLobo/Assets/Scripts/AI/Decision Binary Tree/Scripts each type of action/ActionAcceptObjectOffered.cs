using UnityEngine;
using System.Collections;

public class ActionAcceptObjectOffered : Action {

    public override void DoAction()
    {

		//increase confianza

        Debug.Log("aceptar objeto y aumentar confianza");
        Reaction.spawnReaction(ResponseController.responseEnum.OFFER, ResponseController.responseEnum.OK, this.gameObject);
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
