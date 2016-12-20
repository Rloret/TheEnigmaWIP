using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerPersonality : PersonalityBase {
	public GameObject HealthImage;
	public GameObject panel;



    void Start()
    {
        base.behaviourManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BehaviourAdder>();
        HealthImage.GetComponent<Image>().color = new Color(82f / 255, 178f / 255, 82f / 255);
       
    }
    public void configurePlayer(Color playerColor)
    {
        int numberOfAgents = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>().numberOfIAs;
        TrustInOthers = new int[numberOfAgents];
        initializeTrustInOthers(numberOfAgents);
        health = 100;
        HealthImage.GetComponent<Image>().fillAmount =1;
        attack = 10;
        this.GetComponent<GroupScript>().setOriginalColor(playerColor);
        this.GetComponent<SpriteRenderer>().color = playerColor;
        MyOwnIndex = numberOfAgents - 1;
    }

	public override void takeDamage(int damage, PersonalityBase personality)
	{
		health -= (int)(damage * defense);
        HealthImage.GetComponent<Image>().fillAmount = health / 100f;

        Debug.Log(HealthImage.GetComponent<Image>().fillAmount);
        if (health > 50) {
            HealthImage.GetComponent<Image>().color = new Color(82f / 255, 178f / 255, 82f / 255);
        }
        else if (health <= 50 && health > 33)
		{
			HealthImage.GetComponent<Image>().color = new Color(226f / 255, 213f / 255, 89f / 255);
		}
		else if (health <= 33 && health > 0)
		{
			HealthImage.GetComponent<Image>().color = new Color(193f / 255, 52 / 255f, 52f / 255) ;
		}
		else if (health <= 0)
		{
			VisibleElements.visibleGameObjects.Remove(this.gameObject);

			gameController gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<gameController> ();
					
			if (personality.isMonster && !theThing) {
					theThing = true;
					health = 100;
					Debug.Log ("soy humano y me infectan");
					gc.numberOfMonsters++;
					GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerMenuController>().CheckIfMonster();
					gc.decreaseHumans ();



				} else {
					youLost ();
				}
			Debug.Log ("humanos: " + gc.numberOfHumans + " monstruos: " + gc.numberOfMonsters);

          
		}
	}

	IEnumerator youLost(){
		Debug.Log ("HAS PERDIDO ");
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<gameController> ().textMonster.text = "YOU LOST";
		yield return new WaitForSeconds(3f);

		SceneManager.LoadScene (0);
	}
}
