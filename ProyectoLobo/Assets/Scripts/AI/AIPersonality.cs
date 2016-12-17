using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPersonality: PersonalityBase {

	public DecisionTreeNode[] oldNodes;

    private Memory myMemory;
    private Vector3? rememberedMedicalaidPosition;


    void Start()
    {
        TrustInOthers= new int[5]; // 5 characters
        myMemory = GetComponent<Memory>();
        
       // interactionFromOtherCharacter = ActionsEnum.Actions.ATTACK;
        initializeTrustInOthers();

    }
 
  
    void update()
    {
        if(health < 20)
        {
            rememberedMedicalaidPosition = myMemory.SearchInMemory("Medicalaid");
            if (rememberedMedicalaidPosition != null)
            {
                //moverse hacia alli
            }
        }
    }
}
