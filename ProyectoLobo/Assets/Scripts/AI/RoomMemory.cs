using UnityEngine;
using System.Collections;
using System;

public class RoomMemory : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddLocation(string LocationName)
    {
        Debug.Log("Soy " + this.name + "Estoy en " + LocationName);

    }
}
