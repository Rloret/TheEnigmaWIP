using UnityEngine;
using System.Collections;

public class ObjectAction : MonoBehaviour {

	void OnTriggerEnter(Collider c)
    {
        c.gameObject.GetComponent<ObjectHandler>().youAreOnA(this.gameObject);

    }
}
