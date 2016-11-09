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


        SteeringOutput auxSteering = new SteeringOutput();

        float targetOrientation = target.transform.rotation.eulerAngles.z;

        float desiredAngularVelocity = targetOrientation - agent.orientation;
        //reduceTurningAngle = 0.25f * stopTurningAngle;


        //determines which is the best direction of rotation clockwise or counterclockwise to make the wisest (and shortest) rotation
        desiredAngularVelocity = MapToRange(desiredAngularVelocity);

      

        float desiredAngularVelocitySize = Mathf.Abs(desiredAngularVelocity);

        if (desiredAngularVelocitySize == stopTurningAngle)
        {

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

    public override void OnDrawGizmos()
    {
        float targetOrientation = target.transform.rotation.eulerAngles.z;

        float desiredAngularVelocity = targetOrientation - agent.orientation;
        //reduceTurningAngle = 0.25f * stopTurningAngle;


        //determines which is the best direction of rotation clockwise or counterclockwise to make the wisest (and shortest) rotation
        desiredAngularVelocity = MapToRange(desiredAngularVelocity);



        float desiredAngularVelocitySize = Mathf.Abs(desiredAngularVelocity);

        float targetAngularVelocity =0;
        float steerangularAcceleration;
        if (desiredAngularVelocitySize == stopTurningAngle)
        {
            steerangularAcceleration = 0;
        }

        else
        {

            targetAngularVelocity = desiredAngularVelocitySize > reduceTurningAngle ? agent.maxAngularVelocity : (agent.maxAngularVelocity * desiredAngularVelocitySize / reduceTurningAngle);

            targetAngularVelocity *= desiredAngularVelocity / desiredAngularVelocitySize;
            steerangularAcceleration = (targetAngularVelocity - agent.angularVelocity) / timeToTarget;

            float angularAcceleration = Mathf.Abs(steerangularAcceleration);

            if (angularAcceleration > agent.maxAngularVelocity)
            {
                steerangularAcceleration /= angularAcceleration;
                steerangularAcceleration *= agent.maxAngularVelocity;
            }
        }
        Gizmos.color = Color.red;
        Gizmos.DrawRay(agent.transform.position, base.GetOriAsVec(steerangularAcceleration)*30);
        Gizmos.color = Color.black;
        Gizmos.DrawRay(agent.transform.position, base.GetOriAsVec(desiredAngularVelocity) *30);

        Gizmos.color = new Color(197f / 255f, 100f / 255f, 0f);
        Gizmos.DrawRay(agent.transform.position, base.GetOriAsVec(stopTurningAngle)*30);
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(target.transform.position, Vector3.one * 4);
    }
}
