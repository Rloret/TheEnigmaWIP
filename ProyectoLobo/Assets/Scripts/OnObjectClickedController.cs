using UnityEngine;
using System.Collections;

public class OnObjectClickedController : MonoBehaviour {

    public float MinDistanceOpenMenu = 40f;
    public LayerMask mask;
    private ConversationMenuController menuController;

    [System.Serializable]

    public struct WeightedBehaviours
    {
        [SerializeField]
        public SteeringBehaviour Behaviour;
        [SerializeField]
        [Range(0, 1)]
        [Tooltip("Indica el multipliocador de aceleración o peso. Es decir este comportamiento tendra un weight de influencia. \n"
            + "Por ejemplo:\n si es seek al 0.5f junto con un face al 1, la aceleración lineal será del 50% mientras que la aceleracion angular sera del 100%."
            + "Esta hecho para combinar comportamientos con el collision avoiding sobretodo.")]
        public float weight;
        [Tooltip("El grupo de prioridad al que se añadirá")]
        public int priority;

        public  WeightedBehaviours(SteeringBehaviour behav,float weight,int priority)
        {
            Behaviour = behav;
            this.weight = weight;
            this.priority = 0;
        }


    }
    [Tooltip("Este array sirve para almacenar todos los comportamientos que se van a ejecutar a la vez y sus prioridades.")]
    public WeightedBehaviours[] WeightedBehavioursArray;

    public enum SteeringBehaviour
    {
        NOTHING, SEEK, FLEE, ARRIVE, LEAVE, PURSUE, EVADE, ALIGN, FACE, WANDER , AVOIDWALL ,LOOKWHEREYOUAREGOING
    };


    // Use this for initialization
    void Start () {
        menuController = this.gameObject.GetComponent<ConversationMenuController>();
	}
	

    public void DetermineAction(GameObject behaviorReceiber, GameObject aux) {

        if (aux.tag == "IA")
        {
            if (Mathf.Abs(Vector3.Distance(behaviorReceiber.transform.position, aux.transform.position)) <= MinDistanceOpenMenu)
            {
                Debug.Log("estan cerca, abro menu");
                openConversationMenu(behaviorReceiber, aux);

            }
            else
            {
                Debug.Log("esta lejos, me acercare");
             
                WeightedBehaviours pursue = new WeightedBehaviours(SteeringBehaviour.PURSUE, 0.7f, 0);
                WeightedBehaviours avoidWall = new WeightedBehaviours(SteeringBehaviour.AVOIDWALL, 1f, 0);
                WeightedBehaviours face = new WeightedBehaviours(SteeringBehaviour.FACE, 1f, 0);
                WeightedBehavioursArray =new WeightedBehaviours[] {pursue,avoidWall,face };
                floorAction(behaviorReceiber, aux); //if IA character is too far, we need to arrive/pursue him in order to be near, so we can talk to him
            }
        }

        else if (aux.tag == "MenuButton")
        {
            Debug.Log("menuButton");
            aux.GetComponent<ButtonAction>().Action();
        }
        else if (aux.tag == "Player") {
            Debug.Log("Pa k clikas en el player, jaja salu2");
        } else if (aux.layer == 8) {
            //Esto es para que al clickar en un muro no haga nada
        }
        else
        { // target is floor
            string[] behaviours = { "Arrive", "AvoidWall", "LookWhereYouAreGoing" };
            float[] weightedBehavs = { 0.7f, 1, 1 };
           addBehavioursOver(behaviorReceiber, aux, behaviours, weightedBehavs);
            //floorAction(behaviorReceiber, aux); //if IA character is too far, we need to arrive/pursue him in order to be near, so we can talk to him
        }
        
    }


    public void addBehaviour(GameObject behaviorReceiber, SteeringBehaviour comportamiento, GameObject aux, float weight,int priority)
    {

        switch (comportamiento)
        {
            case SteeringBehaviour.SEEK:
                behaviorReceiber.gameObject.AddComponent<Seek>().setTarget(aux).setWeight(weight).setPriority(priority);
                break;
            case SteeringBehaviour.FLEE:
                behaviorReceiber.gameObject.AddComponent<Flee>().setTarget(aux).setWeight(weight).setPriority(priority);
                break;
            case SteeringBehaviour.ARRIVE:
                behaviorReceiber.gameObject.AddComponent<Arrive>().setTarget(aux).setWeight(weight).setPriority(priority);
                break;
            case SteeringBehaviour.LEAVE:
                behaviorReceiber.gameObject.AddComponent<Leave>().setTarget(aux).setWeight(weight).setPriority(priority);
                break;
            case SteeringBehaviour.PURSUE:
                behaviorReceiber.gameObject.AddComponent<Pursue>().setTarget(aux).setWeight(weight).setPriority(priority);
                break;
            case SteeringBehaviour.EVADE:
                behaviorReceiber.gameObject.AddComponent<Evade>().setTarget(aux).setWeight(weight).setPriority(priority);
                break;
            case SteeringBehaviour.ALIGN:
                behaviorReceiber.gameObject.AddComponent<Align>().setTarget(aux).setWeight(weight).setPriority(priority);
                break;
            case SteeringBehaviour.FACE:
                behaviorReceiber.gameObject.AddComponent<Face>().setTarget(aux).setWeight(weight).setPriority(priority);
                break;
            case SteeringBehaviour.WANDER:
                behaviorReceiber.gameObject.AddComponent<Wander>().setTarget(aux).setWeight(weight).setPriority(priority);
                break;
            case SteeringBehaviour.AVOIDWALL:
                behaviorReceiber.gameObject.AddComponent<AvoidWall>().setTarget(aux).setWeight(weight).setPriority(priority);
                break;
            case SteeringBehaviour.LOOKWHEREYOUAREGOING:
                behaviorReceiber.gameObject.AddComponent<LookWhereYouAreGoing>().setTarget(aux).setWeight(weight).setPriority(priority);
                break;
            default:
                break;
        }

    }
    void floorAction(GameObject behaviorReceiber, GameObject aux) {
        //if (aux.tag != "IA") aux.GetComponent<SpriteRenderer>().color = Color.red; ESTO ES PARA PONER ROJO EL PUNTO DE DESTINO

        //copied from clickPosition
        foreach (var comportamiento in behaviorReceiber.GetComponents<AgentBehaviour>())
        {
            DestroyImmediate(comportamiento);
        }

        foreach (var comportamiento in WeightedBehavioursArray)
        {
            addBehaviour(behaviorReceiber, comportamiento.Behaviour, aux, comportamiento.weight,comportamiento.priority);
        }
    }

