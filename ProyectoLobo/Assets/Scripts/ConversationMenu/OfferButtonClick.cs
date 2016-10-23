using UnityEngine;
using System.Collections;

public class OfferButtonClick : ButtonAction {

    public override void Action()
    {
        Debug.Log("offerAction");
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
