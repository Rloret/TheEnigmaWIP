using UnityEngine;
using System.Collections;

public class AgentPositionController : MonoBehaviour {

    #region Public Domain
    public float MaxLinearVelocity;
    public float angularVelocity;
    public float maxAccel;

    public Vector2 linearVelocity;

    protected SteeringOutput Steering;
    #endregion

    #region private Domain
    private Vector2 position;
 
    private float orientation; // stores the angle in Rads to turn like if you were looking towards the positive Z Axis counterClockWise
    #endregion



    void Start () {
        Steering = new SteeringOutput();
        position = new Vector2(this.transform.position.x, this.transform.position.y);
	}

    public void SetSteering (SteeringOutput Steering)
    {
        this.Steering = Steering;
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
        this.transform.Translate(position);
        this.transform.Rotate(Vector3.up, orientation);
        //this.transform.position = currentPosition;
    }

    void LateUpdate()
    {
        linearVelocity += Steering.LinearAcceleration * Time.deltaTime;
        angularVelocity += Steering.AngularAcceleration * Time.deltaTime;

        if (linearVelocity.magnitude > MaxLinearVelocity)
        {
            linearVelocity.Normalize();
            linearVelocity *= MaxLinearVelocity;
        }

        if (Steering.AngularAcceleration == 0.0f)
            angularVelocity = 0.0f;
        if (Steering.LinearAcceleration.sqrMagnitude == 0.0f)
            linearVelocity = Vector2.zero;
        Steering = new SteeringOutput();

    }
}
