using UnityEngine;
using System.Collections;

public class ActionAcceptObjectOffered : Action {

    public override void DoAction()
    {

		//increase confianza

        Reaction.spawnReaction(ResponseController.responseEnum.OFFER, ResponseController.responseEnum.OK, this.gameObject);

		if (this.gameObject.tag == "Player") {
			Debug.Log("soy player y acepto objeto. Antes tenia: "  + this.GetComponent<PlayerPersonality>().myObject);
			GameObject t = GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerMenuController> (). GetTargetIA();
			this.GetComponent<PlayerPersonality>().myObject=t.GetComponent<PersonalityBase>().myObject;
			this.GetComponent<ObjectHandler> ().currentObject = t.GetComponent<ObjectHandler> ().currentObject;

			t.GetComponent<PersonalityBase> ().myObject = ObjectHandler.ObjectType.NONE;
			t.GetComponent<ObjectHandler> ().currentObject = null;
			Debug.Log("soy player y acepto objeto. despues tengo: "  + this.GetComponent<PlayerPersonality>().myObject);

		}

		else {
			Debug.Log("soy ia y acepto objeto. Antes tenia: "  + this.GetComponent<PersonalityBase>().myObject);

			GameObject t = this.GetComponent<DecisionTreeCreator> ().target;

			this.GetComponent<PlayerPersonality>().myObject=t.GetComponent<PersonalityBase>().myObject;
			this.GetComponent<ObjectHandler> ().currentObject = t.GetComponent<ObjectHandler> ().currentObject;

			t.GetComponent<PersonalityBase> ().myObject = ObjectHandler.ObjectType.NONE;
			t.GetComponent<ObjectHandler> ().currentObject = null;

			Debug.Log("soy ia y acepto objeto. despues tengo: "  + this.GetComponent<PersonalityBase>().myObject);


			base.DestroyTrees ();

			Invoke ("EnableCone", 10f);
		}
    }

    private void EnableCone()
    {
        GetComponent<VisibilityConeCycleIA>().enabled = true;
        base.visibiCone.IDecided = false;
		foreach (DecisionTreeNode n in this.gameObject.GetComponent<AIPersonality>().oldNodes) {
			DestroyImmediate (n);
		}

    }
}
