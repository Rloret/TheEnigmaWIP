using UnityEngine;
using System.Collections;

public class AgentPositionController : MonoBehaviour {

    #region Public Domain
    public float maxLinearVelocity;
    public float angularVelocity;
    public float maxAccel;
    public float maxAngularVelocity;
    public float orientation; // stores the angle in Rads to turn like if you were looking towards the positive Z Axis counterClockWise
    public Vector2 position;

    public Vector2 linearVelocity;

    protected SteeringOutput steering;
    #endregion

    #region private Domain

 
    #endregion



    void Start () {
        steering = new SteeringOutput();
        position = new Vector2(this.transform.position.x, this.transform.position.y);
	}

    public void SetSteering (SteeringOutput Steering)
    {
        this.steering = Steering;
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
        this.transform.position = currentPosition;
        this.transform.rotation = Quaternion.Euler(Vector3.forward * orientation);
    }

    void LateUpdate()
    {
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

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(position, linearVelocity);
    }

}
