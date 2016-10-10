using UnityEngine;
using System.Collections;

public class AgentPositionController : MonoBehaviour {

    #region Public Domain
    public float MaxLinearVelocity;
    public float angularVelocity;

    public Vector2 linearVelocity;

    public SteeringOutput Steering;
    #endregion

    #region private Domain
    private Vector2 position;
 
    private float orientation; // stores the angle in Rads to turn like if you were looking towards the positive Z Axis counterClockWise
    #endregion



    void Start () {
        Steering = new SteeringOutput();
        position = new Vector2(this.transform.position.x, this.transform.position.y);
	}
	

	void Update () {

        position += linearVelocity * Time.deltaTime;
        orientation += angularVelocity * Time.deltaTime;

        linearVelocity += Steering.LinearAcceleration * Time.deltaTime;
        angularVelocity += Steering.AngularAcceleration * Time.deltaTime;

        if(linearVelocity.magnitude > MaxLinearVelocity)
        {
            linearVelocity.Normalize();
            linearVelocity *= MaxLinearVelocity;
        }

        updateUnityVariables(ref position);
	}

    void updateUnityVariables(ref Vector2 currentPosition) {
        this.transform.position = currentPosition;
    }
}
