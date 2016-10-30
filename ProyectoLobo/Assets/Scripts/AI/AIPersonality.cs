using UnityEngine;
using System.Collections;

public class AIPersonality : MonoBehaviour {

    public int health = 100;
    public int attack;

    public float charisma;
    public float selfAssertion;
    public float fear;

	
	void Awake () {

        charisma = Random.Range(6.0f, 1.0f);
        selfAssertion = Random.Range(6.0f, 1.0f);
        fear = Random.Range(6.0f, 1.0f);

        //Debug.Log("charisma = " + charisma + " selfAssertion = " + selfAssertion + " fear = " + fear);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
