﻿using UnityEngine;
using System.Collections;

public class Evade : Flee {

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

    void Ondestroy()
    {
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
        target.transform.position += posAux * prediction;


        return base.GetSteering();
    }
}