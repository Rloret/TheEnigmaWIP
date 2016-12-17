using UnityEngine;
using System.Collections;

public class ButtonAction:MonoBehaviour  {

	protected PlayerMenuController menuController;

    protected void Start()
    {
		menuController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerMenuController>();

    }

    public virtual void Action() {
        Debug.Log("Action");
    }
}
