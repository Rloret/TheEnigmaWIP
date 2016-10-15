using UnityEngine;
using System.Collections;

public class SteeringOutput {

    public Vector2 linearAcceleration;
    public float angularAcceleration;


    public SteeringOutput()
    {
        linearAcceleration = new Vector2();
        angularAcceleration = 0.0f;
    }

}


