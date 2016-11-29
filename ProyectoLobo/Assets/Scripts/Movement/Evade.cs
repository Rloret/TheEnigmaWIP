using UnityEngine;
using System.Collections;

public class Evade : Flee {

    public float maxPrediction;

    private GameObject targetAux;
    private AgentPositionController targetAgent;

    public override void Awake()
    {
        base.Awake();

    }

    void Ondestroy()
    {
        Destroy(targetAux);
    }

    public override SteeringOutput GetSteering()
    {
        if (target.GetComponent<AgentPositionController>() == null)
            targetAgent = new AgentPositionController();
        else
            targetAgent = target.GetComponent<AgentPositionController>();
        targetAux = target;
        target = new GameObject();
        Vector2 direction = targetAux.transform.position - transform.position;
        float distance = direction.magnitude;
        float speed = agent.linearVelocity.magnitude;
        float prediction;

        if (speed <= distance / maxPrediction) prediction = maxPrediction;
        else prediction = distance / (speed + 0.001f);
      //  Debug.Log("evade prediction = " + prediction);

        target.transform.position = targetAux.transform.position;

        Vector3 posAux = new Vector3(targetAgent.linearVelocity.x, targetAgent.linearVelocity.y, 0f) + new Vector3(0.001f, 0.001f, 0f);
        target.transform.position += posAux * prediction;


        DestroyImmediate(target);
        target = targetAux;
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
