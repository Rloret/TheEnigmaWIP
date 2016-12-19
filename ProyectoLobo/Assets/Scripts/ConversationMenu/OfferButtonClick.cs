using UnityEngine;
using System.Collections;

public class OfferButtonClick : ButtonAction {
    private GameObject targetIA;
    private DecisionTreeReactionAfterInteraction reactionTree;

    public override void Action()
    {
        Debug.Log("offerAction");
        targetIA = menuController.GetTargetIA();
		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		targetIA = menuController.GetTargetIA();

		PersonalityBase targetPers = targetIA.GetComponent<AIPersonality> ();

		targetPers.interactionFromOtherCharacter = ActionsEnum.Actions.OFFER;

		if (player.GetComponent<PlayerPersonality> ().myObject != ObjectHandler.ObjectType.NONE) {
			Debug.Log("te doy un "+player.gameObject.GetComponent<ObjectHandler> ().currentObject);

			updateTrust (true, targetPers, player.GetComponent<PersonalityBase> ().GetMyOwnIndex ());

		}


		reactionTree = targetIA.GetComponent<DecisionTreeReactionAfterInteraction>();

        if (reactionTree != null) {
			DestroyImmediate (reactionTree);
        }

		targetIA.gameObject.GetComponent<AIPersonality> ().oldNodes = targetIA.gameObject.GetComponents<DecisionTreeNode> ();
		foreach (DecisionTreeNode n in targetIA.gameObject.GetComponent<AIPersonality>().oldNodes) {
			DestroyImmediate (n);
		}

		reactionTree=targetIA.AddComponent<DecisionTreeReactionAfterInteraction>();

		reactionTree.target = GameObject.FindGameObjectWithTag("Player");

        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

	protected void updateTrust(bool increase, PersonalityBase pers, int index){
		//	Debug.Log ("se esta actualizand la confianza de : " + pers.gameObject.name + " indice: " + index);

		if (increase) {
			pers.TrustInOthers [index] += 1;
		} else {
			pers.TrustInOthers [index] -= 1;
		}
	}
}
