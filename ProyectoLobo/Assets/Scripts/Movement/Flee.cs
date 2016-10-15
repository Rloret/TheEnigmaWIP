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
}
