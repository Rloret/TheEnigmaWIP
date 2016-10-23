using UnityEngine;
using System.Collections;

public class AgentBehaviour : MonoBehaviour {

    public   GameObject target;
    public float Weight = 1.0f;
    protected AgentPositionController agent;

    public virtual void Awake () {
        agent = gameObject.GetComponent<AgentPositionController>();
        Debug.Log("ahora Awake de agentbehaviour");
    }
	
	// Update is called once per frame
	public virtual void Update () {
        agent.SetSteering(GetSteering(),Weight);
	
	}

    public AgentBehaviour setTarget(GameObject targ)
    {
        this.target = targ;
        return this;
    }

    public AgentBehaviour setWeight(float weight)
    {
        this.Weight = weight;
        return this;
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

    public virtual void OnDrawGizmos() {
        
    }

}
