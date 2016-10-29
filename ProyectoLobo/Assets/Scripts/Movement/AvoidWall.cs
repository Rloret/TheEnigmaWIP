using UnityEngine;
using System.Collections;

public class AvoidWall : Seek {

    public float avoidDistance =  10;
    public float lookAhead = 100;

    public override void Awake()
    {
        base.Awake();
        target = new GameObject();
    }

    public override SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();
        Vector2 position = transform.position;
        Vector2 rayVector = agent.linearVelocity.normalized * lookAhead; 
        Vector2 direction = rayVector;
        int wallMask = 1 << 8;
        RaycastHit2D hit = Physics2D.Raycast( position , direction , lookAhead , wallMask);
        Debug.DrawRay(position, direction, Color.yellow, 0.2f);

        if ( hit ) {
            Debug.Log("Hay colision! collider = " + hit.collider.name);
            position = hit.point  * avoidDistance;
            target.transform.position = position;
            steering = base.GetSteering();
        }

        return steering;

    }

}
