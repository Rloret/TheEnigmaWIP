using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickPosition : MonoBehaviour {

    public Vector2 clickPos;
    public LayerMask avoidCollisionWith;

    public float MinDistanceOpenMenu =100f;

	public GameObject[] menus;

    private BehaviourAdder.WeightedBehaviours[] WeightedPlayerBehavioursArray;
    private BehaviourAdder behaviourController;
    private AgentPositionController movementScript;
    private Vector2 lastLinear;
    private ObjectHandler objectHandlerPlayer;

    private bool menuOpened = false;
    private bool clickedOnTile = false;
    private bool attack = false;

    void Start()
    {
		behaviourController = GameObject.FindGameObjectWithTag("GameController").GetComponent<BehaviourAdder>();
        movementScript = GetComponent<AgentPositionController>();
        objectHandlerPlayer = this.GetComponent<ObjectHandler>();

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //Si clicamos
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(mouseWorldPos.x, mouseWorldPos.y); //Esto coje la posición en la pantalla

            Vector2 dir = Vector2.zero;
			RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, dir, 10000, avoidCollisionWith);

           
            string hitinfo = "";
            foreach (var h in hit)
            {
                GameObject GOclicked = h.collider.gameObject;

                //Debug.Log("Estoy pinchando en: " + h.collider);
                if (GOclicked.tag== "IA")
                {
                    PersonalityBase personality = this.GetComponent<PersonalityBase>();

                    if (personality.isMonster)
                    {
                        if (Vector3.Distance(this.transform.position, GOclicked.transform.position) < 50)
                        {
                            attack = true;
                        }
                    }
                    else
                    {
                        menuOpened = true;
                        attack = false;
                    }
                   
                }
                else
                {
                    menuOpened = false;
                    if (h.collider.gameObject.tag == "Object")
                    {
                        objectHandlerPlayer.desiredObject = h.collider.gameObject;
                        //Debug.Log("Deseo: " + objectHandlerPlayer.desiredObject);
                    }
                }


                hitinfo+=h.collider.gameObject.name;
                GameObject aux = h.collider.gameObject;
                DetermineAction(this.gameObject, aux);
            }
        }
    }

    public void LateUpdate()
    {
        if (clickedOnTile)
        {
            movementScript.linearVelocity =lastLinear/2;
            clickedOnTile = false;
        }
    }
    public void DetermineAction(GameObject behaviorReceiber, GameObject aux)
    {
        if (aux.tag == "IA" )

        {
            
			aux.GetComponent<VisibilityConeCycleIA>().enabled = false;

            if (Mathf.Abs(Vector3.Distance(behaviorReceiber.transform.position, aux.transform.position)) <= MinDistanceOpenMenu && menuOpened)
            {
               // Debug.Log("estan cerca, abro menu");
                behaviourController.openConversationMenu(behaviorReceiber, aux);

            }
            else
            {
//                Debug.Log("esta lejos, me acercare");
                clickedOnTile = true;
                lastLinear = movementScript.linearVelocity ;
              
				string[] behaviours = { "Arrive", "AvoidWall", "LookWhereYouAreGoing" };
				float[] weightedBehavs = { 1f, 5f, 1 };

                GameObject[] targets = { aux, aux, aux };
				behaviourController.addBehavioursOver(this.gameObject,targets,behaviours,weightedBehavs); //if IA character is too far, we need to arrive/pursue him in order to be near, so we can talk to him

                if (attack)
                {
                    aux.GetComponent<PersonalityBase>().interactionFromOtherCharacter = ActionsEnum.Actions.ATTACK;
                    ActionAttack a = gameObject.AddComponent<ActionAttack>();
                    a.targetAttack = aux;
                    a.triggered = true;
                    a.DoAction();

                    DecisionTreeReactionAfterInteraction reaction = aux.GetComponent<DecisionTreeReactionAfterInteraction>();
                    if (reaction != null)
                    {
                        DestroyImmediate(reaction);
                    }

                    DecisionTreeNode[] oldNodes = aux.GetComponents<DecisionTreeNode>();
                    foreach (DecisionTreeNode n in oldNodes)
                    {
                        DestroyImmediate(n);
                    }

                    reaction = aux.AddComponent<DecisionTreeReactionAfterInteraction>();

                   // Debug.Log("h collider es :" + aux.name + " reaction es " + reaction);
                    reaction.target = this.gameObject;
                    attack = false;
                   // Debug.Log("reaction target es :" + reaction.target);
                }

            }
        }

        else if (aux.tag == "MenuButton")
        {
            aux.GetComponent<ButtonAction>().Action();
        }
        else if (aux.tag == "Player")
        {
            //Debug.Log("Pa k clikas en el player, jaja salu2");
        }
        else if (aux.layer == 8 || aux.layer == 10)
        {
            //Esto es para que al clickar en un muro o en los muebles no haga nada
        }
        else
        { // target is floor


            string[] behaviours = { "Arrive", "AvoidWall", "LookWhereYouAreGoing" };
            float[] weightedBehavs = { 0.7f, 1, 1 };
            GameObject[] targets = { aux, aux, aux,};
            behaviourController.addBehavioursOver(behaviorReceiber, targets, behaviours, weightedBehavs);
            //ActionWhenClick(behaviorReceiber, aux); //if IA character is too far, we need to arrive/pursue him in order to be near, so we can talk to him
        }

    }


}