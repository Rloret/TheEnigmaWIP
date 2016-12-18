using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AIPersonality: PersonalityBase {

	public DecisionTreeNode[] oldNodes;


    public Memory myMemory;
    public int numberOfIAs;
    public GameObject HealthImage;
    public GameObject panel;
    public float defense=1f;

    /// <summary>
    /// Personalities contains the 6 possible personalities beeing:
    /// SAF: Carismatica>agresiva>miedosa
    /// SFA: Carismatica>miedosa>Agresiva
    /// AFS: Agresiva>Miedosa>Carismatica
    /// ASF: Agresiva>Carismatica>Miedosa
    /// FSA: Miedosa>Carismatica>Agresiva
    /// FAS: Miedosa>Agresiva>Carismatica
    /// </summary>
    public enum Personalities
    {
        SAF=0,SFA=1,AFS=2,ASF=3,FSA=4,FAS=5
    }

    private Vector3? rememberedMedicalaidPosition;


    void Start()
    {
        numberOfIAs = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().numberOfIAs;
        TrustInOthers = new int[numberOfIAs]; 
        myMemory = GetComponent<Memory>();
        
       // interactionFromOtherCharacter = ActionsEnum.Actions.ATTACK;
        initializeTrustInOthers(numberOfIAs);

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

    public void configurePersonality(Personalities type)
    {
        switch (type)
        {
            case Personalities.SAF:
                charisma = 3;
                selfAssertion=2;
                fear=1;
                break;
            case Personalities.SFA:
                charisma=3;
                selfAssertion=1;
                fear=2;
                break;
            case Personalities.AFS:
                charisma=1;
                selfAssertion=3;
                fear=2;
                break;
            case Personalities.ASF:
                charisma = 2;
                selfAssertion = 3;
                fear = 1;
                break;
            case Personalities.FSA:
                charisma = 2;
                selfAssertion = 1;
                fear = 3;
                break;
            case Personalities.FAS:
                charisma = 1;
                selfAssertion = 2;
                fear = 3;
                break;
            default:
                break;
        }

        initializeTrustInOthers(numberOfIAs);
    }

    public void takeDamage(int damage)
    {
        health -= (int)(damage * defense);
        if (health <= 50)
        {
            HealthImage.GetComponent<Image>().color = new Color(255, 255, 0);
        }
        else if (health <= 33)
        {
            HealthImage.GetComponent<Image>().color = new Color(0, 0,255);
        }
        else
        {
            this.GetComponent<VisibilityConeCycleIA>().enabled = false;
            VisibleElements.visibleGameObjects.Remove(this.gameObject);
            Debug.Log(this.gameObject.name + "HA MUERTO");
        }
    }
}
