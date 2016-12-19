using UnityEngine;
using System.Collections.Generic;

public class Memory : MonoBehaviour {

    public Dictionary<string, Vector3?> objectsSeenBefore; //nombre del objeto y la posición en la que se vió
    public Dictionary<string, string> objectWithinRoom;
	// Use this for initialization
	void Awake () {

        objectsSeenBefore = new Dictionary<string, Vector3?>();
        objectWithinRoom = new Dictionary<string, string>();
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

    public GameObject SearchRoomInMemory(string urgentObject) {

        if (objectWithinRoom.ContainsKey(urgentObject))
        {

            string roomName = objectWithinRoom[urgentObject];
            return GameObject.Find(roomName);

        }
        else
            return null;

    }
}
