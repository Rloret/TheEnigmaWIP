using UnityEngine;
using System.Collections;

public class AgentBehaviour : MonoBehaviour {

    public   GameObject target;
    protected AgentPositionController agent;

    //Esto es una errata del libro, estas cuatro variables estan en agentPositionController
    /*public float MaxLinearVelocity; 
    public float MaxLinearAcceleration;
    public float MaxAngularVelocity;
    public float maxAngularAcceleration;*/


    public virtual void Awake () {
        agent = gameObject.GetComponent<AgentPositionController>();
        Debug.Log("ahora Awake de agentbehaviour");
    }
	
	// Update is called once per frame
	public virtual void Update () {
        agent.SetSteering(GetSteering());
	
	}

    public virtual SteeringOutput GetSteering()
    {
        return new SteeringOutput();
    }

    public float MapToRange(float angularVelocity)
    {

        angularVelocity %= 360;

        if (Mathf.Abs(angularVelocity) > 180.0f)
        {
            if (angularVelocity < 0.0f) angularVelocity += 360.0f;
            else angularVelocity -= 360.0f;
        }
        return angularVelocity;

    }

    public Vector2 GetOriAsVec (float orientation)
    {
        Vector2 vector = Vector2.zero;
        vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
        vector.y = Mathf.Cos(orientation * Mathf.Deg2Rad) * 1.0f;
        return vector.normalized;

    }
}
