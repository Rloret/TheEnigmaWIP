using UnityEngine;
using System.Collections;

public class LookWhereYouAreGoing : Align {
    GameObject targetAux;

    public override SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();
        if(agent.linearVelocity.magnitude == 0)
        {
            return steering;
        }


        targetAux = target;
                
        target = new GameObject();
        float angle = Mathf.Atan2(-agent.linearVelocity.x, agent.linearVelocity.y) * Mathf.Rad2Deg;
        //Debug.Log("Angle es " + angle);
        target.transform.rotation = Quaternion.Euler(Vector3.forward *angle);

        steering =base.GetSteering();

        DestroyImmediate(target);
        target = targetAux;
        return steering;
    }


    public override void OnDrawGizmos()
    {

    }
}
