using UnityEngine;
using System.Collections;

public class BehaviourAdder : MonoBehaviour {
    public float MinDistanceOpenMenu = 60f;
    public LayerMask mask;
	private PlayerMenuController menuController;

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
    void Start ()
    {
		menuController = this.gameObject.GetComponent<PlayerMenuController>();

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
    public void ActionWhenClick(GameObject behaviorReceiber, GameObject[] aux) {
        //if (aux.tag != "IA") aux.GetComponent<SpriteRenderer>().color = Color.red; ESTO ES PARA PONER ROJO EL PUNTO DE DESTINO
		if (aux == null) {
			
			Debug.Log ("nuloo");
		}
        //copied from clickPosition
        foreach (var comportamiento in behaviorReceiber.GetComponents<AgentBehaviour>())
        {
            DestroyImmediate(comportamiento);
        }
        int i = 0;
        foreach (var comportamiento in WeightedBehavioursArray)
        {
            addBehaviour(behaviorReceiber, comportamiento.Behaviour, aux[i], comportamiento.weight,comportamiento.priority);
            i++;
        }
    }

    public void openConversationMenu(GameObject character, GameObject targetIA) {
        GroupScript characterGroup = character.GetComponent<GroupScript>();
        GroupScript targetGroup = targetIA.GetComponent<GroupScript>();
        if (targetIA.GetComponent<AIPersonality>().theThing)
        {
            menuController.OpenMenu(PlayerMenuController.MenuTypes.MENU_ATTACKED, targetIA);
        }
        else if (characterGroup.groupLeader == targetGroup.groupLeader || (characterGroup.groupMembers.Count +targetGroup.groupMembers.Count>=3) )
        {
            menuController.OpenMenu(PlayerMenuController.MenuTypes.MENU_CONVERSATION_WITH_MYGROUP, targetIA);
        }
        else
		menuController.OpenMenu(PlayerMenuController.MenuTypes.MENU_CONVERSATION,targetIA);
	}

    public void addBehavioursOver(GameObject behaviourReceiver, GameObject[] target, string[] behaviours, float[] weights)
    {
		if (behaviours.Length != weights.Length || behaviours.Length!=target.Length || target.Length!=weights.Length) Debug.LogError("NO ME HAS MANDADO BIEN LAS PRIORIDADES Y LOS COMPORTAMIENTOS");

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
			case "Nothing":
				//	Debug.LogError("ESE COMPORTAMIENTO NO ESTA CONTEMPLADO ;(");
					WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.NOTHING, weights[iterator], 0);
					break;
                default:
                    Debug.LogError("ESE COMPORTAMIENTO NO ESTA CONTEMPLADO ;(");
                    WeightedBehavioursArray[iterator] = new WeightedBehaviours(SteeringBehaviour.NOTHING, weights[iterator], 0);
                    break;
            }
            iterator++;

        }
        IAAction(behaviourReceiver, target);

    }
    void IAAction(GameObject behaviourreceiver, GameObject[] target)
    {
        ActionWhenClick(behaviourreceiver, target);

    }

}
