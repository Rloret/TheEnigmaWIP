using UnityEngine;
using System.Collections;

public class ActionNothing : Action {

    public override void DoAction()
    {
        base.visibiCone.IDecided = false;

        //Code for attack
        //Placeholder
        Debug.Log("nada: paso de todo, no me interesa");

        base.DestroyTrees();

        Invoke("EnableCone", 10f);
    }

    private void EnableCone()
    {
        GetComponent<VisibilityConeCycleIA>().enabled = true;

    }
}
