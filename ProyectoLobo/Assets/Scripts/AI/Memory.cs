using UnityEngine;
using System.Collections.Generic;

public class Memory : MonoBehaviour {

    public Dictionary<string, GameObject> objectsSeenBefore;

	// Use this for initialization
	void Awake () {

        objectsSeenBefore = new Dictionary<string, GameObject>();
	
	}

    public GameObject SearchInMemory(string urgentObject)
    {
        if (objectsSeenBefore.ContainsKey(urgentObject))
        {
            return objectsSeenBefore[urgentObject];
        }
        else
            return null;
        
    }
}
