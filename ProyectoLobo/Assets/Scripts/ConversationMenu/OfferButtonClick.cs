using UnityEngine;
using System.Collections;

public class OfferButtonClick : ButtonAction {
    private GameObject targetIA;
    private DecisionTreeReactionAfterInteraction reactionTree;

    public override void Action()
    {
        Debug.Log("offerAction");
        targetIA = menuController.GetTargetIA();

        targetIA.GetComponent<AIPersonality>().interactionFromOtherCharacter = ActionsEnum.Actions.OFFER;
        Debug.Log("target es " + targetIA);

        reactionTree = targetIA.GetComponent<DecisionTreeReactionAfterInteraction>();

        if (reactionTree != null) {
			DestroyImmediate (reactionTree);
        }

		targetIA.gameObject.GetComponent<AIPersonality> ().oldNodes = targetIA.gameObject.GetComponents<DecisionTreeNode> ();
		foreach (DecisionTreeNode n in targetIA.gameObject.GetComponent<AIPersonality>().oldNodes) {
			DestroyImmediate (n);
		}

		reactionTree=targetIA.AddComponent<DecisionTreeReactionAfterInteraction>();

        Debug.Log("tree es " + reactionTree);

        reactionTree.target = targetIA;
        Debug.Log("target en tree es es " + reactionTree.target);


        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
