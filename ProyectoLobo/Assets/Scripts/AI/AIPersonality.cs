using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AIPersonality: PersonalityBase {

	public DecisionTreeNode[] oldNodes;
	public float defense=1f;
	public GameObject HealthImage;
	public GameObject panel;

    public Memory myMemory;
    public int numberOfIAs;


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
    private BehaviourAdder behaviourManager;


    void Start()
    {
        numberOfIAs = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().numberOfIAs;
        TrustInOthers = new int[numberOfIAs]; 
        myMemory = GetComponent<Memory>();
        behaviourManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BehaviourAdder>();
       // interactionFromOtherCharacter = ActionsEnum.Actions.ATTACK;
        initializeTrustInOthers(numberOfIAs);

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

    public override void takeDamage(int damage)
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
        else if(health<=0)
        {
            this.GetComponent<VisibilityConeCycleIA>().enabled = false;
            VisibleElements.visibleGameObjects.Remove(this.gameObject);
            Debug.Log(this.gameObject.name + "HA MUERTO");
        }
    }

    public void formacionGrupo(GameObject WhoToFollow,GroupScript leaderGroup)
    {

        string[] baseBehaviours = { "Arrive", "AvoidWall", "LookWhereYouAreGoing" };
        float[] weightedBehavs = { 0.7f, 1, 1 };
        GameObject[] targetsarray = { WhoToFollow, WhoToFollow, WhoToFollow };

        List<GameObject> mates;
        List<string> baseBehavioursformates;
        List<float> baseWeightsformates;

        foreach (var mate in leaderGroup.groupMembers)
        {
            mates = new List<GameObject>();
            baseBehavioursformates = new List<string>();
            baseWeightsformates = new List<float>();

            mates.AddRange(targetsarray);
            baseBehavioursformates.AddRange(baseBehaviours);
            baseWeightsformates.AddRange(weightedBehavs);

            foreach (var othermate in leaderGroup.groupMembers)
            {
                if (mate != othermate)
                {
                    mates.Add(othermate);
                    baseBehavioursformates.Add("Leave");
                    baseWeightsformates.Add(0.8f);
                }
            }
            mates.Add(this.gameObject);
            baseBehavioursformates.Add("Leave");
            baseWeightsformates.Add(0.8f);
            behaviourManager.addBehavioursOver(mate, convertListToArray<GameObject>(mates), convertListToArray<string>(baseBehavioursformates),
                                                convertListToArray<float>(baseWeightsformates));


        }

    }

    public static T[] convertListToArray<T>(List<T> list)
    {
        T[] array = new T[list.Count];
        int i = 0;
        foreach (var value in list)
        {
            array[i] = value;
            i++;
        }
        return array;
    }
}
