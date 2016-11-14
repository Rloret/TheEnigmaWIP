using UnityEngine;
using System.Collections;

public class ButtonAction:MonoBehaviour  {

    protected ConversationMenuController menuController;

    protected void Start()
    {
        menuController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ConversationMenuController>();

    }

    public virtual void Action() {
        Debug.Log("Action");
    }
}
