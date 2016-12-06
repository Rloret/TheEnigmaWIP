﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisibilityConeCycleIA : MonoBehaviour {

    public List<GameObject> visibleGameobjects;
    public LayerMask layers;

    private Vector2 source;
    private Vector2 vi;

    private LinkedList<Vector2> VisibleConePoints;

    private List<GameObject> Objects;

    private float AngleRads,Angle=70;
    private int Radius;
    private int CuantityOfRays = 1;

    private DecisionTarget decisionTargetScript;
    private BehaviourAdder movementController;
    private ObjectHandler objecthand;
    private DecisionTreeISeeSomeoneWhatShouldIDo whatToDoScript;

    // Use this for initialization
    void Start()
    {
        AngleRads = Mathf.Deg2Rad * Angle;

        Radius =200;

        VisibleConePoints = new LinkedList<Vector2>();
        visibleGameobjects = new List<GameObject>();
        visibleGameobjects.Capacity = 50;
        Objects = new List<GameObject>();

        decisionTargetScript = this.GetComponent<DecisionTarget>();
        movementController = GameObject.FindGameObjectWithTag("GameController").GetComponent<BehaviourAdder>();
        whatToDoScript = this.GetComponent<DecisionTreeISeeSomeoneWhatShouldIDo>();
        Objects = VisibleElements.visibleGameObjects;
        objecthand = this.GetComponent<ObjectHandler>();
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

        checkObjectsWithinCone();

        VisibleConePoints.Clear();

    }



    private void sweepRayCastAll(Vector2 From, Vector2 Up, float AngleInRads)
    {
        float scaler;
        float beta = Mathf.Atan2(Up.y, Up.x);
        int increment = (int)(Angle / CuantityOfRays);

        Vector2 viS;

        for (int i = 0; i <= Angle; i += increment)
        {
            scaler = i / Angle;
            vi = rotateVectorTowards(beta, AngleRads, (scaler * Angle) * Mathf.Deg2Rad, Radius) + source;
            viS = (vi - source);
            VisibleConePoints.AddLast( ThrowRayCast(source, viS.normalized, viS.magnitude, vi));
                
        }
        


    }
    private Vector2 ThrowRayCast(Vector2 from, Vector2 direction, float distance, Vector2 raycastvector)
    {

        RaycastHit2D hit = Physics2D.Raycast(from, direction, distance, layers);
       

        if (hit)
        {

            raycastvector = new Vector2(hit.point.x, hit.point.y);

        }
        return raycastvector;
    }
    


    private Vector2 rotateVectorTowards(float beta, float alpha, float i, float radi)
    {
        float alphai = beta - (alpha / 2f) + i;
        return new Vector2(Mathf.Cos(alphai), Mathf.Sin(alphai)) * radi;

    }

    bool isInTriangleABC(Vector2 s, Vector2 a, Vector2 b, Vector2 c)
    {
        float as_x = (s.x - a.x);
        float as_y = (s.y - a.y);

        bool s_ab = (b.x - a.x) * as_y - (b.y - a.y) * as_x > 0;

        if ((c.x - a.x) * as_y - (c.y - a.y) * as_x > 0 == s_ab) return false;

        if ((c.x - b.x) * (s.y - b.y) - (c.y - b.y) * (s.x - b.x) > 0 != s_ab) return false;

        return true;
    }



    private void checkObjectsWithinCone()
    {
        Vector2 A, B, C;
        A = VisibleConePoints.First.Value;
        B = source;
        C = VisibleConePoints.Last.Value;
        foreach (var singleObject in Objects)
        {
            Debug.DrawLine(A, B);
            Debug.DrawLine(B, C);
            Debug.DrawLine(C, A);
            if (isInTriangleABC(singleObject.transform.position, A, B, C))
            {
                if (singleObject!=this.gameObject)
                visibleGameobjects.Add(singleObject);
                //singleObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, Random.Range(0f, 1f));
            }
        }

        if (visibleGameobjects.Count > 0)
        {

            GameObject priorityGO =  decisionTargetScript.ChooseTarget(visibleGameobjects, this.gameObject);
            visibleGameobjects.Clear();
            if (priorityGO == null)
            {
                moveRandomly(A, C);
            }
            else
            {
                string[] behaviours = new string[3];
                if (priorityGO.tag == "IA")
                {
                    whatToDoScript.target = priorityGO;
                    behaviours = new string[3] { "Pursue", "AvoidWall", "Face" };
                }
                else
                {
                    objecthand.setDesiredGameObject(priorityGO);
                    behaviours = new string[3]{ "Arrive", "AvoidWall", "Face" };


                }
                float[] weightedBehavs = { 0.7f, 1, 1 };
                movementController.addBehavioursOver(this.gameObject, priorityGO.transform.position, behaviours, weightedBehavs);

            }
        }
        else
        {
            if (this.GetComponent<AgentPositionController>() == null) {
                Debug.Log("no tengo control de movimiento y lo añado");
                this.gameObject.AddComponent<AgentPositionController>();
            }
            moveRandomly(A, C);
        }

    }

    private void moveRandomly(Vector2 A, Vector2 C)
    {
        Vector3 AC = C - A;
        int random = Random.Range(1, 4);
        Vector3 percentageAC = AC / (float)random;
        Vector3 target = A + (Vector2)percentageAC;

        string[] behaviours = { "Arrive", "AvoidWall", "LookWhereYouAreGoing" };
        float[] weightedBehavs = { 0.7f, 1, 1 };
        movementController.addBehavioursOver(gameObject, target, behaviours, weightedBehavs);
    }
}

