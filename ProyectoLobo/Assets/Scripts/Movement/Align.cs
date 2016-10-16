using UnityEngine;
using System.Collections;

public class Align : AgentBehaviour {

    public float stopTurningAngle=0;
    public float reduceTurningAngle=90;
    public float timeToTarget=0.1f;

    ///<summary>
    ///Align steering behaviour computes de angular acceleration according to a certain raius. Which we will use to determine the Quantity of angles to rotate.
    ///</summary>
    public override SteeringOutput GetSteering()
    {

        Debug.Log("ahora getseteering de Align");
        SteeringOutput auxSteering = new SteeringOutput();

        float targetOrientation = target.transform.rotation.eulerAngles.z;

        float desiredAngularVelocity = targetOrientation - agent.orientation;
        //reduceTurningAngle = 0.25f * stopTurningAngle;


        //determines which is the best direction of rotation clockwise or counterclockwise to make the wisest (and shortest) rotation
        desiredAngularVelocity = MapToRange(desiredAngularVelocity);

      

        float desiredAngularVelocitySize = Mathf.Abs(desiredAngularVelocity);

        if (desiredAngularVelocitySize == stopTurningAngle)
        {
            Debug.Log("paro");
            return auxSteering;
        }

        float targetAngularVelocity;

        targetAngularVelocity = desiredAngularVelocitySize > reduceTurningAngle? agent.maxAngularVelocity:(agent.maxAngularVelocity* desiredAngularVelocitySize / reduceTurningAngle);

        targetAngularVelocity *= desiredAngularVelocity / desiredAngularVelocitySize;
        auxSteering.angularAcceleration = (targetAngularVelocity - agent.angularVelocity) / timeToTarget;


        float angularAcceleration = Mathf.Abs(auxSteering.angularAcceleration);

        if (angularAcceleration > agent.maxAngularVelocity)
        {
            auxSteering.angularAcceleration /= angularAcceleration;
            auxSteering.angularAcceleration *= agent.maxAngularVelocity;
        }
        return auxSteering;
    }


}
