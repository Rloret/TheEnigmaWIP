using UnityEngine;
using System.Collections;

public class ButtonAction:MonoBehaviour  {

	protected PlayerMenuController menuController;
    protected GroupScript playerGroup;

    protected void Start()
    {
		menuController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerMenuController>();
        playerGroup = this.GetComponent<GroupScript>();

    }

    public virtual void Action() {
        Debug.Log("Action");
    }
}
