using UnityEngine;
using System.Collections;

public class Wander : Face {

    public float offset=90;
    public float radius=32;
    public float rate=90;

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
        target.transform.position = targetAux.transform.position;
        Vector3 direction = target.transform.position - this.transform.position;

        Debug.Log("ahora getsteering de face");
        if (direction.magnitude > 0.0f)
        {
            float Tarorient = Mathf.Atan2(direction.x, direction.y);
            Tarorient *= Mathf.Rad2Deg;
            target.transform.rotation = Quaternion.Euler(0, 0, Tarorient);
        }

        SteeringOutput steering = new SteeringOutput();
        Debug.Log("ahora steering de wander");

        float wanderOrientation = Random.Range(-1.0f, 1.0f) * rate;
        float targetOrientation = wanderOrientation + target.transform.rotation.eulerAngles.z;
        Vector2 orientationVec = GetOriAsVec(targetOrientation);
        Vector2 transformposition = (Vector2)transform.position;
        Vector2 targetPosition = (offset * orientationVec) + transformposition;

        targetPosition = targetPosition + (GetOriAsVec(targetOrientation) * radius);
        target.transform.position = targetPosition;

        steering = base.GetSteering();
        steering.linearAcceleration = target.transform.position - transform.position;
        steering.linearAcceleration.Normalize();
        steering.linearAcceleration *= agent.maxLinearVelocity;

        DestroyImmediate(target);
        target = targetauxaux;
        return steering;
    }

}
