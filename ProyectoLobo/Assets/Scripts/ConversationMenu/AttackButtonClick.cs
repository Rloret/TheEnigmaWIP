using UnityEngine;
using System.Collections;

public class AttackButtonClick : ButtonAction {

    public override void Action()
    {
        Debug.Log("attackAction");
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
