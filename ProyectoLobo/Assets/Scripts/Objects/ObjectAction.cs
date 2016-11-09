using UnityEngine;
using System.Collections;

public class ObjectAction : MonoBehaviour {

	void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        c.gameObject.GetComponent<ObjectHandler>().youAreOnA(this.gameObject);

    }
}
