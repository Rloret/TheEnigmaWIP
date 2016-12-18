using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisibilityConeCycle : MonoBehaviour {
    [Tooltip("Angle of raycastSweep")]
    [Range(0,90)]
    public float Angle;
    [Range(2, 10)]
    public int CuantityOfRays;

    public LayerMask layers;

    private Vector2 source;
    private Vector2 vi;
    private VisibilityCone Cone;

    private int colorauxIterator=0;

    private struct hitInfo
    {
        public bool collision;
        public Vector2 pointOfCollision;
        public Collider2D collisionCollider;

        public hitInfo(bool collision,Vector2 pointOfCollision, Collider2D collisionCollider)
        {
            this.collision = collision;
            this.pointOfCollision = pointOfCollision;
            this.collisionCollider = collisionCollider;
        }
    }

    private LinkedList<Vector2> VisibleConePoints;
    private List<hitInfo> hitsList;
    private List<GameObject> visibleGameobjects;
    private List<GameObject> Objects;


    private float AngleRads;
    private float Radius;
    private float lastRadius;


    // Use this for initialization
    void Start()
    {
        AngleRads = Mathf.Deg2Rad * Angle;
        Cone = this.GetComponent<VisibilityCone>();
        Radius = Cone.Radius;
        hitsList = new List<hitInfo>();
        VisibleConePoints = new LinkedList<Vector2>();
        visibleGameobjects = new List<GameObject>();
        visibleGameobjects.Capacity = 50;
        hitsList.Capacity = (int)Angle;


        Objects = VisibleElements.visibleGameObjects;

        

    }

    public void changeRadius(float newR)
    {
        //Debug.Log("Me llega: " + newR);
        lastRadius = Radius;
        Radius *= newR;
        //Debug.Log("He cambiado el radio = " + Radius);
    }


    /* void OnDrawGizmos()
     {
         foreach (var item in VisibleConePoints)
         {
             Gizmos.DrawSphere(item, 5);
         }
     }*/
    // Update is called once per frame
    void Update()
    {

        vi = this.transform.up;
        source = this.transform.position;
        AngleRads = Mathf.Deg2Rad * Angle;

        sweepRayCastAll(source, vi, AngleRads);
        determineVisiblePoints();
        sendVisiblePointsToCone();

     //   checkObjectsWithinCone();

        hitsList.Clear();
        VisibleConePoints.Clear();
    }
    


    private void sweepRayCastAll(Vector2 From, Vector2 Up, float AngleInRads)
    {
        float scaler;
        float beta = Mathf.Atan2(Up.y,Up.x);
        int increment = (int)(Angle / CuantityOfRays);
        
        Vector2 viS;
        
        for (int i = 0; i <= Angle; i += increment)
        {
            scaler = i / Angle;
            vi= rotateVectorTowards(beta, AngleRads, (scaler * Angle) * Mathf.Deg2Rad, Radius) + source;
            viS = (vi -source).normalized;
            ThrowRayCast(source, viS, Radius, vi);
        }


    }

    private void ThrowRayCast(Vector2 from, Vector2 direction, float distance,Vector2 raycastvector)
    {

        RaycastHit2D hit = Physics2D.Raycast(from, direction, distance, layers);
        hitInfo hitInformation;
        Debug.DrawLine(from, direction*distance + from,Color.red *(1-colorauxIterator/Angle) );

        if (hit)
        {

            raycastvector = new Vector2(hit.point.x, hit.point.y);
           
        }
        hitInformation = new hitInfo(true, raycastvector, hit.collider);
        hitsList.Add(hitInformation);
    }

    private void determineVisiblePoints()
    {
        hitInfo currentHitinfo, lastHitInfo =hitsList[0];
        Vector2 firstCollision =lastHitInfo.pointOfCollision;
        Vector2[] currentColliderVertices;
        VisibleConePoints.AddFirst(firstCollision);

        LinkedListNode<Vector2> currentpoint;
        LinkedListNode<Vector2> previouspoint;
        Collider2D colliderBetweenpoints = new Collider2D();



        for (int i = 1; i < hitsList.Count; i++)
        {
            currentHitinfo = hitsList[i];

            currentpoint = VisibleConePoints.AddLast(lastHitInfo.pointOfCollision);
            //si uno de los dos 
            if (lastHitInfo.collisionCollider != currentHitinfo.collisionCollider)
            {


                if (currentpoint.Previous != null)
                {
                    previouspoint = currentpoint.Previous;

                    colliderBetweenpoints = currentHitinfo.collisionCollider == null ? lastHitInfo.collisionCollider : currentHitinfo.collisionCollider;

                }

            }
            else
            {

                Vector2 direction = currentHitinfo.pointOfCollision - lastHitInfo.pointOfCollision;
                RaycastHit2D hit = Physics2D.Raycast(lastHitInfo.pointOfCollision, direction.normalized, direction.magnitude, layers);

                if (hit)
                {
                    colliderBetweenpoints = hit.collider;
                }

            }
            if (colliderBetweenpoints != null)
            {
                currentColliderVertices = colliderBetweenpoints.gameObject.GetComponent<SpriteRenderer>().sprite.vertices;
                addVertexInTriangle(currentColliderVertices, colliderBetweenpoints, lastHitInfo, currentHitinfo, currentpoint);
            }
            lastHitInfo = currentHitinfo;

        }
       VisibleConePoints.AddLast(hitsList[hitsList.Count-1].pointOfCollision);
    }


    private Vector2 rotateVectorTowards(float beta, float alpha, float i, float radi)
    {
        float alphai = beta - (alpha / 2f) + i;
        return new Vector2(Mathf.Cos(alphai), Mathf.Sin(alphai)) * radi;

    }

    bool isInTriangleABC(Vector2 s, Vector2 a, Vector2 b, Vector2 c)
    {
        float as_x =( s.x - a.x);
        float as_y =( s.y - a.y);

        bool s_ab = (b.x - a.x) * as_y - (b.y - a.y) * as_x > 0;

        if ((c.x - a.x) * as_y - (c.y - a.y) * as_x > 0 == s_ab) return false;

        if ((c.x - b.x) * (s.y - b.y) - (c.y - b.y) * (s.x - b.x) > 0 != s_ab) return false;

        return true;
    }

    private void  addVertexInTriangle(Vector2[]currentColliderVertices, Collider2D colliderBetweenpoints,hitInfo lastHitInfo, hitInfo currentHitinfo,LinkedListNode<Vector2> currentpoint)
    {
        foreach (var vertex in currentColliderVertices)
        {
            Vector2 vertPosition = (Vector2)colliderBetweenpoints.gameObject.transform.position + vertex;

            if (isInTriangleABC(vertPosition, lastHitInfo.pointOfCollision, currentHitinfo.pointOfCollision, source))
            {
                VisibleConePoints.AddAfter(currentpoint, vertPosition);
            }

        }
    }

    private void sendVisiblePointsToCone()
    {
        List<Vector3> Visiblepoints = new List<Vector3>();
        foreach (var point in VisibleConePoints)
        {
            Visiblepoints.Add(new Vector3(point.x, point.y, this.transform.position.z));
        }
        Cone.addCollisionPoints(Visiblepoints);
    }

   /* private void checkObjectsWithinCone()
    {
        Vector2 A, B, C;
        A = VisibleConePoints.First.Value;
        B = source;
        C = VisibleConePoints.Last.Value;
        foreach (var singleObject in Objects)
        {
            if (singleObject != this.gameObject)
            {
                Debug.DrawLine(A, B);
                Debug.DrawLine(B, C);
                Debug.DrawLine(C, A);
                if (isInTriangleABC(singleObject.transform.position, A, B, C))
                {
                    visibleGameobjects.Add(singleObject);
                    //singleObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, Random.Range(0f, 1f));
                }
            }
        }

    }*/
}
