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
        if (reactionTree == null) {
            reactionTree=targetIA.AddComponent<DecisionTreeReactionAfterInteraction>();
        }
        Debug.Log("tree es " + reactionTree);

        reactionTree.target = targetIA;
        Debug.Log("target en tree es es " + reactionTree.target);


        reactionTree.StartTheDecision();


        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
