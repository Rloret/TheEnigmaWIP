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

	public override void takeDamage(int damage, PersonalityBase personality)
	{
		health -= (int)(damage * defense);
        HealthImage.GetComponent<Image>().fillAmount = health / 100f;
        Debug.Log(HealthImage.GetComponent<Image>().fillAmount);
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
            //contador_humanos == 1;
            // youLost();
            //else
            //si te mata el monstruo: the thing = true
            //sino youLost()
			youLost ();
		}
	}

	private void youLost(){
		Debug.Log ("HAS PERDIDO ");
	}
}
