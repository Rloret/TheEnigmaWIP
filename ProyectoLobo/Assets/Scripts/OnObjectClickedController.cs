using UnityEngine;
using System.Collections;

public class OnObjectClickedController : MonoBehaviour {

    public float MinDistanceOpenMenu = 40f;
    private ConversationMenuController menuController;

    [System.Serializable]
    public class WeightedBehaviours
    {
        [SerializeField]
        public SteeringBehaviour Behaviour;
        [SerializeField]
        [Range(0, 1)]
        public float weight;


    }

    public WeightedBehaviours[] WeightedBehavioursArray;

    public enum SteeringBehaviour
    {
        NOTHING, SEEK, FLEE, ARRIVE, LEAVE, PURSUE, EVADE, ALIGN, FACE, WANDER
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

              //  floorAction(behaviorReceiber, aux); //if IA character is too far, we need to arrive/pursue him in order to be near, so we can talk to him
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
            Debug.Log(aux.name);
            floorAction(behaviorReceiber, aux);
        }
        
    }


    void addBehaviour(GameObject behaviorReceiber, SteeringBehaviour comportamiento, GameObject aux, float weight)
    {

        switch (comportamiento)
        {
            case SteeringBehaviour.SEEK:
                this.gameObject.AddComponent<Seek>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.FLEE:
                this.gameObject.AddComponent<Flee>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.ARRIVE:
                this.gameObject.AddComponent<Arrive>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.LEAVE:
                this.gameObject.AddComponent<Leave>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.PURSUE:
                this.gameObject.AddComponent<Pursue>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.EVADE:
                this.gameObject.AddComponent<Evade>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.ALIGN:
                this.gameObject.AddComponent<Align>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.FACE:
                this.gameObject.AddComponent<Face>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.WANDER:
                this.gameObject.AddComponent<Wander>().setTarget(aux).setWeight(weight);
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

            addBehaviour(behaviorReceiber, comportamiento.Behaviour, aux, comportamiento.weight);
            //counter++;
        }
    }

    void openConversationMenu(GameObject character, GameObject targetIA) {
        menuController.OpenConversationMenu(character, targetIA);
    }
}
