using UnityEngine;
using System.Collections;

public class DestroyTimed : MonoBehaviour {

    public float lifeTime = 2f;
    private float alive = 0;
	
	void Start()
    {
        alive = Time.time;
    }
	void Update () {

        if (Time.time - alive > lifeTime)
        {
            Destroy(this.gameObject);
        }

	}
}
