using UnityEngine;
using System.Collections;

public class Pursue : Seek {

    public float maxPrediction;

    private GameObject targetAux;
    private AgentPositionController targetAgent;

    public override void Awake()
    {
        base.Awake();
        targetAgent = target.GetComponent<AgentPositionController>();
        targetAux = target;
        target = new GameObject();


    }

    void Ondestroy() {
        Destroy(targetAux);
    }

    public override SteeringOutput GetSteering()
    {
        Vector2 direction = targetAux.transform.position - transform.position;
        float distance = direction.magnitude;
        float speed = agent.linearVelocity.magnitude;
        float prediction;

        if (speed <= distance / maxPrediction) prediction = maxPrediction;
        else prediction = distance / speed;

        target.transform.position = targetAux.transform.position;

        Vector3 posAux = new Vector3(targetAgent.linearVelocity.x, targetAgent.linearVelocity.y, 0f);
        target.transform.position +=posAux* prediction;


        return base.GetSteering();
    }

    public override void OnDrawGizmos()
    {

        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(target.transform.position, Vector3.one * 5);
        // Gizmos.DrawRay(agent.transform.position, (transform.position - target.transform.position).normalized * agent.maxAccel);
        base.OnDrawGizmos();
    }
}
