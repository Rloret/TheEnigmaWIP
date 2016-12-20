using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersonalityBase : MonoBehaviour {
	public int health = 0;
	public int attack=0;
	public int confidence;

	public int lastAttackValue=0;

    public float defense = 1f;

	public float charisma;
	public float selfAssertion; // supongo que esto es agresividad para los arboles de decisiones ¿?
	public float fear;

	public bool isMonster = false; // INDICA SI ESTÁ CONVERTIDO EN EL MONSTRUO
    public bool theThing = true; //INDICA QUE ES EL MONSTRUO

	public ObjectHandler.ObjectType myObject;

	public ActionsEnum.Actions interactionFromOtherCharacter;
	public int[] TrustInOthers;

	public int MyOwnIndex;

    public BehaviourAdder behaviourManager;


    public void SetMyOwnIndex(int i) {
		MyOwnIndex = i;
	}

	public int GetMyOwnIndex() { return MyOwnIndex; }


	protected void initializeTrustInOthers(int numOfAIs) {
		for (int i = 0; i < numOfAIs; i++) TrustInOthers[i] =Random.Range(5,8);
	}
	public ActionsEnum.Actions GetInteraction() { return interactionFromOtherCharacter; }

	public virtual void takeDamage(int damage, PersonalityBase personality){
	}

    public void formacionGrupo(GameObject WhoToFollow, GroupScript leaderGroup)
    {

        string[] baseBehaviours = { "Arrive", "AvoidWall", "Face" };
        float[] weightedBehavs = { 1f, 8, 1 };
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
                    baseWeightsformates.Add(2f);
                }
            }
            mates.Add(this.gameObject);
            baseBehavioursformates.Add("Leave");
            baseWeightsformates.Add(2f);
            behaviourManager.addBehavioursOver(mate, convertListToArray<GameObject>(mates), convertListToArray<string>(baseBehavioursformates),
                                                convertListToArray<float>(baseWeightsformates));


        }


    }
    public void formacionAtaque(GameObject WhoToFollow, GroupScript leaderGroup)
    {

        string[] baseBehaviours = { "Arrive", "AvoidWall", "Face" };
        float[] weightedBehavs = { 1f, 8, 1 };
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
                    baseWeightsformates.Add(2f);
                }
            }
            mates.Add(this.gameObject);
            baseBehavioursformates.Add("Leave");
            baseWeightsformates.Add(2f);
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
