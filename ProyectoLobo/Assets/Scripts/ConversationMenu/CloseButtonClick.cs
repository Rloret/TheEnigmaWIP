using UnityEngine;
using System.Collections;

public class CloseButtonClick : ButtonAction {

    public override void Action()
    {
        Debug.Log("closeMenu");
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
