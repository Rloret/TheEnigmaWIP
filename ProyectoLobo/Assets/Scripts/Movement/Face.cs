using UnityEngine;
using System.Collections;

public class Face : Align
{
    protected GameObject targetAux;

    public override void Awake()
    {
        base.Awake();

        targetAux = target;
        target = new GameObject();
        target.AddComponent<AgentPositionController>();
    }

    void OnDestroy()
    {
        Destroy(target);
    }

    ///<summary>
    ///Computes target's orientation and then delegates to Align.
    /// A.K.A lookAt().
    ///</summary>
    public override SteeringOutput GetSteering()
    {
        
        Vector2 direction = targetAux.GetComponent<AgentPositionController>().Position - new Vector2(transform.position.x, transform.position.y);

        if (direction.magnitude > 0.0f)
        {
            float targetOrientation = Mathf.Atan2(direction.x,
            direction.y);
            targetOrientation *= Mathf.Rad2Deg;
            target.GetComponent<AgentPositionController>().Orientation=
            targetOrientation;
        }
        return base.GetSteering();
    }
}
