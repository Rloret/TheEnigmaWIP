using UnityEngine;
using System.Collections;

public class OnObjectClickedController : MonoBehaviour {

    public float MinDistanceOpenMenu = 40f;
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
        }
        else
        { // target is floor
            floorAction(behaviorReceiber, aux);
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
        if (aux.tag != "IA") aux.GetComponent<SpriteRenderer>().color = Color.red;

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
}
