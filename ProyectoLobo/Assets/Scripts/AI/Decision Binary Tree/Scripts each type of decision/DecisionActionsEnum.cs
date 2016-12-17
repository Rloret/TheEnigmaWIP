using UnityEngine;
using System.Collections;

public class DecisionActionsEnum : Decision {

    public ActionsEnum.Actions valueDecision;
	public PersonalityBase personality;

    public override DecisionTreeNode GetBranch()
    {
       // Debug.Log("nodeTrue es : " + nodeTrue + " nodeFalse es " + nodeFalse);
        if (valueDecision == personality.interactionFromOtherCharacter)
        {
            return nodeTrue;

        }
        return nodeFalse;
    }

 }
