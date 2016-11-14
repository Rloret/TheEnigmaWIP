using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecisionTarget : MonoBehaviour {

    //private AIPersonality personality;
    private Dictionary<GameObject, int> analyzedTargets;
    private PriorityTree priorityTree;
    private Memory memory;

    public GameObject AI;

    void Awake() {

        analyzedTargets = new Dictionary<GameObject, int>();
        priorityTree = new PriorityTree();
        memory = AI.GetComponent<Memory>();


        //ChooseTarget(targets);

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
        string nameCarriedTarget;
        GameObject chosenTarget = null;
        AIPersonality personality = Ai.GetComponent<AIPersonality>();


        foreach (GameObject target in viewedTargets)
        {
            priority = priorityTree.GetPriority(target, personality); // Llama al árbol de prioridad que devuelve la prioridad de ese GameObject
            Debug.Log("La prioridad de " + target + " es " + priority);
            if (!analyzedTargets.ContainsKey(target))
                analyzedTargets.Add(target, priority);
        }

        chosenTarget = GivePriorityTarget(analyzedTargets); // Recoge el GameObject más prioritario
        nameCarriedTarget = objectTraduction(personality); // Mira qué objeto lleva en ese momento la IA

        if(chosenTarget.name == nameCarriedTarget) // Si lleva un objeto y es el que ha visto más prioritario: ese objeto se elimina del diccionario y se recoge el siguiente con más prioridad
        {
            analyzedTargets.Remove(chosenTarget);
            chosenTarget = GivePriorityTarget(analyzedTargets);
        }
        
        Debug.Log(chosenTarget);

        analyzedTargets.Clear();

        return chosenTarget;

    }
    /// <summary>
    /// Determina el objeto más prioritario que hay en el diccionario
    /// </summary>
    /// <param name="analyzedTargets"></param>
    /// <returns></returns>
    private GameObject GivePriorityTarget(Dictionary<GameObject, int> analyzedTargets)
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
            memory.objectsSeenBefore.Add(par.Key.name, par.Key);

        }
        return chosenTarget;

    }
    /// <summary>
    /// Traduce a una string el objeto que tiene la IA para poder compararlo con el nombre
    /// del GameObject más prioritario y saber así si ya lo tiene o no
    /// </summary>
    /// <param name="personality"></param>
    /// <returns></returns>

    private string objectTraduction (AIPersonality personality)
    {
        if (personality.myObject == ObjectHandler.ObjectType.AXE)
            return "AXE";
        else if (personality.myObject == ObjectHandler.ObjectType.BOOTS)
            return "BOOTS";
        else if (personality.myObject == ObjectHandler.ObjectType.FLASHLIGHT)
            return "FLASHLIGTH";
        else if (personality.myObject == ObjectHandler.ObjectType.JUMPSUIT)
            return "JUMPSUIT";
        else if (personality.myObject == ObjectHandler.ObjectType.MEDICALAID)
            return "MEDICALAID";
        else if (personality.myObject == ObjectHandler.ObjectType.SHIELD)
            return "SHIELD";
        else
            return "NONE";


    }
}
