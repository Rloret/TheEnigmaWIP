﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerPersonality : PersonalityBase {
	public GameObject HealthImage;
	public GameObject panel;



    void Start()
    {
        base.behaviourManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BehaviourAdder>();
    }
    public void configurePlayer(Color playerColor)
    {
        int numberOfAgents = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().numberOfIAs;
        TrustInOthers = new int[numberOfAgents];
        initializeTrustInOthers(numberOfAgents);
        health = 100;
        attack = 10;
        this.GetComponent<SpriteRenderer>().color = playerColor;
        MyOwnIndex = numberOfAgents - 1;
    }

	public override void takeDamage(int damage, PersonalityBase personality)
	{
		health -= (int)(damage * defense);
        HealthImage.GetComponent<Image>().fillAmount = health / 100f;
        //Debug.Log(HealthImage.GetComponent<Image>().fillAmount);
        if (health <= 50 && health > 33)
		{
			HealthImage.GetComponent<Image>().color = new Color(255, 255, 0);
		}
		else if (health <= 33 && health > 0)
		{
			HealthImage.GetComponent<Image>().color = new Color(255, 0, 0);
		}
		else
		{
			VisibleElements.visibleGameObjects.Remove(this.gameObject);

			gameController gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<gameController> ();

			//Debug.Log ("humanos: " + gc.numberOfHumans + " monstruos: " + gc.numberOfMonsters);

		
				if (personality.isMonster) {
					theThing = true;
					health = 100;
				} else {
					youLost ();
				}

          
		}
	}

	IEnumerator youLost(){
		Debug.Log ("HAS PERDIDO ");
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<gameController> ().textMonster.text = "YOU LOST";
		yield return new WaitForSeconds(3f);

		SceneManager.LoadScene (0);
	}
}
