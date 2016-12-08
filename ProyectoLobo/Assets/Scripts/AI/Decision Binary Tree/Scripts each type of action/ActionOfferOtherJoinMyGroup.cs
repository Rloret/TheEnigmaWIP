using UnityEngine;
using System.Collections;

public class ActionOfferOtherJoinMyGroup : Action {

    public override void DoAction()
    {
        //Code for attack
        //Placeholder
        Debug.Log("unete a mi grupo");
        base.DestroyTrees();

        Invoke("EnableCone", 5f);
    }

    private void EnableCone()
    {
        Debug.Log("HAN PASADO 5!!!!!!!!!!!!");
        GetComponent<VisibilityConeCycleIA>().enabled = true;
        base.visibiCone.IDecided = false;

    }
}
