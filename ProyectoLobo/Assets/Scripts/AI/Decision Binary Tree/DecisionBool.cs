using UnityEngine;
using System.Collections;

public class DecisionBool : Decision
{
    public bool valueDecision;
    public bool valueTest;

    public override Action GetBranch()
    {
        if (valueDecision == valueTest) {
            return nodeTrue;

        }
        return nodeFalse;
    }
}
