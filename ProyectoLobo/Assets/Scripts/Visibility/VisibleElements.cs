using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisibleElements : MonoBehaviour {

    public static List<GameObject> visibleGameObjects;

	// Use this for initialization
	void Start () {
        visibleGameObjects = new List<GameObject>();
        visibleGameObjects.AddRange(GameObject.FindGameObjectsWithTag("Object"));
        visibleGameObjects.AddRange(GameObject.FindGameObjectsWithTag("IA"));
        visibleGameObjects.Add(GameObject.FindGameObjectWithTag("Player"));
	}
	
}
