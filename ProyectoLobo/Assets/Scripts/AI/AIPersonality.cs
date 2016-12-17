using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPersonality: PersonalityBase {

	public DecisionTreeNode[] oldNodes;


    public Memory myMemory;


    void Start()
    {
        TrustInOthers= new int[5]; // 5 characters
        myMemory = GetComponent<Memory>();
        // interactionFromOtherCharacter = ActionsEnum.Actions.ATTACK;
        initializeTrustInOthers();

    }


}