    void openConversationMenu(GameObject character, GameObject targetIA) {
        menuController.OpenConversationMenu(character, targetIA);
    }

    public void addBehavioursOver(GameObject behaviourReceiver, Vector3 target,string[] behaviours, float[] weights)
    {
        if (behaviours.Length != weights.Length) Debug.LogError("NO ME HAS MANDADO BIEN LAS PRIORIDADES Y LOS COMPORTAMIENTOS");
        WeightedBehaviours aux_behav = new WeightedBehaviours();
        WeightedBehavioursArray = new WeightedBehaviours[behaviours.Length];
        int iterator = 0;
        foreach (var behav in behaviours)
        {
            switch (behav)
            {
                case "Pursue":
                    WeightedBehavioursArray[iterator]= new WeightedBehaviours(SteeringBehaviour.PURSUE, weights[iterator], 0);
                    break;
                case "Face":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.FACE, weights[iterator], 0);
                    break;
                case "LookWhereYouAreGoing":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.LOOKWHEREYOUAREGOING, weights[iterator], 0);
                    break;
                case "AvoidWall":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.AVOIDWALL, weights[iterator], 0);
                    break;
                case "Evade":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.EVADE, weights[iterator], 0);
                    break;
                case "Wander":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.WANDER, weights[iterator], 0);
                    break;
                case "Arrive":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.ARRIVE, weights[iterator], 0);
                    break;
                case "Leave":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.LEAVE, weights[iterator], 0);
                    break;
                default:
                    Debug.LogError("ESE COMPORTAMIENTO NO ESTA CIONTEMPLADO ;(");
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.NOTHING, weights[iterator], 0);
                    break;
            }
            iterator++;

        }
        IAAction(behaviourReceiver, target);

    }

    public void addBehavioursOver(GameObject behaviourReceiver, GameObject target, string[] behaviours, float[] weights)
    {
        if (behaviours.Length != weights.Length) Debug.LogError("NO ME HAS MANDADO BIEN LAS PRIORIDADES Y LOS COMPORTAMIENTOS");
        WeightedBehaviours aux_behav = new WeightedBehaviours();
        WeightedBehavioursArray = new WeightedBehaviours[behaviours.Length];
        int iterator = 0;
        foreach (var behav in behaviours)
        {
            switch (behav)
            {
                case "Pursue":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.PURSUE, weights[iterator], 0);
                    break;
                case "Face":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.FACE, weights[iterator], 0);
                    break;
                case "LookWhereYouAreGoing":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.LOOKWHEREYOUAREGOING, weights[iterator], 0);
                    break;
                case "AvoidWall":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.AVOIDWALL, weights[iterator], 0);
                    break;
                case "Evade":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.EVADE, weights[iterator], 0);
                    break;
                case "Wander":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.WANDER, weights[iterator], 0);
                    break;
                case "Arrive":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.ARRIVE, weights[iterator], 0);
                    break;
                case "Leave":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.LEAVE, weights[iterator], 0);
                    break;
                case "Flee":
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.FLEE, weights[iterator], 0);
                    break;
                default:
                    Debug.LogError("ESE COMPORTAMIENTO NO ESTA CIONTEMPLADO ;(");
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.NOTHING, weights[iterator], 0);
                    break;
            }
            iterator++;

        }
        IAAction(behaviourReceiver, target);

    }
    void IAAction(GameObject behaviourreceiver,Vector3 target)// busca el primer gameobjeto diferente de objext
    {

        Collider2D[] coli= Physics2D.OverlapCircleAll((Vector2)target, 16,mask);
        Collider2D selected =null;

        foreach (var colis in coli)
        {
            if(colis.tag != "Object" ) {
                selected = colis;
                break;
            }
        }
        if (selected != null )
        {
            GameObject targetgo = selected.gameObject;
            floorAction(behaviourreceiver, targetgo);
        }
      /*  else
        {
            Debug.LogError("En ese punto no hay absolutamente nada");
        }*/

    }
    void IAAction(GameObject behaviourreceiver, GameObject target)
    {
            floorAction(behaviourreceiver, target);

    }

}
