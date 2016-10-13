using UnityEngine;
using System.Collections;

public class SteeringOutput {

    public Vector2 LinearAcceleration;
    public float AngularAcceleration;


    public SteeringOutput()
    {
        LinearAcceleration = new Vector3();
        AngularAcceleration = 0.0f;
    }

}


