using UnityEngine;
using System.Collections;

public class PlayerPersonality : PersonalityBase {

	void Start()
	{
		TrustInOthers= new int[GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().numberOfIAs]; // 5 characters

		initializeTrustInOthers(GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().numberOfIAs);

	}
}
