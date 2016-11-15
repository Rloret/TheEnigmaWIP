using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecisionTarget : MonoBehaviour {

    private AIPersonality personality;
    private Dictionary<GameObject, int> analyzedTargets;
    private PriorityTree priorityTree;


    // Para testear
    private GameObject boots;
    private GameObject axe;
    List<GameObject> targets;

	void Awake () {

        analyzedTargets = new Dictionary<GameObject, int>();
        personality = new AIPersonality();
        priorityTree = new PriorityTree();

        boots = GameObject.Find("Boots");
        axe = GameObject.Find("Axe");

        Debug.Log(personality.charisma + " " + personality.selfAssertion + " " + personality.fear);

      /*  //para testear
        targets = new List<GameObject>();
        targets.Add(axe);
        targets.Add(boots);

        ChooseTarget(targets);
	*/
	}

    public void ChooseTarget(List<GameObject> viewedTargets) // Lista de GameObjects
    {
        int priority;
        int maxPriority = -1;
        GameObject chosenTarget = null;
        

        foreach (GameObject target in viewedTargets)
        {
            priority = priorityTree.GetPriority(target, personality);
            Debug.Log("La prioridad de " + target + " es " + priority);
            if (!analyzedTargets.ContainsKey(target))
                analyzedTargets.Add(target, priority);
        }

        foreach (KeyValuePair<GameObject, int> par in analyzedTargets)
        {
            if (par.Value > maxPriority)
            {
                maxPriority = par.Value;
                chosenTarget = par.Key;
            }
                
        }
        Debug.Log(chosenTarget);

        //return chosenTarget;

    }


	
}
