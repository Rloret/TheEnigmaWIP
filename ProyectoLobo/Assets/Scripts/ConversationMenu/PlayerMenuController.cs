using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMenuController : MonoBehaviour {

	private GameObject targetIA;
    private GameObject player;
    private PersonalityBase playerPersonality;
    private SpriteRenderer playerSprite;

    public Sprite playerNormalStateImage;
    public Sprite playerMonsterStateImage;
    public GameObject menuConversation;  // conversation menu is in player
	public GameObject menuAttacked; // menu when someone attacks you
	public GameObject menuOfferedJoinGroup;// menu when someone offers you to join his group
	public GameObject menuObjectOffered;
    public Button buttonConvert;

    public enum MenuTypes{MENU_CONVERSATION, MENU_ATTACKED, MENU_OFFERED_JOIN, MENU_OFFERED_OBJECT};

	private Renderer render;

   

	void Start() {
		render =  menuConversation.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPersonality = player.GetComponent<PersonalityBase>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        CheckIfMonster();
    }
	/*  public void OpenConversationMenu(GameObject character, GameObject targetIA)
    {
        this.targetIA = targetIA;
		menuConversation.transform.position = targetIA.transform.position;
		menuConversation.transform.rotation = Quaternion.Euler(Vector3.zero);
		targetIA.GetComponent<VisibilityConeCycleIA>().enabled = false;

		menuConversation.SetActive(true);
    }*/

    private void CheckIfMonster()
    {
        if(playerPersonality.theThing)
        {
            buttonConvert.enabled = true;
            buttonConvert.image.enabled = true;
            buttonConvert.GetComponentInChildren<Text>().enabled = true;
        }
        else
        {
            buttonConvert.enabled = false;
            buttonConvert.image.enabled = false;
            buttonConvert.GetComponentInChildren<Text>().enabled = false;
        }

    }
     
    public void Convert2Monster()
    {
        Debug.Log("OnClick");
        if (playerPersonality.isMonster)
        {
			playerPersonality.attack = playerPersonality.lastAttackValue ;
            playerPersonality.GetComponent<AgentPositionController>().maxLinearVelocity = 150;
            playerSprite.sprite = playerNormalStateImage;
            playerPersonality.isMonster = false;

        }
        else //si NO estás convertido
        {
            Debug.Log("Convirtiendose en monstruo");
			playerPersonality.lastAttackValue =playerPersonality.attack;

            playerPersonality.attack = 20;
            playerPersonality.GetComponent<AgentPositionController>().maxLinearVelocity = 150;
            playerSprite.sprite = playerMonsterStateImage;
            playerPersonality.isMonster = true;
            //cambiar el sprite

        }
    }

	public void CloseConversationMenu() {
		menuConversation.SetActive(false);
		targetIA.GetComponent<VisibilityConeCycleIA> ().enabled = true;
	}
	public void CloseAttackMenu() {
		menuAttacked.SetActive(false);
	}
	public void CloseObjectMenu() {
		menuObjectOffered.SetActive(false);
	}
	public void CloseJoinMenu() {
		menuOfferedJoinGroup.SetActive(false);
	}

	public GameObject GetTargetIA() {
		return targetIA;
	}
	public void SetTargetIA(GameObject t) {
		 targetIA=t;
	}
	/*public void OpenAttackedMenu(GameObject targetIA){
		this.targetIA = targetIA;
		menuAttacked.transform.position = targetIA.transform.position;
		menuAttacked.transform.rotation = Quaternion.Euler(Vector3.zero);

		menuAttacked.SetActive(true);
	}
   */
	public void OpenMenu(MenuTypes menu, GameObject target){

        //Debug.Log ("target IA es " + target);
        Debug.Log("abro puto menu");
        this.targetIA = target;


		switch (menu) {
		case MenuTypes.MENU_ATTACKED:
			Debug.Log("abro menu attack");

			menuAttacked.transform.position = targetIA.transform.position;
			menuAttacked.transform.rotation = Quaternion.Euler(Vector3.zero);
			menuAttacked.SetActive(true);
			break;


		case MenuTypes.MENU_CONVERSATION:
			Debug.Log("abro menu conver");

			menuConversation.transform.position = targetIA.transform.position;
			menuConversation.transform.rotation = Quaternion.Euler(Vector3.zero);
			menuConversation.SetActive(true);
			break;


		case MenuTypes.MENU_OFFERED_JOIN:

			Debug.Log("abro menu join");

			menuOfferedJoinGroup.transform.position = targetIA.transform.position;
			menuOfferedJoinGroup.transform.rotation = Quaternion.Euler(Vector3.zero);
			menuOfferedJoinGroup.SetActive(true);
			break;


		case MenuTypes.MENU_OFFERED_OBJECT:
			Debug.Log("abro menu object");

			menuObjectOffered.transform.position = targetIA.transform.position;
			menuObjectOffered.transform.rotation = Quaternion.Euler(Vector3.zero);
			menuObjectOffered.SetActive(true);
			break;

		default:
			Debug.LogError ("menu type incorrect");
			break;
		}

	}


}
