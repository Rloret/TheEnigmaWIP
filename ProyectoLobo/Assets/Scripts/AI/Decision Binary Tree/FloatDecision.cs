using UnityEngine;
using System.Collections;

public class FloatDecision : Decision {

    public float maxValue=10000;
    public float minvalue=0;
    public float testValue;

    public override DecisionTreeNode GetBranch()
    {
        Debug.Log(""+maxValue+ " >= " + testValue + " >= " + minvalue);
        if (maxValue >= testValue && testValue >= minvalue) {
            return nodeTrue;

        }
        return nodeFalse;
    }
}
