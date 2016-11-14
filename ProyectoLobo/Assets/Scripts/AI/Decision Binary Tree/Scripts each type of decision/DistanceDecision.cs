using UnityEngine;
using System.Collections;

public class DistanceDecision : Decision {

    public Transform MySelfTransform;
    public Transform TargetTransform;

    public int mindistance;


    public override DecisionTreeNode GetBranch()
    {
      //  Debug.Log("la dist es " + Vector3.Distance(MySelfTransform.position, TargetTransform.position));

        if (Vector3.Distance(MySelfTransform.position, TargetTransform.position) <= mindistance)
        {
            Debug.Log("true");
            return nodeTrue;
        }
        return nodeFalse;
    }
}
