using UnityEngine;
using System.Collections;

public class Align : AgentBehaviour {

    public float targetRadius;
    public float slowRadius;
    public float timeToTarget=0.1f;

    ///<summary>
    ///Align steering behaviour computes de angular acceleration according to a certain raius. Which we will use to determine the Quantity of angles to rotate.
    ///</summary>
    public override SteeringOutput GetSteering()
    {
        SteeringOutput auxSteering = new SteeringOutput();

        float targetOrientation = target.GetComponent<AgentPositionController>().Orientation;
        float desiredAngularVelocity = targetOrientation - agent.Orientation;

        //determines which is the best direction of rotation clockwise or counterclockwise to make the wisest (and shortest) rotation
        desiredAngularVelocity = MapToRange(desiredAngularVelocity);

        float desiredAngularVelocitySize = Mathf.Abs(desiredAngularVelocity);

        if (desiredAngularVelocitySize < targetRadius)
        {
            return auxSteering;
        }

        float targetAngularVelocity;

        targetAngularVelocity = desiredAngularVelocitySize > slowRadius? agent.MaxAngularVelocity:(agent.MaxAngularVelocity* desiredAngularVelocitySize / slowRadius);

        targetAngularVelocity *= desiredAngularVelocity / desiredAngularVelocitySize;
        auxSteering.AngularAcceleration = (targetAngularVelocity - agent.AngularVelocity) / timeToTarget;


        float angularAcceleration = Mathf.Abs(auxSteering.AngularAcceleration);

        if (angularAcceleration > agent.MaxAngularVelocity)
        {

            auxSteering.AngularAcceleration /= angularAcceleration;
            auxSteering.AngularAcceleration *= agent.MaxAngularVelocity;
        }
        return auxSteering;
    }


}
