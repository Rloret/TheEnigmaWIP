using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

    // Use this for initialization

    public Animator AnimController;


    private AgentPositionController movementController;
    void Start () {
        movementController = this.transform.parent.GetComponent<AgentPositionController>();
	}
	
	// Update is called once per frame
	void Update () {
        float linearSpeedClamped = movementController.linearVelocity.magnitude / movementController.maxLinearVelocity;
        Debug.Log(linearSpeedClamped);
        if (linearSpeedClamped ==0f )
        {
            AnimController.speed = 1;
            AnimController.SetInteger("State", 0);
        }
        else
        {
            AnimController.speed =3*linearSpeedClamped;
            AnimController.SetInteger("State", 1);

        }
	}
}
