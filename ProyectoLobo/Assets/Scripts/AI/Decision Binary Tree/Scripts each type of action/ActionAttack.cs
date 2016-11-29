using UnityEngine;
using System.Collections;

public class ActionAttack : Action {



    public override void DoAction()
    {

        base.visibiCone.IDecided = false;

        Debug.Log("voy a atacar y bajo confianza");
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
