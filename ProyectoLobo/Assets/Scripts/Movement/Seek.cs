using UnityEngine;
using System.Collections;

public class Seek : AgentBehaviour {

    public override SteeringOutput GetSteering()
    {

        SteeringOutput steering = new SteeringOutput();
        steering.linearAcceleration = target.transform.position - transform.position;
        steering.linearAcceleration.Normalize();
        steering.linearAcceleration = steering.linearAcceleration * agent.maxAccel;

        return steering;
    }

    public override void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(agent.transform.position, (target.transform.position - agent.transform.position).normalized * agent.maxAccel);

    }

}
