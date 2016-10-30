using UnityEngine;
using System.Collections;
using System;

public class PriorityDictionary : MonoBehaviour {

    public string name;
    public int priority;

    public PriorityDictionary(string name, int priority)
    {
        this.name = name;
        this.priority = priority;
    }
	
    public int CompareTo(PriorityDictionary other)
    {
        if (other == null)
            return 1;
        return priority - other.priority;
    }
}
