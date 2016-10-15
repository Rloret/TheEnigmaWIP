using UnityEngine;
using System.Collections;

public class Wander : Face {

    public float offset;
    public float radius;
    public float rate;

    public override void Awake()
    {
        target = new GameObject();
        target.transform.position = transform.position;
        base.Awake();
    }

    public override SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();

        float wanderOrientation = Random.Range(-1.0f, 1.0f) * rate;
        float targetOrientation = wanderOrientation + agent.Orientation;
        Vector2 orientationVec = GetOriAsVec(agent.Orientation);
        Vector2 transformposition = (Vector2)transform.position;
        Vector2 targetPosition = (offset * orientationVec) + transformposition;

        targetPosition = targetPosition + (GetOriAsVec(targetOrientation) * radius);
        targetAux.transform.position = targetPosition;
        steering = base.GetSteering();
        steering.LinearAcceleration = targetAux.transform.position - transform.position;
        steering.LinearAcceleration.Normalize();
        steering.LinearAcceleration *= agent.MaxLinearVelocity;

        return steering;
    }

}
