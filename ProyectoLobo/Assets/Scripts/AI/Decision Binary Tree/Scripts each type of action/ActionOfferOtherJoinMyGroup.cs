using UnityEngine;
using System.Collections;

public class ActionOfferOtherJoinMyGroup : Action {

    public override void DoAction()
    {
        //Code for attack
        //Placeholder
        //Debug.Log("unete a mi grupo");
        Reaction.spawnReaction(ResponseController.responseEnum.GROUP, ResponseController.responseEnum.QUESTIONMARK, this.gameObject);

		updateTrust (true, this.GetComponent<DecisionTreeCreator> ().target.GetComponent<PersonalityBase> (),this.gameObject.GetComponent<PersonalityBase>().GetMyOwnIndex ());

        
		if (this.gameObject.tag != "Player") {
			base.DestroyTrees ();

			Invoke ("EnableCone", 5f);
		}
    }

    private void EnableCone()
    {
		this.GetComponent<AgentPositionController> ().orientation += 180;

		GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerMenuController> ().CloseJoinMenu ();

        GetComponent<VisibilityConeCycleIA>().enabled = true;
        base.visibiCone.IDecided = false;

		foreach (DecisionTreeNode n in this.gameObject.GetComponent<AIPersonality>().oldNodes) {
			DestroyImmediate (n);
		}

    }
}
