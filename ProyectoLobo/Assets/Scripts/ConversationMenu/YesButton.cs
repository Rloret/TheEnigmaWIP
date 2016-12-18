using UnityEngine;
using System.Collections;

public class YesButton : ButtonAction {

	private GameObject targetIA;
	private GameObject player;

	private PlayerPersonality playerPers;

	private DecisionTreeReactionAfterInteraction reactionTree;

public override void Action()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerPers = player.GetComponent<PlayerPersonality> ();
		playerGroup = player.GetComponent<GroupScript> ();

		Debug.Log("YES");

		targetIA = menuController.GetTargetIA();

		if(	playerPers.interactionFromOtherCharacter== ActionsEnum.Actions.OFFER){
			playerPers.myObject = targetIA.GetComponent<AIPersonality> ().myObject;
			targetIA.GetComponent<AIPersonality> ().myObject = ObjectHandler.ObjectType.NONE;
			Debug.Log ("player:he cogido tu objeto");
		}

		else if(playerPers.interactionFromOtherCharacter== ActionsEnum.Actions.JOIN){
		//	playerGroup.addSingleMember (targetIA);
		//	targetIA.GetComponent<GroupScript> ().updateGroups (player);
			Debug.Log ("player:me uno a tu grupo bro");

		GroupScript myGroup = playerPers.gameObject.GetComponent<GroupScript>();
		GroupScript leadergroup = targetIA.GetComponent<GroupScript>();
			Debug.Log (targetIA);


		myGroup.groupLeader = targetIA ;
        myGroup.inGroup = true;
        myGroup.IAmTheLeader = false;
        myGroup.groupMembers.Clear();
        myGroup.groupMembers.AddRange(leadergroup.copyGroup());
		myGroup.addSingleMember(targetIA);
		leadergroup.updateGroups(playerPers.gameObject);
        leadergroup.makeLeader();
        

		}
			

		this.gameObject.transform.parent.gameObject.SetActive(false);
	}

}
