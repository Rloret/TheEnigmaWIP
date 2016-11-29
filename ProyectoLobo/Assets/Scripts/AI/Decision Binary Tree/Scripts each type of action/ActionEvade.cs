using UnityEngine;
using System.Collections;

public class ActionEvade : Action {
  

    public override void DoAction()
    {

        Debug.Log("voy a huir y bajo confianza");

        
        string[] behaviours = { "Flee", "AvoidWall", "LookWhereYouAreGoing" };
        float[] weightedBehavs = { 0.7f, 1, 1 };

        GetComponent<VisibilityConeCycleIA>().enabled = false;
        base.visibiCone.IDecided = false;


        GameObject.FindGameObjectWithTag("GameController").GetComponent<OnObjectClickedController>().addBehavioursOver(this.gameObject, this.GetComponent<DecisionTreeCreator>().target, behaviours, weightedBehavs);

        Invoke("EnableCone", 10f);
    }

    private void EnableCone()
    {
        GetComponent<VisibilityConeCycleIA>().enabled = true;

    }
}
