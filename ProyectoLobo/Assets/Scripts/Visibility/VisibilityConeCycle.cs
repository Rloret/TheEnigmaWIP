using UnityEngine;
using System.Collections;

public class VisibilityConeCycle : MonoBehaviour {
    [Tooltip("Angle of raycastSweep")]
    [Range(0,90)]
    public float Angle;
    private int Radius;

    private Vector3 source,a,b,pointAtRadius;
    private float[] cosinesAlpha;
    private VisibilityCone Cone;
    private float cosmedios, sinmedios, AngleRads;

    // Use this for initialization
    void Start () {
        cosinesAlpha = new float[(int)Angle];
        for (int i = 0; i < cosinesAlpha.Length; i++)
        {
            cosinesAlpha[i] = Mathf.Cos(i);
        }
        Cone = this.GetComponent<VisibilityCone>();
        Radius = VisibilityCone.Radius;


	}
	void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointAtRadius, 10);
        Gizmos.DrawWireSphere(a, 10);
        Gizmos.DrawWireSphere(b, 10);
    }
	// Update is called once per frame
	void Update () {
        AngleRads = Angle * Mathf.Deg2Rad;
        cosmedios = Mathf.Cos(AngleRads / 2);
        sinmedios = Mathf.Sin(AngleRads / 2);
        source = this.transform.position;
        pointAtRadius = this.transform.position  +this.transform.up * Radius;

        float con,opu,hip;
        con = Radius;
        hip = con / cosmedios;
        opu = hip * sinmedios;

        a = this.transform.right * opu;
        b = this.transform.right * -opu;
       // Debug.Log("Y es" + y + " X: " + x + " hip: " + hip);
        a = new Vector3(a.x +pointAtRadius.x, a.y+ pointAtRadius.y, this.transform.position.z);
        b = new Vector3(b.x + pointAtRadius.x, b.y + pointAtRadius.y, this.transform.position.z);

        Vector3[] auxVector = { a, b };
        Cone.addCollisionPoints(auxVector);

    }
}
