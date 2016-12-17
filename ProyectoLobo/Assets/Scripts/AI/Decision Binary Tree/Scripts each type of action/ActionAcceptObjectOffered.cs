using UnityEngine;
using System.Collections;

public class ActionAcceptObjectOffered : Action {

    public override void DoAction()
    {
        //Code for attack
        //Placeholder
        Debug.Log("aceptar objeto y aumentar confianza");
        Reaction.spawnReaction(ResponseController.responseEnum.OFFER, ResponseController.responseEnum.OK, this.gameObject);
        base.DestroyTrees();


        // atack()
        // decrease friendship 

        Invoke("EnableCone", 10f);
    }

    private void EnableCone()
    {
        GetComponent<VisibilityConeCycleIA>().enabled = true;
        base.visibiCone.IDecided = false;

    }
}
