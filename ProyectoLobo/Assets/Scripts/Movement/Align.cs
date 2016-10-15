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

        float targetOrientation = target.GetComponent<AgentPositionController>().orientation;
        float desiredAngularVelocity = targetOrientation - agent.orientation;

        //determines which is the best direction of rotation clockwise or counterclockwise to make the wisest (and shortest) rotation
        desiredAngularVelocity = MapToRange(desiredAngularVelocity);

        float desiredAngularVelocitySize = Mathf.Abs(desiredAngularVelocity);

        if (desiredAngularVelocitySize < targetRadius)
        {
            return auxSteering;
        }

        float targetAngularVelocity;

        targetAngularVelocity = desiredAngularVelocitySize > slowRadius? agent.maxAngularVelocity:(agent.maxAngularVelocity* desiredAngularVelocitySize / slowRadius);

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
