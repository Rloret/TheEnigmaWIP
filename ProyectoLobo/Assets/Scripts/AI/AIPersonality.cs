using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPersonality : MonoBehaviour {

    public int health = 70;
    public int attack;
    public int confidence;

    public float charisma=3;
    public float selfAssertion=2; // supongo que esto es agresividad para los arboles de decisiones ¿?
    public float fear=4;

    public bool isMonster = true; // MOCK
    public bool inGroup = false; //MOCK

    public ActionsEnum.Actions interactionFromOtherCharacter;
    public int[] TrustInOthers;
    public int IndexOtherCharacter; // index to TrustInOthers from the one who interacted with me

    private int MyOwnIndex; 

    void Start()
    {
        TrustInOthers= new int[5]; // 5 characters
        
        interactionFromOtherCharacter = ActionsEnum.Actions.JOIN;
        SetMyOwnIndex(0);
        SetIndexOther(1);
        initializeTrustInOthers();

    }
    public void SetInteraction(ActionsEnum.Actions a)
    {
        interactionFromOtherCharacter = a;
    }
    public void SetMyOwnIndex(int i) {
        MyOwnIndex = i;
    }
    public int GetMyOwnIndex() { return MyOwnIndex; }

    public void SetIndexOther(int i)
    {
        IndexOtherCharacter = i;
    }
    public int GetIndexOther() { return IndexOtherCharacter; }

    private void initializeTrustInOthers() {
        for (int i = 0; i < 5; i++) TrustInOthers[i] = 4;
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
