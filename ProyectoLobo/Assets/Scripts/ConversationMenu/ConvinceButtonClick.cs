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

        if (reactionTree == null)
        {
            reactionTree = targetIA.AddComponent<DecisionTreeReactionAfterInteraction>();
        }

        reactionTree.target = targetIA;
        reactionTree.StartTheDecision();

        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
