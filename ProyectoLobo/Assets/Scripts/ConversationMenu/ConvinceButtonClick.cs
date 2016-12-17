using UnityEngine;
using System.Collections;

public class ConvinceButtonClick : ButtonAction {
    //convince= join
    private GameObject targetIA;
    private DecisionTreeReactionAfterInteraction reactionTree;


    public override void Action()
    {
        Debug.Log("convinceAction");

        targetIA = menuController.GetTargetIA();

        targetIA.GetComponent<AIPersonality>().interactionFromOtherCharacter = ActionsEnum.Actions.JOIN;

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
