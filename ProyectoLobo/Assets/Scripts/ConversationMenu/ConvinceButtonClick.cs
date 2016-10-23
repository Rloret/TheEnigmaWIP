using UnityEngine;
using System.Collections;

public class ConvinceButtonClick : ButtonAction {

    public override void Action()
    {
        Debug.Log("convinceAction");
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
