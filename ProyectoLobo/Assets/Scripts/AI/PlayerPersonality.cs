using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerPersonality : PersonalityBase {
	public GameObject HealthImage;
	public GameObject panel;

    public void configurePlayer()
    {
        int numberOfAgents = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().numberOfIAs;
        TrustInOthers = new int[numberOfAgents];
        initializeTrustInOthers(numberOfAgents);
        health = 100;
        attack = 10;

        MyOwnIndex = numberOfAgents - 1;
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
		else
		{
			VisibleElements.visibleGameObjects.Remove(this.gameObject);
			youLost ();
		}
	}

	private void youLost(){
		Debug.Log ("HAS PERDIDO ");
	}
}
