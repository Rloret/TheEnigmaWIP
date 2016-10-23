using UnityEngine;
using System.Collections;

public class Flee : AgentBehaviour {

    public override SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();

        steering.linearAcceleration = transform.position - target.transform.position;
        steering.linearAcceleration.Normalize();
        steering.linearAcceleration = steering.linearAcceleration * agent.maxAccel;

        return steering;
    }
    public override void OnDrawGizmos()
    {
        Gizmos.color =Color.Lerp( Color.magenta,Color.red,0.5f);
        Gizmos.DrawRay(agent.transform.position, (agent.transform.position - target.transform.position).normalized * agent.maxAccel);
    }

}
