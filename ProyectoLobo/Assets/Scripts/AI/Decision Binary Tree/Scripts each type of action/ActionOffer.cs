using UnityEngine;
using System.Collections;

public class ActionOffer : Action {

    public override void DoAction()
    {
        Reaction.spawnReaction(ResponseController.responseEnum.OFFER, ResponseController.responseEnum.QUESTIONMARK, this.gameObject);
        //Code for attack
        //Placeholder
        Debug.Log("voy a ofrecer");

		if (this.gameObject.GetComponent<PersonalityBase> ().myObject != ObjectHandler.ObjectType.NONE) {
			
			Debug.Log("te doy un "+this.gameObject.GetComponent<ObjectHandler> ().currentObject);

			updateTrust (true, this.GetComponent<DecisionTreeCreator> ().target.GetComponent<PersonalityBase> (), this.gameObject.GetComponent<PersonalityBase> ().GetMyOwnIndex ());
		}

		if (this.gameObject.tag != "Player") {
			base.DestroyTrees ();
            if(this.gameObject.GetComponent<GroupScript>().groupLeader == this.gameObject && this.gameObject.GetComponent<GroupScript>().inGroup)
			Invoke ("EnableCone", 5f);
		}
    }

    private void EnableCone()
    {
		this.GetComponent<AgentPositionController> ().orientation += 180;

        GetComponent<VisibilityConeCycleIA>().enabled = true;
        base.visibiCone.IDecided = false;
		foreach (DecisionTreeNode n in this.gameObject.GetComponent<AIPersonality>().oldNodes) {
			DestroyImmediate (n);
		}

    }
}
