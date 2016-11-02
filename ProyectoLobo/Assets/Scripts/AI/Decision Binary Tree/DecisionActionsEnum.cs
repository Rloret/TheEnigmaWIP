using UnityEngine;
using System.Collections;

public class DecisionActionsEnum : Decision {

    public ActionsEnum.Actions valueDecision;
    public ActionsEnum.Actions valueTest;

    public override DecisionTreeNode GetBranch()
    {
        Debug.Log("nodeTrue es : " + nodeTrue + " nodeFalse es " + nodeFalse);
        if (valueDecision == valueTest)
        {
            return nodeTrue;

        }
        return nodeFalse;
    }

 }
