using UnityEngine;
using System.Collections;

public class Leave : AgentBehaviour {

    public float escapeRadius;
    public float dangerRadius;
    public float timeToTarget = 0.1f;


    public override SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();

        Vector2 direction = (Vector2)target.transform.position - (Vector2)transform.position; // transform.position es vector3 y queremos vector2, puede dar problemas.
        float distance = direction.magnitude;
        float reduce;
        if (distance < dangerRadius) // si la distancia es menor que el radio del objetivo, es que ya hemos llegado.
            return steering;
        if (distance > escapeRadius) // si la distancia es mayor que el radio de deceleración
            reduce = 0f;
        else
            reduce = distance / dangerRadius * agent.maxLinearVelocity;
        float targetSpeed = agent.maxLinearVelocity - reduce;

        Vector2 desiredVelocity = direction;
        desiredVelocity.Normalize();
        desiredVelocity *= targetSpeed;
        steering.linearAcceleration = desiredVelocity - agent.linearVelocity;
        steering.linearAcceleration /= timeToTarget;

        if (steering.linearAcceleration.magnitude > agent.maxAccel)
        {
            steering.linearAcceleration.Normalize();
            steering.linearAcceleration *= agent.maxAccel;
        }

        return steering;
    }
}
