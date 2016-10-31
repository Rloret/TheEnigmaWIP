using UnityEngine;
using System.Collections;
using System;

public class PriorityDictionary : MonoBehaviour {

    public GameObject target;
    public int priority;

    public PriorityDictionary(GameObject target, int priority) //Referencia a gameObject
    {
        this.target = target;
        this.priority = priority;
    }
	
    public int CompareTo(PriorityDictionary other)
    {
        if (other == null)
            return 1;
        return priority - other.priority;
    }
}
