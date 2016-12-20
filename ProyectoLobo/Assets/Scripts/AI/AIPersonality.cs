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
    public Sprite ghostSprite;

	GroupScript aiGroup;
	VisibilityConeCycleIA cone;
	gameController controller;


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
		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<gameController>();
        numberOfIAs = controller.GetComponent<gameController>().numberOfIAs;
        TrustInOthers = new int[numberOfIAs]; 
        myMemory = GetComponent<Memory>();
		base.behaviourManager = controller.GetComponent<BehaviourAdder>();
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
					//attackAction ();
				}

			}
			else if (health <= 50) {
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
//		Debug.Log ("convirtiendome");
		attack = 20;
		GetComponent<AgentPositionController>().maxLinearVelocity = 150;
		GetComponent<SpriteRenderer>().sprite = playerMonsterStateImage;
		isMonster = true;

		GroupScript myGroup=this.GetComponent<GroupScript>();
		if (myGroup.groupMembers.Count > 1) {
			Debug.LogError ("hay mas de uno");

		} else if (myGroup.groupMembers.Count > 0) {
			if (myGroup.IAmTheLeader) {

				myGroup.groupMembers [0].GetComponent<GroupScript> ().ExitGroup ();

			} else {
				myGroup.ExitGroup ();
			}
		}



	}
	void attackAction(){
		ActionAttack a= gameObject.AddComponent<ActionAttack> ();
		//a.targetAttack = aiGroup.groupMembers [0];
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

    public override void takeDamage(int damage, PersonalityBase personality)
    {
        health -= (int)(damage * defense);
        HealthImage.GetComponent<Image>().fillAmount = health / 100f;
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

			if (personality.isMonster && !theThing)
            {
				theThing = true;

				HealthImage.GetComponent<Image>().color = new Color(0, 255, 0);

				controller.numberOfMonsters++;
				controller.decreaseHumans ();
				Debug.Log ("me mata un puto monstruo y me he convertido.\thumanos: " + controller.numberOfHumans + " monstruos: " + controller.numberOfMonsters);


            }
			else 
            {
				if (theThing) {
					controller.numberOfMonsters--;
					if (controller.CheckPlayerWin ()) {
						//Debug.Log ("ha devuelto true en win");
						controller.youWin (true);
					} else if (controller.CheckPlayerLost ()) {
						//Debug.Log ("ha devuelto true en lost");
						controller.youWin (false);

					}
				}
					
				else 
					controller.decreaseHumans ();

				Debug.Log ("humanos: " + controller.numberOfHumans + " monstruos: " + controller.numberOfMonsters + "muere " + this.name);

				this.GetComponent<VisibilityConeCycleIA>().enabled = false;
				VisibleElements.visibleGameObjects.Remove(this.gameObject);
				string nameIAdeath = this.name+ "ghost";
				Vector3 IADeathPosition = this.transform.position;

				this.enabled = false;

                var mygroup = this.GetComponent<GroupScript>();

                if (mygroup.groupMembers.Count > 0)
                {
                    if (mygroup.IAmTheLeader)
                    {
                        var members = mygroup.groupMembers;
                        mygroup.ExitGroup();
                        foreach (var m in members)
                        {
                            GroupScript memberGroup = m.GetComponent<GroupScript>();
                            memberGroup.groupLeader = members[0];
                        }
                        members[0].GetComponent<GroupScript>().makeLeader();
                        members[0].GetComponent<PersonalityBase>().formacionGrupo(members[0], members[0].GetComponent<GroupScript>());
                    }
                    else
                    {
                        mygroup.ExitGroup();
                    }
                }

                var IAs = GameObject.FindGameObjectsWithTag("IA");

                foreach (var ia in IAs)
                {
                   var arbol= ia.GetComponent<DecisionTreeCreator>();
                    if (arbol != null)
                    {
                        if(arbol.target == this.gameObject)
                        {
                            Destroy(this.GetComponent<DecisionTreeCreator>());
                            
                            ia.GetComponent<AIPersonality>().oldNodes = ia.GetComponents<DecisionTreeNode>();
                            foreach (var item in ia.GetComponent<AIPersonality>().oldNodes)
                            {
                                DestroyImmediate(item);
                            }

                        }
                    }
                }

				PlayerMenuController menu = controller.GetComponent<PlayerMenuController> ();
				menu.CloseAttackMenu ();
				menu.menuConversation.SetActive(false);
				menu.CloseJoinMenu ();
				menu.CloseObjectMenu ();


                GameObject ghost = new GameObject();
                ghost.AddComponent<SpriteRenderer>();
                ghost.GetComponent<SpriteRenderer>().sprite = ghostSprite;
                ghost.GetComponent<SpriteRenderer>().sortingLayerName = "Personajes";
                ghost.transform.localScale = new Vector2(5f, 5f);
                ghost.name = nameIAdeath;
                ghost.transform.position = IADeathPosition;


				Destroy(personality.gameObject.GetComponent<Pursue> ());

				this.gameObject.SetActive (false);
				Invoke ("DestroyIA", 1f);

            }    
        }

    }

	void DestroyIA(){
		foreach (var v in GetComponents<MonoBehaviour>()) {
			Destroy (v);
		}
		Destroy(this.gameObject);

	}
}
