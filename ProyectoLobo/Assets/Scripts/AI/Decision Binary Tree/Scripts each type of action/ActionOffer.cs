using UnityEngine;
using System.Collections;

public class ActionOffer : Action {

    public override void DoAction()
    {

        //Code for attack
        //Placeholder
        Debug.Log("voy a ofrecer");
        base.DestroyTrees();

        Invoke("EnableCone", 10f);
    }

    private void EnableCone()
    {
        GetComponent<VisibilityConeCycleIA>().enabled = true;
        base.visibiCone.IDecided = false;

    }
}
