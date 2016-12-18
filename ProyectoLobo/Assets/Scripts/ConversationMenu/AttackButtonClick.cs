using UnityEngine;
using System.Collections;

public class AttackButtonClick : ButtonAction {

    private GameObject targetIA;
    private DecisionTreeReactionAfterInteraction reactionTree;
    

    public override void Action()
    {
        Debug.Log("attackAction");

        targetIA = menuController.GetTargetIA();
        targetIA.GetComponent<AIPersonality>().interactionFromOtherCharacter = ActionsEnum.Actions.ATTACK;

		reactionTree = targetIA.GetComponent<DecisionTreeReactionAfterInteraction>();

		if (reactionTree != null) {
			DestroyImmediate (reactionTree);
		}

		targetIA.gameObject.GetComponent<AIPersonality> ().oldNodes = targetIA.gameObject.GetComponents<DecisionTreeNode> ();

		foreach (DecisionTreeNode n in targetIA.gameObject.GetComponent<AIPersonality>().oldNodes) {
			DestroyImmediate (n);
		}

		reactionTree=targetIA.AddComponent<DecisionTreeReactionAfterInteraction>();
        reactionTree.target = targetIA;

        this.gameObject.transform.parent.gameObject.SetActive(false);

    }
}
