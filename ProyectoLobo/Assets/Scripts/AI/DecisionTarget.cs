using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecisionTarget : MonoBehaviour {

    private AIPersonality personality;
    private List<PriorityDictionary> analyzedTargets;
    private PriorityTree priorityTree;

    public GameObject boti;
    public GameObject hacha;

	void Awake () {

        analyzedTargets = new List<PriorityDictionary>();
        personality = new AIPersonality();
	
	}

    public void ChooseTarget(ref List<GameObject> viewedTargets) // Lista de GameObjects
    {
        int priority;

        foreach (GameObject target in viewedTargets)
        {
            priority = priorityTree.GetPriority(target, personality);
            analyzedTargets.Add(new PriorityDictionary(target, priority));
        }

        Debug.Log("El diccionario tiene las entradas: " + analyzedTargets[0].target.name + " " + analyzedTargets[1].target.name);

        analyzedTargets.Sort();
        Debug.Log("El target más prioritario es: " + analyzedTargets[0].target.name);
    }


	
}
