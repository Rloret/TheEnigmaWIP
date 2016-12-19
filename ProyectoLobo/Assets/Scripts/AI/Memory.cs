using UnityEngine;
using System.Collections.Generic;

public class Memory : MonoBehaviour {

    public Dictionary<string, Vector3?> objectsSeenBefore; //nombre del objeto y la posición en la que se vió

	// Use this for initialization
	void Awake () {

        objectsSeenBefore = new Dictionary<string, Vector3?>();
	
	}

    public Vector3? SearchInMemory(string urgentObject)
    {
        if (objectsSeenBefore.ContainsKey(urgentObject))
        {
            return objectsSeenBefore[urgentObject];
        }
        else
			return null;
        
    }
}
