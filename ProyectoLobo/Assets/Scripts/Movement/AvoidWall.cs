using UnityEngine;
using System.Collections;

public class AvoidWall : Seek {

    public float avoidDistance =  100; // distancia de la pared a la que se posicionará el nuevo target
    public float lookAhead = 100; // distancia de RayCast

    private GameObject auxTarget;
    private Sprite sp;

    public override void Awake()
    {
        base.Awake();
        target = new GameObject();
        auxTarget = new GameObject();
        sp = this.GetComponent<SpriteRenderer>().sprite;
    }

    public override SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();
        Vector2 position = transform.position;
        Vector2 rayVector = agent.linearVelocity.normalized * lookAhead; 
        Vector2 direction = rayVector;
        int wallMask = 1 << 8; // sólo se revisarán las colisiones con los objetos en la capa 8

        RaycastHit2D hit3 = Physics2D.Raycast( sp.border , direction , lookAhead , wallMask);
        Debug.DrawRay(position, direction, Color.green, 0.2f);

        Debug.Log("Centro = " + position + "; / Bounds.Min = " + sp.bounds.min + "");

        //RaycastHit2D hit2 = Physics2D.Raycast(sp.bounds.max, direction, lookAhead, wallMask);
        //Debug.DrawRay(position, direction, Color.blue, 0.2f);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, lookAhead, wallMask);
        Debug.DrawRay(position, direction, Color.yellow, 0.2f);

        if ( hit ) {
            Debug.Log("Hay colision! collider = " + hit.collider.name);
            position = hit.point + hit.normal * avoidDistance;
            auxTarget.transform.position = position;
            target = auxTarget;
            base.target = auxTarget;
            steering = base.GetSteering();
            
        }


        return steering;

    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(target.transform.position, Vector3.one * 10);
        base.OnDrawGizmos();
    }

}
