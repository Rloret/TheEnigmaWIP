using UnityEngine;
using System.Collections;

public class Leave : AgentBehaviour {

    public float escapeRadius=32;
    public float dangerRadius=64;
    public float timeToTarget = 0.1f;


    public override SteeringOutput GetSteering()
    {
        if (this != null && target!=null)
        {
            SteeringOutput steering = new SteeringOutput();
            Vector2 direction = -(Vector2)target.transform.position + (Vector2)transform.position; // transform.position es vector3 y queremos vector2, puede dar problemas.
            float distance = direction.magnitude;
            float reduce;
            if (distance > dangerRadius) // si la distancia es mayor que el radio de peligro para(Y cambia de behaviour, probablemente).
                return steering;
            if (distance < escapeRadius) // si la distancia es menor que el radio de escape sala  toda hostia
                reduce = 0f;
            else
                reduce = distance / dangerRadius / 2 * agent.maxLinearVelocity;
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
        return new SteeringOutput();
    }

    public override void OnDrawGizmos()
    {
        if (this != null && target != null)
        {   
            Gizmos.color = Color.green;
            Vector2 direction = (Vector2)target.transform.position - (Vector2)transform.position; // transform.position es vector3 y queremos vector2, puede dar problemas.
            float distance = direction.magnitude;
            float targetSpeed;
            if (distance > dangerRadius) // si la distancia es menor que el radio del objetivo, es que ya hemos llegado.
                targetSpeed = 0;
            if (distance > escapeRadius) // si la distancia es mayor que el radio de deceleración
                targetSpeed = agent.maxLinearVelocity;
            else
                targetSpeed = distance / dangerRadius * agent.maxLinearVelocity; // si no, es que estamos dentro del radio de deceleración y aminoramos progresivamente.

            Vector2 desiredVelocity = direction;
            desiredVelocity.Normalize();
            desiredVelocity *= targetSpeed;

            Vector2 linacceletration = desiredVelocity - agent.linearVelocity;
            linacceletration /= timeToTarget;

            if (linacceletration.magnitude > agent.maxAccel)
            {
                linacceletration.Normalize();
                linacceletration *= agent.maxAccel;
            }
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(agent.transform.position, linacceletration);
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(target.transform.position, escapeRadius);

            Gizmos.color = new Color(197f / 255f, 100f / 255f, 0f);//orange
            Gizmos.DrawWireSphere(target.transform.position, dangerRadius);
        }
    }

        // Gizmos.DrawRay(agent.transform.position, (transform.position - target.transform.position).normalized * agent.maxAccel);
}
