using UnityEngine;
using System.Collections;

public class Action : DecisionTreeNode {

    public VisibilityConeCycleIA visibiCone;

    private void Awake() {
        visibiCone = this.GetComponent<VisibilityConeCycleIA>();

    }
    public override DecisionTreeNode MakeDecision()
    {

        Debug.Log("estoy en makedecision() de action");
        return this;
    }


    public virtual void DoAction() {
        Debug.Log("doAction de padre action");
        return;
    }

}
