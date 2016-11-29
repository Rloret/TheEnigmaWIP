using UnityEngine;
using System.Collections;

public class DistanceDecision : Decision {

    public Transform MySelfTransform;
    public Transform TargetTransform;

    public int mindistance;


    public override DecisionTreeNode GetBranch()
    {

        if (Vector3.Distance(MySelfTransform.position, TargetTransform.position) <= mindistance)
        {
            Debug.Log("yo " + MySelfTransform + "target " + TargetTransform);
            Debug.Log("la distancoia es " + Vector3.Distance(MySelfTransform.position, TargetTransform.position));
            return nodeTrue;
        }
        return nodeFalse;
    }
}
