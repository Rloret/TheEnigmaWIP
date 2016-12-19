using UnityEngine;
using System.Collections;

public class ActionAcceptObjectOffered : Action {

    public override void DoAction()
    {

		//increase confianza

        Reaction.spawnReaction(ResponseController.responseEnum.OFFER, ResponseController.responseEnum.OK, this.gameObject);


			Debug.Log(" acepto objeto. Antes tenia: "  + this.GetComponent<PersonalityBase>().myObject);

			GameObject t = this.GetComponent<DecisionTreeCreator> ().target;

			this.GetComponent<PersonalityBase>().myObject=t.GetComponent<PersonalityBase>().myObject;
			this.GetComponent<ObjectHandler> ().currentObject = t.GetComponent<ObjectHandler> ().currentObject;
			this.GetComponent<ObjectHandler> ().hasObject = true;


			t.GetComponent<PersonalityBase> ().myObject = ObjectHandler.ObjectType.NONE;
			t.GetComponent<ObjectHandler> ().currentObject = null;
			t.GetComponent<ObjectHandler> ().hasObject = false;

			Debug.Log(" acepto objeto. despues tengo: "  + this.GetComponent<PersonalityBase>().myObject);


			base.DestroyTrees ();

			Invoke ("EnableCone", 2f);

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
