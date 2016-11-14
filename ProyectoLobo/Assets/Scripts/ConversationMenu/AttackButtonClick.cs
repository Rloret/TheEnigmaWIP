using UnityEngine;
using System.Collections;

public class AttackButtonClick : ButtonAction {

    private GameObject targetIA;
    private DecisionTreeReactionAfterInteraction reactionTree;

    public override void Action()
    {
        Debug.Log("attackAction");

        targetIA = menuController.GetTargetIA();
        Debug.Log("target es " + targetIA);

        targetIA.GetComponent<AIPersonality>().interactionFromOtherCharacter = ActionsEnum.Actions.ATTACK;

        reactionTree = targetIA.GetComponent<DecisionTreeReactionAfterInteraction>();
        if (reactionTree == null)
        {
            reactionTree = targetIA.AddComponent<DecisionTreeReactionAfterInteraction>();
        }

        Debug.Log("tree es " + reactionTree);

        reactionTree.target = targetIA;
        reactionTree.StartTheDecision();

        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
