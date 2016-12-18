using UnityEngine;
using System.Collections;

public class LocationDiscovery : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D IACollider)
    {
        if (IACollider.gameObject.tag == "IA" && IACollider.gameObject.name == "IA0") {
            //Debug.Log("Collider de: " + this.name);
            IACollider.gameObject.GetComponent<RoomMemory>().AddLocation(this.name);
        }
    }

}
