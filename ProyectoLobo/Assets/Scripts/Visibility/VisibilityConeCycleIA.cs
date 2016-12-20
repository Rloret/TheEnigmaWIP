using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisibilityConeCycleIA : MonoBehaviour
{

    public List<GameObject> visibleGameobjects;
    public LayerMask layers;

    private Vector2 source;
    private Vector2 vi;

    private LinkedList<Vector2> VisibleConePoints;
    //public Vector2 A, B, C;

    private List<GameObject> Objects;

    private float AngleRads, Angle = 70;
    private float Radius;
    private float lastRadius = 200;
    private int CuantityOfRays = 1;
    public float scalingFactorofVision = 1;

    private DecisionTarget decisionTargetScript;
    private BehaviourAdder movementController;
    private ObjectHandler objecthand;
    private DecisionTreeISeeSomeoneWhatShouldIDo whatToDoScript;
    private List<GameObject> targetsAux;
    private AIPersonality personality;

    public GameObject ghostTarget;
    private GameObject priorityGO;
    private GameObject rememberedObject;

    public bool IDecided = false;

    public bool stuckedAI = false;



    // Use this for initialization
    void Start()
    {
        AngleRads = Mathf.Deg2Rad * Angle;

        Radius = 200;

        VisibleConePoints = new LinkedList<Vector2>();
        visibleGameobjects = new List<GameObject>();
        targetsAux = new List<GameObject>();
        visibleGameobjects.Capacity = 50;
        Objects = new List<GameObject>();
        whatToDoScript = this.GetComponent<DecisionTreeISeeSomeoneWhatShouldIDo>();
        decisionTargetScript = this.GetComponent<DecisionTarget>();
        movementController = GameObject.FindGameObjectWithTag("GameController").GetComponent<BehaviourAdder>();
        whatToDoScript = this.GetComponent<DecisionTreeISeeSomeoneWhatShouldIDo>();
        Objects = VisibleElements.visibleGameObjects;
        objecthand = this.GetComponent<ObjectHandler>();
        personality = this.GetComponent<AIPersonality>();
        rememberedObject = new GameObject();
        rememberedObject.name = "rememberedObjectPosition";

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
        //Debug.Log("Radio = " + Radius);
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
            VisibleConePoints.AddLast(ThrowRayCast(source, viS.normalized, viS.magnitude, vi));

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
        string objectINeedRemember = ObjectINeedRemember();
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
                if (singleObject != this.gameObject)
                    visibleGameobjects.Add(singleObject);
                //singleObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, Random.Range(0f, 1f));
            }
        }

        if (visibleGameobjects.Count > 0)
        {

            if (objectINeedRemember != "None")
            {
                // = RememberedObject(objectINeedRemember);
                RoomMemory rM = this.gameObject.GetComponent<RoomMemory>();
                if (personality.myMemory.objectWithinRoom.ContainsKey(objectINeedRemember))
                    rM.SetDestinyRoom(personality.myMemory.objectWithinRoom[objectINeedRemember]);                
                //Debug.Log("Quiero ir a por el " + objectINeedRemember + ", que está en " + personality.myMemory.objectWithinRoom[objectINeedRemember]);
                priorityGO = RememberedObjectInARoom(objectINeedRemember);

                //Debug.Log("necesito el objeto: " + priorityGO);
            }
            else
            {
                priorityGO = decisionTargetScript.ChooseTarget(visibleGameobjects, this.gameObject);
                //Debug.Log("SOY: "+ this.gameObject.name +" PRIORITY GO ES " + priorityGO);

            }

            visibleGameobjects.Clear();
            //Debug.Log ("priority object= " + priorityGO.name);
            if (priorityGO == null) // no ha visto nada
            {
                moveRandomly(A, C);
            }
            else
            {
				if (priorityGO.tag == "IA"  ) //lo más prioritario es una persona
                {
                    #region deccidingReg
                    if (!IDecided)
                    {
                        //  Debug.Log("veo una Ia voy a decidir, soy " + this.name );
                        IDecided = true;

						priorityGO.GetComponent<VisibilityConeCycleIA>().enabled = false;

                        if (whatToDoScript == null)
                        {
                            whatToDoScript = this.gameObject.AddComponent<DecisionTreeISeeSomeoneWhatShouldIDo>();
                        }
                        else
                        {
                            DestroyImmediate(whatToDoScript);

							DecisionTreeNode[] oldNodes= this.gameObject.GetComponents<DecisionTreeNode>();
							foreach(DecisionTreeNode n in oldNodes){
								DestroyImmediate(n);
							}

                            whatToDoScript = this.gameObject.AddComponent<DecisionTreeISeeSomeoneWhatShouldIDo>();


                        }

                        whatToDoScript.target = priorityGO;

                        string[] behaviours = new string[3] { "Pursue", "AvoidWall", "Face" };
                        float[] weightedBehavs = { 0.7f, 1, 1 };
                        GameObject[] targets = { priorityGO, priorityGO, priorityGO };
                        movementController.addBehavioursOver(this.gameObject, targets, behaviours, weightedBehavs);

                    }
                    #endregion decidingReg
                }
				else if (priorityGO.tag == "Player"  ) //lo más prioritario es una persona
				{
					

					#region deccidingReg
					if (!IDecided) {
						//Debug.Log ("veo al player");
						//  Debug.Log("veo una Ia voy a decidir, soy " + this.name );
						IDecided = true;

						// Debug.Log("yo " + this.gameObject.transform + "veo a  " + priorityGO + " (target)");
						if (whatToDoScript == null)
						{
							whatToDoScript = this.gameObject.AddComponent<DecisionTreeISeeSomeoneWhatShouldIDo>();
						}
						else {
							DestroyImmediate(whatToDoScript);

							whatToDoScript = this.gameObject.AddComponent<DecisionTreeISeeSomeoneWhatShouldIDo>();


						}

						whatToDoScript.target = priorityGO;

						string[] behaviours = new string[3] { "Pursue", "AvoidWall", "Face" };
						float[] weightedBehavs = { 0.7f, 1, 1 };
                        GameObject[] targets = { priorityGO, priorityGO, priorityGO };
						movementController.addBehavioursOver(this.gameObject,targets , behaviours, weightedBehavs);

					}
					#endregion decidingReg
				}
                else //lo más prioritario es un objeto
                {

                    if (VisibleElements.visibleGameObjects.Contains(priorityGO))
                    {
                        //Debug.Log("priorityGo es: " + priorityGO);
                        if (priorityGO.tag == "Object")
                            objecthand.desiredObject = priorityGO;
                        GameObject targetGO = (GameObject)Instantiate(ghostTarget, priorityGO.transform.position, Quaternion.identity);
                        targetsAux.Add(targetGO);
                        // Debug.Log("busco el objeto");
                        //Debug.Log ("Deseo " + objecthand.desiredObject);
                        //objecthand.setDesiredGameObject(priorityGO);
                        string[] behaviours = new string[3] { "Arrive", "AvoidWall", "LookWhereYouAreGoing" };
                        float[] weightedBehavs = { 0.7f, 1, 1 };
                        GameObject[] targets = { targetGO, targetGO, targetGO };
                        movementController.addBehavioursOver(this.gameObject, targets, behaviours, weightedBehavs);
                        DeleteTargetAux();

                    }
                   else
                       moveRandomly(A, C);
                }
            }
        }
        else
        {
            if (this.GetComponent<AgentPositionController>() == null)
            {
                Debug.Log("no tengo control de movimiento y lo añado");
                this.gameObject.AddComponent<AgentPositionController>();
            }
            if (objectINeedRemember != "None")
            {
                //priorityGO = RememberedObject(objectINeedRemember);
                RoomMemory rM = this.gameObject.GetComponent<RoomMemory>();
                if(personality.myMemory.objectWithinRoom.ContainsKey(objectINeedRemember))
                    rM.SetDestinyRoom(personality.myMemory.objectWithinRoom[objectINeedRemember]);
                //Debug.Log("Quiero ir a por el " + objectINeedRemember + ", que está en " + personality.myMemory.objectWithinRoom[objectINeedRemember]);
                priorityGO = RememberedObjectInARoom(objectINeedRemember);

                if (priorityGO != null)
                {
                    if (priorityGO.tag == "Object")
                        objecthand.desiredObject = priorityGO;
                    //Debug.Log("Deseo " + objecthand.desiredObject);
                    GameObject targetGO = (GameObject)Instantiate(ghostTarget, priorityGO.transform.position, Quaternion.identity);
                    targetsAux.Add(targetGO);
                    string[] behaviours = new string[3] { "Arrive", "AvoidWall", "LookWhereYouAreGoing" };
                    float[] weightedBehavs = { 0.7f, 1, 1 };
                    GameObject[] targets = { targetGO, targetGO, targetGO };
                    movementController.addBehavioursOver(this.gameObject, targets, behaviours, weightedBehavs);
                    DeleteTargetAux();

                }
                else
                    moveRandomly(A, C);

            }
            else
            {
                moveRandomly(A, C);
            }

        }

    }

    public void moveRandomly(Vector2 A, Vector2 C)
    {
        int random;

        Vector3 AC = C - A;
        if (Time.frameCount % 30 == 0 && !stuckedAI)
        {
            random = Random.Range(1, 8);
            Vector3 percentageAC = AC / (float)random;
            Vector3 targetPosition = A + (Vector2)percentageAC;
            GameObject targetGO = (GameObject)Instantiate(ghostTarget, targetPosition, Quaternion.identity);
            targetsAux.Add(targetGO);

            string[] behaviours = { "Arrive", "AvoidWall", "LookWhereYouAreGoing" };
            float[] weightedBehavs = { 0.4f, 1, 1 };
            GameObject[] targets = { targetGO, targetGO, targetGO};
            movementController.addBehavioursOver(this.gameObject, targets, behaviours, weightedBehavs);

            DeleteTargetAux();
        }

    }

    private void DeleteTargetAux()
    {
        if (targetsAux.Count > 1)
        {
            GameObject t = targetsAux[0];
            targetsAux.RemoveAt(0);
            DestroyImmediate(t);
        }
        //Debug.Log ("Eliminando target aux");
    }

    private string ObjectINeedRemember() //completar
    {
        if (personality.health < 20)
        {
           // Debug.Log("Necesito curarme");
            return "Medicalaid";
        }
        else
            return "None";
    }

    /*private GameObject RememberedObject(string obj)
    {
        
        if (obj == "Medicalaid")
        {
            GameObject room = personality.myMemory.SearchRoomInMemory("Medicalaid");
            //Vector3? rememberedObjectPosition = personality.myMemory.SearchInMemory("Medicalaid"); Antiguo!
            if (room != null)
            {

                rememberedObject.transform.position = new Vector2(rememberedObjectPosition.Value.x, rememberedObjectPosition.Value.y);
                rememberedObject.name = obj;

            }
            else
                rememberedObject = null;

        }
        //return rememberedObject;
    }*/

    private GameObject RememberedObjectInARoom(string obj)
    {
        GameObject room;
        if (obj == "Medicalaid")
        {
            room = personality.myMemory.SearchRoomInMemory("Medicalaid");
            //Debug.Log("Mi siguiente destino es: " + room);
        }
        else
            return null;

        return room;
    }
}

