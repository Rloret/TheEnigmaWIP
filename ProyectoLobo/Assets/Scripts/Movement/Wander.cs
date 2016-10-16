using UnityEngine;
using System.Collections;

public class Wander : Face {

    public float offset=20;
    public float radius=50;
    public float rate=50;

   /* public override void Awake()
    {
        target = new GameObject();
        target.transform.position = transform.position;
        base.Awake();
    }*/

    public override SteeringOutput GetSteering()
    {
        targetAux = target;
        GameObject targetauxaux = target;
        target = new GameObject();
        target.transform.rotation = targetAux.transform.rotation;
        target.transform.position = targetAux.transform.position;

        SteeringOutput steering = new SteeringOutput();
        Debug.Log("ahora steering de wander");

        float wanderOrientation = Random.Range(-1.0f, 1.0f) * rate;
        float targetOrientation = wanderOrientation + agent.orientation;
        Vector2 orientationVec = GetOriAsVec(agent.orientation);
        Vector2 transformposition = (Vector2)transform.position;
        Vector2 targetPosition = (offset * orientationVec) + transformposition;

        targetPosition = targetPosition + (GetOriAsVec(targetOrientation) * radius);
        target.transform.position = targetPosition;

        steering = base.GetSteering();
        steering.linearAcceleration = targetAux.transform.position - transform.position;
        steering.linearAcceleration.Normalize();
        steering.linearAcceleration *= agent.maxLinearVelocity;

        DestroyImmediate(target);
        target = targetauxaux;
        return steering;
    }

}
