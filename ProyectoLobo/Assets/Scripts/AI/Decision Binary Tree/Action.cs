using UnityEngine;
using System.Collections;

public class Action : DecisionTreeNode {

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

    public virtual void DoAction() {
        Debug.Log("doAction de padre action");
        return; }

}
