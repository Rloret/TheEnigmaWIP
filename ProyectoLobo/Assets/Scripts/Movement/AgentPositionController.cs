using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AgentPositionController : MonoBehaviour {

    #region Public Domain
    public float maxLinearVelocity;
    public float angularVelocity;
    public float maxAccel;
    public float maxAngularVelocity;
    public float orientation; // stores the angle in Rads to turn like if you were looking towards the positive Z Axis counterClockWise
    [Tooltip("Esta variable es un minimo que se ha de cumplir determinar que grupo se va a aejecutar")]
    public float priorityThreshold = 0.1f;
    public Vector2 position;

    public Vector2 linearVelocity;

    protected SteeringOutput steering;
    #endregion

    #region private Domain

    private Dictionary<int, List<SteeringData>> SteeringGroups;

    private struct SteeringData
    {
        public SteeringOutput steering;
        public float weight;
        
        public SteeringData(SteeringOutput steer, float weight)
        {
            this.steering = steer;
            this.weight = weight;
        }
    }

    #endregion



    void Start () {
        steering = new SteeringOutput();
        position = new Vector2(this.transform.position.x, this.transform.position.y);
        SteeringGroups = new Dictionary<int, List<SteeringData>>();
    }

    public void SetSteering (SteeringOutput Steering,int priority, float weight)
    {
        if (!SteeringGroups.ContainsKey(priority))
        {
            SteeringGroups.Add(priority, new List<SteeringData>());
        }
        SteeringGroups[priority].Add(new SteeringData(Steering,weight));
       /* this.steering.linearAcceleration += (weight* Steering.linearAcceleration);
        this.steering.angularAcceleration += (weight * Steering.angularAcceleration);*/
    }
	

	void Update () {

        position += linearVelocity * Time.deltaTime;
        orientation += angularVelocity * Time.deltaTime;
        if (orientation < 0.0f)
            orientation += 360.0f;
        else if (orientation > 360.0f)
            orientation -= 360.0f;

        transform.rotation = new Quaternion();

        updateUnityVariables(ref position);

	}

    void updateUnityVariables(ref Vector2 currentPosition) {
        //this.transform.Translate(position);
        //this.transform.Rotate(Vector3.up, angularVelocity * Time.deltaTime);
        this.transform.position = new Vector3(currentPosition.x,currentPosition.y,0);
        this.transform.rotation = Quaternion.Euler(Vector3.forward * orientation);
    }

    void LateUpdate()
    {
        steering = GetPrioritySteering();
        SteeringGroups.Clear();

        linearVelocity += steering.linearAcceleration * Time.deltaTime;
        angularVelocity += steering.angularAcceleration * Time.deltaTime;

        if (linearVelocity.magnitude > maxLinearVelocity)
        {
            linearVelocity.Normalize();
            linearVelocity *= maxLinearVelocity;
        }

        if (steering.angularAcceleration == 0.0f)
            angularVelocity = 0.0f;
        if (steering.linearAcceleration.sqrMagnitude == 0.0f)
            linearVelocity = Vector2.zero;

        steering = new SteeringOutput();

    }

    private SteeringOutput GetPrioritySteering()
    {
        SteeringOutput steering = new SteeringOutput();
        float sqrThreshold = priorityThreshold * priorityThreshold;

        foreach (var steeringdata in SteeringGroups)
        {
            steering = new SteeringOutput();
            for (int i =0;i<steeringdata.Value.Count; i++)
            {
                SteeringData singlesteering = steeringdata.Value[i];
                steering.linearAcceleration += (singlesteering.weight * singlesteering.steering.linearAcceleration);
                steering.angularAcceleration += (singlesteering.weight * singlesteering.steering.angularAcceleration);
            }
            if (steering.linearAcceleration.sqrMagnitude > sqrThreshold ||
            Mathf.Abs(steering.angularAcceleration) > priorityThreshold)
            {
                return steering;
            }

        }
        return steering;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(position, linearVelocity);
    }

}
