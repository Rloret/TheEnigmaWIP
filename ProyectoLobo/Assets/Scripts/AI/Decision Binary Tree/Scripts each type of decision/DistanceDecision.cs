using UnityEngine;
using System.Collections;

public class DistanceDecision : Decision {

    public Transform MySelfTransform;
    public Transform TargetTransform;

    public int mindistance;


    public override DecisionTreeNode GetBranch()
    {

        if (Vector3.Distance(MySelfTransform.position, TargetTransform.position) < mindistance)
        {
            return nodeTrue;
        }
        return nodeFalse;
    }
}
