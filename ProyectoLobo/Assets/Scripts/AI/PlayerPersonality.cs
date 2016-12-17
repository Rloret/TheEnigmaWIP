using UnityEngine;
using System.Collections;

public class PlayerPersonality : PersonalityBase {

	void Start()
	{
		TrustInOthers= new int[5]; // 5 characters

		initializeTrustInOthers();

	}
}
