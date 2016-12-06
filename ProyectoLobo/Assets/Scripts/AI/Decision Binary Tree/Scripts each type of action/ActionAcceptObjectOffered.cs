using UnityEngine;
using System.Collections;

public class ActionAcceptObjectOffered : Action {

    public override void DoAction()
    {
        //Code for attack
        //Placeholder
        Debug.Log("aceptar objeto y aumentar confianza");
        base.visibiCone.IDecided = false;

        base.DestroyTrees();


        // atack()
        // decrease friendship 

        Invoke("EnableCone", 10f);
    }

    private void EnableCone()
    {
        GetComponent<VisibilityConeCycleIA>().enabled = true;

    }
}
