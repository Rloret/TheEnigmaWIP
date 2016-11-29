using UnityEngine;
using System.Collections;

public class ActionOfferOtherJoinMyGroup : Action {

    public override void DoAction()
    {
        base.visibiCone.IDecided = false;

        //Code for attack
        //Placeholder
        Debug.Log("unete a mi grupo");
        base.DestroyTrees();

        Invoke("EnableCone", 10f);
    }

    private void EnableCone()
    {
        GetComponent<VisibilityConeCycleIA>().enabled = true;

    }
}
