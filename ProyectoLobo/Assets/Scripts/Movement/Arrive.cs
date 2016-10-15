using UnityEngine;
using System.Collections;

public class Arrive : AgentBehaviour {

    public float targetRadius;
    public float slowRadius;
    public float timeToTarget = 0.1f;


    public override SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();

        Vector2 direction = (Vector2) target.transform.position - (Vector2) transform.position; // transform.position es vector3 y queremos vector2, puede dar problemas.
        float distance = direction.magnitude;
        float targetSpeed;
        if (distance < targetRadius) // si la distancia es menor que el radio del objetivo, es que ya hemos llegado.
            return steering;
        if (distance > slowRadius) // si la distancia es mayor que el radio de deceleración
            targetSpeed = agent.MaxLinearVelocity; // vamos a tope
        else
            targetSpeed = agent.MaxLinearVelocity * distance / slowRadius; // si no, es que estamos dentro del radio de deceleración y aminoramos progresivamente.

        Vector2 desiredVelocity = direction;
        desiredVelocity.Normalize();
        desiredVelocity *= targetSpeed;
        steering.LinearAcceleration = desiredVelocity - agent.linearVelocity;
        steering.LinearAcceleration /= timeToTarget;

        if (steering.LinearAcceleration.magnitude > agent.MaxLinearVelocity) {
            steering.LinearAcceleration.Normalize();
            steering.LinearAcceleration *= agent.MaxLinearVelocity;
        }

        return steering;
    }

}
