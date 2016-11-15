using UnityEngine;
using System.Collections;

public class AvoidWall : Seek {

    public float avoidDistance =  100; // distancia de la pared a la que se posicionará el nuevo target
    public float lookAhead = 80; // distancia de RayCast

    private GameObject auxTarget;
    private Sprite sp;
    private float agentRadius;

    public override void Awake()
    {
        if (GameObject.Find("target" +this.name) != null) {
            //Debug.Log("Hay un target preexistente");
            DestroyImmediate(GameObject.Find("target" +this.name));
            DestroyImmediate(GameObject.Find("auxTarget" +this.name));
        }

        base.Awake();
        target = new GameObject();
        auxTarget = new GameObject();
        target.name = "target" + this.name;
        auxTarget.name = "auxTarget" + this.name;
        sp = this.GetComponent<SpriteRenderer>().sprite;
        agentRadius = sp.bounds.min.x * transform.localScale.x;
    }

    public override SteeringOutput GetSteering()
    {
        SteeringOutput steering = new SteeringOutput();
        Vector2 position = transform.position;
        Vector2 rayVector = agent.linearVelocity.normalized * lookAhead; 
        Vector2 direction = rayVector;
        int wallMask = 1 << 8; // sólo se revisarán las colisiones con los objetos en la capa 8

        Vector2 rayOrigin1 = position + PerpendicularClockWise(direction) * agentRadius;
        Vector2 rayOrigin2 = position + PerpendicularCounterClockWise(direction) * agentRadius;

        RaycastHit2D hit = Physics2D.Raycast( rayOrigin1  , direction , lookAhead , wallMask);
        Debug.DrawRay(rayOrigin1, direction, Color.green, 0.2f);

        RaycastHit2D hit2 = Physics2D.Raycast(rayOrigin2, direction, lookAhead, wallMask);
        Debug.DrawRay(rayOrigin2, direction, Color.blue, 0.2f);
        
        //RaycastHit2D hit = Physics2D.Raycast(position, direction, lookAhead, wallMask); ESTE ES SOLO UN RAYO DESDE EL CENTROº
        //Debug.DrawRay(position, direction, Color.yellow, 0.2f);

        if ( hit ) {
            //Debug.Log("Hay colision! collider = " + hit.collider.name);
            position = hit.point + hit.normal * avoidDistance;
            auxTarget.transform.position = position;
            target = auxTarget;
            base.target = auxTarget;
            steering = base.GetSteering();
            
        }else if (hit2)
        {
            //Debug.Log("Hay colision! collider = " + hit2.collider.name);
            position = hit2.point + hit2.normal * avoidDistance;
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
        Gizmos.DrawWireSphere(transform.position, (sp.bounds.min.x * transform.localScale.x));

        base.OnDrawGizmos();
    }

    private Vector2 PerpendicularClockWise(Vector2 v2) {
        return new Vector2( -v2.y , v2.x ).normalized;
    }
    private Vector2 PerpendicularCounterClockWise(Vector2 v2)
    {
        return new Vector2( v2.y, -v2.x ).normalized;
    }



}
