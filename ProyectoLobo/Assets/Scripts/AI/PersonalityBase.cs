﻿using UnityEngine;
using System.Collections;

public class PersonalityBase : MonoBehaviour {
	public int health = 0;
	public int attack=0;
	public int confidence;

	public float charisma;
	public float selfAssertion; // supongo que esto es agresividad para los arboles de decisiones ¿?
	public float fear;

	public bool isMonster = false; // MOCK

	public ObjectHandler.ObjectType myObject;

	public ActionsEnum.Actions interactionFromOtherCharacter;
	public int[] TrustInOthers;

	public int MyOwnIndex;

	public void SetMyOwnIndex(int i) {
		MyOwnIndex = i;
	}
	public int GetMyOwnIndex() { return MyOwnIndex; }


	protected void initializeTrustInOthers() {
		for (int i = 0; i < 5; i++) TrustInOthers[i] =4;
	}
	public ActionsEnum.Actions GetInteraction() { return interactionFromOtherCharacter; }

}