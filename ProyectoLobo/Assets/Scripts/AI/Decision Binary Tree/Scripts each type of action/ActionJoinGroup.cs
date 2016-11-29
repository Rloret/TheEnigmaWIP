using UnityEngine;
using System.Collections;

public class ActionJoinGroup : Action {
    public override void DoAction()
    {
        base.visibiCone.IDecided = false;

        Debug.Log("me uno a tu grupo");
        string[] behaviours = { "Pursue", "AvoidWall", "Face" };
        float[] weightedBehavs = { 0.7f, 1, 1 };
        GameObject.FindGameObjectWithTag("GameController").GetComponent<OnObjectClickedController>().addBehavioursOver(this.gameObject, this.GetComponent<DecisionTreeCreator>().target, behaviours, weightedBehavs);


    }
}
