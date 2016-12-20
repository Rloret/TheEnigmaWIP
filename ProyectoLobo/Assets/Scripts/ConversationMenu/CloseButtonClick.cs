using UnityEngine;
using System.Collections;

public class CloseButtonClick : ButtonAction {
	private GameObject targetIA;

    public override void Action()
    {
        //Debug.Log("closeMenu");
		targetIA = menuController.GetTargetIA();
		targetIA.GetComponent<VisibilityConeCycleIA>().enabled=true;

        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
