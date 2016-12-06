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

    protected void DestroyTrees() {
        if (this.GetComponent<DecisionTreeISeeSomeoneWhatShouldIDo>() != null)Destroy( this.GetComponent<DecisionTreeISeeSomeoneWhatShouldIDo>());
        if (this.GetComponent<DecisionTreeReactionAfterInteraction>() != null) Destroy(this.GetComponent<DecisionTreeReactionAfterInteraction>());

    }

}
