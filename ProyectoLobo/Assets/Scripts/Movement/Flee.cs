using UnityEngine;
using System.Collections;

public class Flee : AgentBehaviour {

    public override SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();
        steering.LinearAcceleration = transform.position - target.transform.position;
        steering.LinearAcceleration.Normalize();
        steering.LinearAcceleration = steering.LinearAcceleration * agent.maxAccel;

        return steering;
    }
}
