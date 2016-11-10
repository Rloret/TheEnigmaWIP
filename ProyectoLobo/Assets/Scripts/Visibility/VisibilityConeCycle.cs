using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisibilityConeCycle : MonoBehaviour {
    [Tooltip("Angle of raycastSweep")]
    [Range(0,90)]
    public float Angle;
    [Range(2, 30)]
    public int CuantityOfRays;
    public LayerMask layers;

    private Vector3 source,a,b,pointAtRadius;
    private Vector3 vi;
    private VisibilityCone Cone;
    private Ray2D ray;
    private List<RaycastHit2D> hitsList;
    private List<Vector3> VisibleConePoints;

    private float cosmedios, sinmedios, AngleRads,beta,alpha;
    private int Radius;


    // Use this for initialization
    void Start()
    {
        AngleRads = Mathf.Deg2Rad * Angle;
        Cone = this.GetComponent<VisibilityCone>();
        Radius = VisibilityCone.Radius;
        hitsList = new List<RaycastHit2D>();
        VisibleConePoints = new List<Vector3>();
        hitsList.Capacity = 100;
        VisibleConePoints.Capacity = 100;

    }
    void OnDrawGizmos()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vi =this.transform.up;
        source = this.transform.position;
        AngleRads = Mathf.Deg2Rad * Angle;
        hitsList.Clear();
        VisibleConePoints.Clear();
        sweepRayCastAll(source, vi, AngleRads);
        pointAtRadius = this.transform.position + (Vector3)vi * Radius;

        //float beta = Mathf.Atan2(vi.y, vi.x);
        
       /* a = (Vector3)rotateVectorTowards(beta, AngleRads, AngleRads/2, Radius) + this.transform.position;
        ThrowRayCastAll(source, (a - source).normalized, Radius);
       
        b = (Vector3)rotateVectorTowards(beta, AngleRads, AngleRads, Radius) + this.transform.position;*/


    }



    private void sweepRayCastAll(Vector3 From, Vector3 Up, float AngleInRads)
    {
        float scaler;
        float beta = Mathf.Atan2(Up.y,Up.x);
        int increment = (int)(Angle / CuantityOfRays);
        
        Vector3 viS;
        
        for (int i = 0; i < Angle; i += increment)
        {
            scaler = i / Angle;
            vi= (Vector3)rotateVectorTowards(beta, AngleRads, (scaler * Angle) * Mathf.Deg2Rad, Radius) + source;
            viS = (vi -source).normalized;
            vi = ThrowRayCastAll(source, viS, Radius,vi);
            VisibleConePoints.Add(vi);
        }

        vi = (Vector3)rotateVectorTowards(beta, AngleRads, AngleInRads, Radius) + source;
        viS = (vi - source).normalized;
        vi = ThrowRayCastAll(source, viS, Radius, vi);
        VisibleConePoints.Add(vi);
        Debug.DrawLine(source, vi, Color.red);


        Cone.addCollisionPoints(VisibleConePoints);

    }

    private Vector3 ThrowRayCastAll(Vector3 from, Vector3 direction, float distance,Vector3 raycastvector)
    {
        RaycastHit2D hit = Physics2D.Raycast(from, direction, distance, layers);
        Debug.DrawLine(from, direction*distance + from );

        if (hit)
        {
            //Debug.DrawLine(from,direction,Color.red);

            raycastvector= new Vector3(hit.point.x, hit.point.y, from.z);
            //hitsList.AddRange(hits);

        }

        return raycastvector;
    }


    private Vector2 rotateVectorTowards(float beta, float alpha, float i, float radi)
    {
        float alphai = beta - (alpha / 2f) + i;
        return new Vector2(Mathf.Cos(alphai), Mathf.Sin(alphai)) * radi;

    }
}
