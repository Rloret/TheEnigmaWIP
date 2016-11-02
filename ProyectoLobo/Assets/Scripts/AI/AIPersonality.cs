using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPersonality : MonoBehaviour {

    public int health = 30;
    public int attack;
    public int confidence;

    public float charisma;
    public float selfAssertion;
    public float fear;

    public bool isMonster = true; // MOCK
    public bool inGroup = false; //MOCK

    private ActionsEnum.Actions interactionFromOtherCharacter;

    void Start()
    {
        interactionFromOtherCharacter = ActionsEnum.Actions.OFFER;
    }
    public void SetInteraction(ActionsEnum.Actions a)
    {
        interactionFromOtherCharacter = a;
    }

    public ActionsEnum.Actions GetInteraction() { return interactionFromOtherCharacter; }

    public AIPersonality ()
    {
        /*charisma = Random.Range(6.0f, 1.0f);
        selfAssertion = Random.Range(6.0f, 1.0f);
        fear = Random.Range(6.0f, 1.0f);*/

    }
	/*void Awake () {

        charisma = Random.Range(6.0f, 1.0f);
        selfAssertion = Random.Range(6.0f, 1.0f);
        fear = Random.Range(6.0f, 1.0f);

        Debug.Log("charisma = " + charisma + " selfAssertion = " + selfAssertion + " fear = " + fear);

    }*/

    void Awake()
    {

    }
	
}
