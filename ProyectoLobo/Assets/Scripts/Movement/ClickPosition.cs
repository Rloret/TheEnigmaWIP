using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ClickPosition : MonoBehaviour {

    public Vector2 clickPos;
    public BehaviourAdder clickController;
    public LayerMask avoidCollisionWith;
    public float MinDistanceOpenMenu =100f;

	public GameObject[] menus;

    private BehaviourAdder.WeightedBehaviours[] WeightedPlayerBehavioursArray;
    private BehaviourAdder behaviourController;

    private bool menuOpened = false;

    void Start()
    {
		behaviourController = GameObject.FindGameObjectWithTag("GameController").GetComponent<BehaviourAdder>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //Si clicamos
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(mouseWorldPos.x, mouseWorldPos.y); //Esto coje la posición en la pantalla

            Vector2 dir = Vector2.zero;
			RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, dir, 10000, avoidCollisionWith);

            //int counter = 0;

            //if (hit.collider != null)
            // {
            string hitinfo = "";
            foreach (var h in hit)
            {
                if (h.collider.gameObject.tag == "IA") menuOpened = true;
                else menuOpened = false;

                hitinfo+=h.collider.gameObject.name;
                GameObject aux = h.collider.gameObject;
                DetermineAction(this.gameObject, aux);
            }
              // Debug.Log(hitinfo);
           // }
        }
    }


    public void DetermineAction(GameObject behaviorReceiber, GameObject aux)
    {
        if (aux.tag == "IA")
        {
			aux.GetComponent<VisibilityConeCycleIA>().enabled = false;

            if (Mathf.Abs(Vector3.Distance(behaviorReceiber.transform.position, aux.transform.position)) <= MinDistanceOpenMenu)
            {
                Debug.Log("estan cerca, abro menu");
                behaviourController.openConversationMenu(behaviorReceiber, aux);

            }
            else
            {
                Debug.Log("esta lejos, me acercare");

                BehaviourAdder.WeightedBehaviours pursue = new BehaviourAdder.WeightedBehaviours(BehaviourAdder.SteeringBehaviour.PURSUE, 0.7f, 0);
                BehaviourAdder.WeightedBehaviours avoidWall = new BehaviourAdder.WeightedBehaviours(BehaviourAdder.SteeringBehaviour.AVOIDWALL, 1f, 0);
                BehaviourAdder.WeightedBehaviours face = new BehaviourAdder.WeightedBehaviours(BehaviourAdder.SteeringBehaviour.FACE, 1f, 0);
                WeightedPlayerBehavioursArray = new BehaviourAdder.WeightedBehaviours[] { pursue, avoidWall, face };
                behaviourController.ActionWhenClick(behaviorReceiber, aux); //if IA character is too far, we need to arrive/pursue him in order to be near, so we can talk to him
            }
        }

        else if (aux.tag == "MenuButton")
        {
            Debug.Log("menuButton");
            aux.GetComponent<ButtonAction>().Action();
        }
        else if (aux.tag == "Player")
        {
            Debug.Log("Pa k clikas en el player, jaja salu2");
        }
        else if (aux.layer == 8 || aux.layer == 10)
        {
            //Esto es para que al clickar en un muro o en los muebles no haga nada
        }
        else
        { // target is floor

            if (!menuOpened)
            {
                foreach (var m in menus)
                {
                    if (m.activeSelf)
                        m.SetActive(false);
                }
            }

            string[] behaviours = { "Arrive", "AvoidWall", "LookWhereYouAreGoing" };
            float[] weightedBehavs = { 0.7f, 1, 1 };
            behaviourController.addBehavioursOver(behaviorReceiber, aux, behaviours, weightedBehavs);
            //ActionWhenClick(behaviorReceiber, aux); //if IA character is too far, we need to arrive/pursue him in order to be near, so we can talk to him
        }

    }


}