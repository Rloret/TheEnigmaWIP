using UnityEngine;
using System.Collections;

public class MoveUp : MonoBehaviour {

    public GameObject whoToFollow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.position = new Vector2(Mathf.Lerp(this.transform.position.x,whoToFollow.transform.position.x,0.2f),this.transform.position.y + 2+Time.deltaTime);
        this.transform.position = whoToFollow.transform.position;

	}
}
