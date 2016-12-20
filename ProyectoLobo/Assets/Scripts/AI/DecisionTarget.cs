using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecisionTarget : MonoBehaviour {
	
    private Dictionary<GameObject, int> analyzedTargets;
    private PriorityTree priorityTree;
    private Memory memory;


    void Awake() {

        analyzedTargets = new Dictionary<GameObject, int>();
        priorityTree = this.gameObject.AddComponent<PriorityTree>();

	}


    /// <summary>
    /// Devuelve el GAmeObject, que la IA NO TIENE, más prioritario
    /// </summary>
    /// <param name="viewedTargets"></param>
    /// <param name="Ai"></param>
    /// <returns></returns>

    public GameObject ChooseTarget(List<GameObject> viewedTargets, GameObject Ai)
    {
        int priority;
        int currentTargetpriority;
        string nameCurrentTarget;
        GameObject chosenTarget = null;
        GameObject currentTarget = null;
        AIPersonality personality = Ai.GetComponent<AIPersonality>();
		ObjectHandler objectHand = Ai.GetComponent<ObjectHandler> ();
		memory = Ai.GetComponent<Memory>();

        string priorities = "veo un : ";
        foreach (GameObject target in viewedTargets)
        {
            priority = priorityTree.GetPriority(target, personality); // Llama al árbol de prioridad que devuelve la prioridad de ese GameObject
            priorities += target.name + " (priority)";
            //Debug.Log("La prioridad de " + target + " es " + priority);

            if (!analyzedTargets.ContainsKey(target) && priority!=-1)
                analyzedTargets.Add(target, priority);
        }

        chosenTarget = GivePriorityTarget(analyzedTargets, memory); // Recoge el GameObject más prioritario
        //Debug.Log("elegido es " + chosenTarget.name);
        nameCurrentTarget = objectTraduction(personality); // Mira qué objeto lleva en ese momento la IA
        if(chosenTarget == null)
        {
            return chosenTarget;
        }
        if (chosenTarget.tag == "IA"  )
        {
            analyzedTargets.Clear();

            //active tree
            if (this.GetComponent<GroupScript>().checkIAInGroup(chosenTarget))
            {
                return null;
            }
            return chosenTarget;
        }
        else if (chosenTarget.tag == "Player")
        {
            analyzedTargets.Clear();

            //active tree

            return chosenTarget;
        }
        else if (chosenTarget.name == "Medicalaid" && analyzedTargets[chosenTarget] != 4) //Si el objeto más prioritario que ha visto es un botiquín pero no lo necesita, no lo coge
        {
            return null;
        }
        else if ( nameCurrentTarget != "NONE")
        {
            string aux = "Prefabs/Objects/";
            aux += nameCurrentTarget;
            currentTarget = Resources.Load(aux) as GameObject;
            
            currentTargetpriority = priorityTree.GetPriority(currentTarget, personality);
			//Debug.Log ("currentTarget: " + currentTarget);
			//Debug.Log ("prioridad del target actual: " + currentTargetpriority + " prioridad del que estoy viendo: " + analyzedTargets [chosenTarget]);

            if (currentTargetpriority > analyzedTargets[chosenTarget])
            {
                analyzedTargets.Clear();
                chosenTarget = null;
            }
            else
            {
                if (chosenTarget.name == nameCurrentTarget) // Si lleva un objeto y es el que ha visto más prioritario: ese objeto se elimina del diccionario y se recoge el siguiente con más prioridad
                {
					//Debug.Log ("El que veo es más prioritario");
                    analyzedTargets.Remove(chosenTarget);
                    chosenTarget = GivePriorityTarget(analyzedTargets, memory);
                    analyzedTargets.Clear();
                }

            }
            return chosenTarget;

        }
        else
        {
            analyzedTargets.Clear();
            return chosenTarget;

        }
        
    }
    /// <summary>
    /// Determina el objeto más prioritario que hay en el diccionario
    /// </summary>
    /// <param name="analyzedTargets"></param>
    /// <returns></returns>
	private GameObject GivePriorityTarget(Dictionary<GameObject, int> analyzedTargets, Memory memory)
    {
        int maxPriority = -1;
        GameObject chosenTarget = null;
        foreach (KeyValuePair<GameObject, int> par in analyzedTargets)
        {
            if (par.Value > maxPriority)
            {
                maxPriority = par.Value;
                chosenTarget = par.Key;
                
            }
            //Debug.Log("El objeto: " + chosenTarget.name + " está ya en la memoria? " + memory.objectsSeenBefore.ContainsKey(par.Key.name));
            //Debug.Log("El objeto: " + chosenTarget.name + " está ya en la memoria? " + memory.objectWithinRoom.ContainsKey(par.Key.name));
            if (!memory.objectsSeenBefore.ContainsKey(par.Key.name) && (par.Key.tag != "IA" && par.Key.tag != "Player") && !memory.objectWithinRoom.ContainsKey(par.Key.name))
            {
				memory.objectsSeenBefore.Add(par.Key.name, par.Key.transform.position);
                memory.objectWithinRoom.Add(par.Key.name, this.gameObject.GetComponent<RoomMemory>().currentRoom);
                Debug.Log("SOY "+ this.gameObject.name +". Meto en la memoria de habitaciones " + par.Key.name + " y " + this.gameObject.GetComponent<RoomMemory>().currentRoom);
            }
        }

        
        return chosenTarget;

    }
    /// <summary>
    /// Traduce a una string el objeto que tiene la IA para poder compararlo con el nombre
    /// del GameObject más prioritario y saber así si ya lo tiene o no
    /// </summary>
    /// <param name="personality"></param>
    /// <returns></returns>

    public string objectTraduction (PersonalityBase personality)
    {
        if (personality.myObject == ObjectHandler.ObjectType.AXE)
            return "Axe";
        else if (personality.myObject == ObjectHandler.ObjectType.BOOTS)
            return "Boots";
        else if (personality.myObject == ObjectHandler.ObjectType.FLASHLIGHT)
            return "Flashlight";
        else if (personality.myObject == ObjectHandler.ObjectType.JUMPSUIT)
            return "Jumpsuit";
        else if (personality.myObject == ObjectHandler.ObjectType.MEDICALAID)
            return "Medicalaid";
        else if (personality.myObject == ObjectHandler.ObjectType.SHIELD)
            return "Shield";
        else
            return "NONE";


    }
}
