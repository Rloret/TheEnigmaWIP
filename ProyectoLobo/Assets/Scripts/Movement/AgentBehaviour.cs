using UnityEngine;
using System.Collections;

public class AgentBehaviour : MonoBehaviour {

    public GameObject target;
    protected AgentPositionController agent;

	// Use this for initialization
	public virtual void Awake () {
        agent = gameObject.GetComponent<AgentPositionController>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
        agent.SetSteering(GetSteering());
	
	}

    public virtual SteeringOutput GetSteering()
    {
        return new SteeringOutput();
    }
}
