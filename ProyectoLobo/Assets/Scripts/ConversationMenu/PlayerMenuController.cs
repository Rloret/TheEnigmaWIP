using UnityEngine;
using System.Collections;

public class PlayerMenuController : MonoBehaviour {

	private GameObject targetIA;

	public GameObject menuConversation;  // conversation menu is in player
	public GameObject menuAttacked; // menu when someone attacks you
	public GameObject menuOfferedJoinGroup;// menu when someone offers you to join his group
	public GameObject menuObjectOffered;

	public enum MenuTypes{MENU_CONVERSATION, MENU_ATTACKED, MENU_OFFERED_JOIN, MENU_OFFERED_OBJECT};

	private Renderer render;

	void Start() {
		render =  menuConversation.GetComponent<SpriteRenderer>();

	}
	/*  public void OpenConversationMenu(GameObject character, GameObject targetIA)
    {
        this.targetIA = targetIA;
		menuConversation.transform.position = targetIA.transform.position;
		menuConversation.transform.rotation = Quaternion.Euler(Vector3.zero);
		targetIA.GetComponent<VisibilityConeCycleIA>().enabled = false;

		menuConversation.SetActive(true);
    }*/

	public void CloseConversationMenu() {
		menuConversation.SetActive(false);
	}

	public GameObject GetTargetIA() {
		return targetIA;
	}

	/*public void OpenAttackedMenu(GameObject targetIA){
		this.targetIA = targetIA;
		menuAttacked.transform.position = targetIA.transform.position;
		menuAttacked.transform.rotation = Quaternion.Euler(Vector3.zero);

		menuAttacked.SetActive(true);
	}
   */
	public void OpenMenu(MenuTypes menu, GameObject target){

		Debug.Log ("target IA es " + target);

		this.targetIA = target;
		targetIA.GetComponent<VisibilityConeCycleIA>().enabled = false;

		switch (menu) {
		case MenuTypes.MENU_ATTACKED:

			menuAttacked.transform.position = targetIA.transform.position;
			menuAttacked.transform.rotation = Quaternion.Euler(Vector3.zero);
			menuAttacked.SetActive(true);
			break;
		case MenuTypes.MENU_CONVERSATION:
			menuConversation.transform.position = targetIA.transform.position;
			menuConversation.transform.rotation = Quaternion.Euler(Vector3.zero);
			menuConversation.SetActive(true);
			break;
		case MenuTypes.MENU_OFFERED_JOIN:
			menuOfferedJoinGroup.transform.position = targetIA.transform.position;
			menuOfferedJoinGroup.transform.rotation = Quaternion.Euler(Vector3.zero);
			menuOfferedJoinGroup.SetActive(true);
			break;
		case MenuTypes.MENU_OFFERED_OBJECT:
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
