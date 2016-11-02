using UnityEngine;
using System.Collections;

public class Action : DecisionTreeNode {

    public bool activated = false;

    public override DecisionTreeNode MakeDecision()
    {

        Debug.Log("estoy en makedecision() de action");
        return this;
    }

    public virtual void LateUpdate() {
        if (!activated) {
            return;
        }

        //part which should be overriden in each action class
    }

}
