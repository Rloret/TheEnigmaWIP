using UnityEngine;
using System.Collections;

public class ConversationMenuController:MonoBehaviour  {

    private GameObject targetIA;
    public GameObject menu;  // conversation menu is in player

    private Renderer render;

    void Start() {
        render = menu.GetComponent<SpriteRenderer>();
    }
    public void OpenConversationMenu(GameObject character, GameObject targetIA)
    {
        this.targetIA = targetIA;
        menu.transform.position = targetIA.transform.position;
        menu.transform.rotation = Quaternion.Euler(Vector3.zero);
        menu.SetActive(true);
    }

    public void CloseConversationMenu() {
        menu.SetActive(false);
    }
}
