using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class RoomMemory : MonoBehaviour {

    public string currentRoom;

    private List<string> locationsKnownList;
    private List<string> RouteToDestination;

    private string destination;
    private bool debugeandoMemoria;

    // Use this for initialization
    void Awake () {
        locationsKnownList = new List<string>();
        RouteToDestination = new List<string>();
        debugeandoMemoria = true;
    }
	
	// Update is called once per frame
	void Update () {
        //DEBUG
        if (locationsKnownList.Count >= 12 && debugeandoMemoria) {
            Debug.Log("Quiero ir a: " + locationsKnownList[0]);
            foreach (string sitio in locationsKnownList)
            {
                Debug.Log("Conozco estos sitios: " + sitio);
            }
            SetDestinyRoom(locationsKnownList[0]);
            debugeandoMemoria = false;
        }

	}

    public void AddLocation(string LocationName)
    {
        if ( !locationsKnownList.Contains(LocationName) ) // si el sitio es nuevo para mi, me lo guardo
        {
            locationsKnownList.Add(LocationName);
            currentRoom = LocationName;
        }
    }

    public void SetDestinyRoom(string destinationDesired) {
        destination = destinationDesired;
        RouteToDestination = CalculateRoute(currentRoom, destination, locationsKnownList);

        //DEBUG
        int i = 1;
        foreach (string room in RouteToDestination)
        {
            Debug.Log("" + i + " voy a " + room);
            i++;
        }

    }

    private List<string> CalculateRoute(string current,  string destination, List<string> discoveredRooms)
    {
        return GameObject.Find(currentRoom).GetComponent<LocationDiscovery>().TraceRoute(current,destination, discoveredRooms);
    }
}
