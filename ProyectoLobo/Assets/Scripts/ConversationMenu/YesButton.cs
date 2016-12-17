using UnityEngine;
using System.Collections;

public class YesButton : ButtonAction {

	private GameObject targetIA;
	private GameObject player;

	private PlayerPersonality playerPers;
	private GroupScript playerGroup;

	private DecisionTreeReactionAfterInteraction reactionTree;

/*public override void Action()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerPers = player.GetComponent<PlayerPersonality> ();
		playerGroup = player.GetComponent<GroupScript> ();

		Debug.Log("YES");

		targetIA = menuController.GetTargetIA();

		if(	playerPers.interactionFromOtherCharacter== ActionsEnum.Actions.OFFER){
			playerPers.myObject = targetIA.GetComponent<AIPersonality> ().myObject;
			targetIA.GetComponent<AIPersonality> ().myObject = ObjectHandler.ObjectType.NONE;
		}

		else if(playerPers.interactionFromOtherCharacter== ActionsEnum.Actions.JOIN){
			playerGroup.addSingleMember (targetIA);
			targetIA.GetComponent<GroupScript> ().updateGroups (player);

		}

		targetIA.GetComponent<AIPersonality>().interactionFromOtherCharacter = ActionsEnum.Actions.OFFER;
		Debug.Log("target es " + targetIA);

		reactionTree = targetIA.GetComponent<DecisionTreeReactionAfterInteraction>();
		if (reactionTree == null) {
			reactionTree=targetIA.AddComponent<DecisionTreeReactionAfterInteraction>();
		}
		Debug.Log("tree es " + reactionTree);

		reactionTree.target = targetIA;
		Debug.Log("target en tree es es " + reactionTree.target);


		reactionTree.StartTheDecision();


		this.gameObject.transform.parent.gameObject.SetActive(false);
	}
	*/
}
