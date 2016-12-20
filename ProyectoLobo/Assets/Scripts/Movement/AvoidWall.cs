using UnityEngine;
using System.Collections;

public class AvoidWall : Seek {

    public float avoidDistance =  100; // distancia de la pared a la que se posicionará el nuevo target
    public float lookAhead = 80; // distancia de RayCast
    public bool stuck = false;
    public LayerMask layerMask;

    private GameObject auxTarget;
    private Sprite sp;
    private float agentRadius;

    public override void Awake()
    {
        if (GameObject.Find("target" + this.name) != null)
        {
            //Debug.Log("Hay un target preexistente");
            DestroyImmediate(GameObject.Find("target" + this.name));
            DestroyImmediate(GameObject.Find("auxTarget" + this.name));
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


        lookAhead = agent.linearVelocity.magnitude + 30;
        SteeringOutput steering = new SteeringOutput();
        Vector2 position = transform.position;
        Vector2 rayVector = agent.linearVelocity.normalized * lookAhead;
        Vector2 directionIzq = rayVector + (Vector2)(Quaternion.Euler(0, 0, -30) * rayVector);
        Vector2 directionDer = rayVector + (Vector2)(Quaternion.Euler(0, 0, 30) * rayVector);
        LayerMask wallMask = 1 << 8 | 1 << 10; // sólo se revisarán las colisiones con los objetos en la capa 8

        Vector2 rayOrigin1 = position + PerpendicularClockWise(rayVector) * agentRadius;
        Vector2 rayOrigin2 = position + PerpendicularCounterClockWise(rayVector) * agentRadius;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin1, directionIzq, lookAhead, wallMask);
        directionIzq.Normalize();
        directionIzq *= lookAhead;
        Debug.DrawRay(rayOrigin1, directionIzq, Color.green, 0.2f);

        RaycastHit2D hit2 = Physics2D.Raycast(rayOrigin2, directionDer, lookAhead, wallMask);
        directionDer.Normalize();
        directionDer *= lookAhead;
        Debug.DrawRay(rayOrigin2, directionDer, Color.blue, 0.2f);

        RaycastHit2D hitCentral = Physics2D.Raycast(position, rayVector, lookAhead / 2, wallMask);
        Debug.DrawRay(position, rayVector.normalized * lookAhead / 2, Color.white, 0.2f);

        //RaycastHit2D hit = Physics2D.Raycast(position, direction, lookAhead, wallMask); ESTE ES SOLO UN RAYO DESDE EL CENTROº
        //Debug.DrawRay(position, direction, Color.yellow, 0.2f);

       /* if (target.gameObject.tag == "RoomsAndDoors") {
            auxTarget = new GameObject();
            auxTarget.transform.position = target.transform.position;
            Debug.Log("Cambio " + target.name + " por " + auxTarget);
            target = auxTarget;
        }*/
        
            if (!hit && !hit2 && !hitCentral)
            {
                if (agent.gameObject.tag == "IA")
                {
                    this.GetComponent<VisibilityConeCycleIA>().stuckedAI = false;
                }
            }
            else if (hit && !hit2)
            {
                //Debug.Log("Hay colision! collider = " + hit.collider.name);
                position = hit.point + hit.normal * avoidDistance;
                auxTarget.transform.position = position;
                target = auxTarget;
                base.target = auxTarget;

                if (agent.gameObject.tag == "IA")
                {
                    if (this.GetComponent<Arrive>())
                    {
                        //Debug.Log("No tengo arrive: " + this);
                        this.GetComponent<Arrive>().target.transform.position = auxTarget.transform.position;
                    }
                    this.GetComponent<VisibilityConeCycleIA>().stuckedAI = true;
                }
                steering = base.GetSteering();

            }
            else if (hit2 && !hit)
            {
                //Debug.Log("Hay colision! collider = " + hit2.collider.name);
                position = hit2.point + hit2.normal * avoidDistance;
                auxTarget.transform.position = position;
                target = auxTarget;
                base.target = auxTarget;
                if (agent.gameObject.tag == "IA")
                {
                    //if (!this.GetComponent<GroupScript>().inGroup || this.GetComponent<GroupScript>().IAmTheLeader || this.GetComponent<Evade>()) // Esto es para las IAs que siguen a los lideres y no tienen Arrive
                    if (this.GetComponent<Arrive>())
                    {
                        //Debug.Log("No tengo arrive: " + this);
                        this.GetComponent<Arrive>().target.transform.position = auxTarget.transform.position;
                    }
                    this.GetComponent<VisibilityConeCycleIA>().stuckedAI = true;
                }
                steering = base.GetSteering();

            }
            else if (hitCentral)
            {
                if (agent.gameObject.tag == "IA" && !this.GetComponent<VisibilityConeCycleIA>().stuckedAI)
                {
                    // Debug.Log("Soy IA y estoy ATASCADISIMA");

                    agent.linearVelocity = Vector2.zero;
                    agent.orientation *= -1;

                    position = hitCentral.point * -1 * avoidDistance;
                    auxTarget.transform.position = position;
                    target = auxTarget;
                    base.target = auxTarget;
                    if (this.GetComponent<Arrive>())
                    {
                        //Debug.Log("No tengo arrive: " + this);
                        this.GetComponent<Arrive>().target.transform.position = auxTarget.transform.position;
                    }
                    this.GetComponent<VisibilityConeCycleIA>().stuckedAI = true;
                    steering = base.GetSteering();

                }
                else
                {
                    agent.linearVelocity = Vector2.zero;
                }
            }
            else if (hit && hit2)
            {
                if (agent.gameObject.tag == "IA" && !this.GetComponent<VisibilityConeCycleIA>().stuckedAI)
                {
                    //Debug.Log("Soy IA y estoy atascada");
                    agent.linearVelocity *= -1 / 2;

                    position = hitCentral.point * -avoidDistance;
                    auxTarget.transform.position = position;
                    target = auxTarget;
                    base.target = auxTarget;
                    if (this.GetComponent<Arrive>())
                    {
                        // Debug.Log("No tengo arrive: " + this);
                        this.GetComponent<Arrive>().target.transform.position = auxTarget.transform.position;
                    }
                    this.GetComponent<VisibilityConeCycleIA>().stuckedAI = true;
                    steering = base.GetSteering();
                }
                else
                {
                    agent.linearVelocity = Vector2.zero;
                }
            }
        return steering;
    }
    public override void OnDrawGizmos()
    {
        if (this != null && target != null)
        {
            if (this.target.transform.position == null)
            {
                Debug.LogError("Error en " + this.name);
            }
            Gizmos.color = Color.red;
            Gizmos.DrawCube(target.transform.position, Vector3.one * 10);
            //Gizmos.DrawWireSphere(transform.position, (sp.bounds.min.x * transform.localScale.x));

            base.OnDrawGizmos();
        }
    }

    private Vector2 PerpendicularClockWise(Vector2 v2) {
        return new Vector2( -v2.y , v2.x ).normalized;
    }
    private Vector2 PerpendicularCounterClockWise(Vector2 v2)
    {
        return new Vector2( v2.y, -v2.x ).normalized;
    }



}
