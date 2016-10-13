using UnityEngine;
using System.Collections;

public class AgentPositionController : MonoBehaviour {

    #region Public Domain
    public float MaxLinearVelocity;
    public float MaxAngularVelocity;
    public float AngularVelocity;
    public float Orientation; // stores the angle in Rads to turn like if you were looking towards the positive Z Axis counterClockWise
    public Vector2 Position;
    public Vector2 linearVelocity;

    protected SteeringOutput Steering;
    #endregion

    #region private Domain

 
    #endregion



    void Start () {
        Steering = new SteeringOutput();
        Position = new Vector2(this.transform.position.x, this.transform.position.y);
	}

    public void SetSteering (SteeringOutput Steering)
    {
        this.Steering = Steering;
    }
	

	void Update () {

        Position += linearVelocity * Time.deltaTime;
        Orientation += AngularVelocity * Time.deltaTime;
        if (Orientation < 0.0f)
            Orientation += 360.0f;
        else if (Orientation > 360.0f)
            Orientation -= 360.0f;

        transform.rotation = new Quaternion();

        updateUnityVariables(ref Position);
	}

    void updateUnityVariables(ref Vector2 currentPosition) {
        this.transform.Translate(Position);
        this.transform.Rotate(Vector3.up, Orientation);
        //this.transform.Position = currentPosition;
    }

    void LateUpdate()
    {
        linearVelocity += Steering.LinearAcceleration * Time.deltaTime;
        AngularVelocity += Steering.AngularAcceleration * Time.deltaTime;

        if (linearVelocity.magnitude > MaxLinearVelocity)
        {
            linearVelocity.Normalize();
            linearVelocity *= MaxLinearVelocity;
        }

        if (Steering.AngularAcceleration == 0.0f)
            AngularVelocity = 0.0f;
        if (Steering.LinearAcceleration.sqrMagnitude == 0.0f)
            linearVelocity = Vector2.zero;
        Steering = new SteeringOutput();

    }
}
