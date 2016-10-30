using UnityEngine;
using System.Collections;

public class ObjectDecision : MonoBehaviour {

    private float charisma;
    private float selfAssertion;
    private float fear;

    private AIPersonality personality;

	// Use this for initialization
	void Awake () {

        charisma = personality.charisma;
        selfAssertion = personality.selfAssertion;
        fear = personality.fear;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
