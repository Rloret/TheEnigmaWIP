using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AIPersonality: PersonalityBase {

	public DecisionTreeNode[] oldNodes;
	public GameObject HealthImage;
	public GameObject panel;

	private Sprite playerNormalStateImage;
	public Sprite playerMonsterStateImage;

    public Memory myMemory;
    public int numberOfIAs;

	GroupScript aiGroup;
	VisibilityConeCycleIA cone;

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
        base.behaviourManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BehaviourAdder>();
       // interactionFromOtherCharacter = ActionsEnum.Actions.ATTACK;
        initializeTrustInOthers(numberOfIAs);
		aiGroup = GetComponent<GroupScript> ();
		cone = GetComponent<VisibilityConeCycleIA> ();
		playerNormalStateImage = gameObject.GetComponent<SpriteRenderer> ().sprite;
    }

	void Update(){
	
		if (theThing && !isMonster) {
			if (aiGroup.inGroup && aiGroup.groupMembers.Count==1) {
				if (cone.visibleGameobjects.Count <= 1) {
					convertToMonster ();
					attackAction ();
				}

			}
			if (health <= 50) {
				convertToMonster ();
			}
		} 
		else if (theThing) {
			if (cone.visibleGameobjects.Count > 1) {
				returnToHuman ();
			}
		
		}
	}


	void convertToMonster(){
		lastAttackValue = attack;
		Debug.Log ("convirtiendome");
		attack = 20;
		GetComponent<AgentPositionController>().maxLinearVelocity = 150;
		GetComponent<SpriteRenderer>().sprite = playerMonsterStateImage;
		isMonster = true;



	}
	void attackAction(){
		ActionAttack a= gameObject.AddComponent<ActionAttack> ();
		a.targetAttack = aiGroup.groupMembers [0];
		a.triggered = true;
		a.DoAction ();}


	void returnToHuman(){
		attack = lastAttackValue;
		GetComponent<AgentPositionController>().maxLinearVelocity = 150;
		GetComponent<SpriteRenderer>().sprite= playerNormalStateImage;
		isMonster = false;
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
        if (health <= 50 && health > 33)
        {
            HealthImage.GetComponent<Image>().color = new Color(255, 255, 0);
        }
        else if (health <= 33 && health > 0)
        {
            HealthImage.GetComponent<Image>().color = new Color(255, 0, 0);
        }
        else if(health<=0)
        {
            Debug.Log("Recibiendo daño: " +this.gameObject);
            this.GetComponent<VisibilityConeCycleIA>().enabled = false;
            VisibleElements.visibleGameObjects.Remove(this.gameObject);
            Debug.Log(this.gameObject.name + "HA MUERTO");
            this.enabled = false;
            Destroy(this.gameObject);
          /* var comportam= this.GetComponents<MonoBehaviour>();
            foreach (var compo in comportam)
            {
                Destroy(compo);
            }
            var treenodes = this.GetComponents<DecisionTreeNode>();
            foreach (var compo in treenodes)
            {
                Destroy(compo);
            }*/
        }
    }
}
