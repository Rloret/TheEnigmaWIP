using UnityEngine;
using System.Collections;

public class Seek : AgentBehaviour {

    public override SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();
        steering.LinearAcceleration = target.transform.position - transform.position;
        steering.LinearAcceleration.Normalize();
        steering.LinearAcceleration = steering.LinearAcceleration * agent.maxAccel;

        return steering;
    }
}
