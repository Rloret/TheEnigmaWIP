using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class RoomMemory : MonoBehaviour {

    public string currentRoom;

    private List<string> locationsKnownList;
    private List<string> RouteToDestination;

    private string destination;

    // Use this for initialization
    void Awake () {
        locationsKnownList = new List<string>();
        RouteToDestination = new List<string>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddLocation(string LocationName)
    {
        //Debug.Log("Soy " + this.name + "Estoy en " + LocationName);
        if ( !locationsKnownList.Contains(LocationName) ) // si el sitio es nuevo para mi, me lo guardo
        {
            locationsKnownList.Add(LocationName);
            currentRoom = LocationName;
            /*Debug.Log("Conozco " + locationsKnownList.Count + " sitios distintos.");
            foreach (string loc in locationsKnownList) {
                Debug.Log("He estado en " + loc );
            }*/
        }
    }

    public void SetDestinyRoom(string destinationDesired) {
        destination = destinationDesired;
        RouteToDestination = CalculateRoute(currentRoom, destination);
    }

    private List<string> CalculateRoute(string current,  string destination)
    {
        return GameObject.Find(currentRoom).GetComponent<LocationDiscovery>().TraceRoute(current,destination);
    }
}
