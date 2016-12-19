using UnityEngine;
using System.Collections;

public class Action : DecisionTreeNode {

    public VisibilityConeCycleIA visibiCone;
    public ResponseController Reaction;

    private void Awake() {
        visibiCone = this.GetComponent<VisibilityConeCycleIA>();
        Reaction = GameObject.FindGameObjectWithTag("GameController").GetComponent<ResponseController>();

    }
    public override DecisionTreeNode MakeDecision()
    {

        Debug.Log("estoy en makedecision() de action");
        return this;
    }


    public virtual void DoAction() {
        Debug.LogError("doAction de padre action");
        return;
    }

    protected void DestroyTrees() {
        if (this.GetComponent<DecisionTreeISeeSomeoneWhatShouldIDo>() != null)Destroy( this.GetComponent<DecisionTreeISeeSomeoneWhatShouldIDo>());
        if (this.GetComponent<DecisionTreeReactionAfterInteraction>() != null) Destroy(this.GetComponent<DecisionTreeReactionAfterInteraction>());

		this.gameObject.GetComponent<AIPersonality>().oldNodes= this.gameObject.GetComponents<DecisionTreeNode>();

		//Debug.Log ("nodos viejos: " + this.gameObject.GetComponent<AIPersonality>().oldNodes.Length);

    }

	protected void updateTrust(bool increase, PersonalityBase pers, int index){

	//	Debug.Log ("se esta actualizand la confianza de : " + pers.gameObject.name + " indice: " + index);


		if (increase) {
			pers.TrustInOthers [index] += 1;
		} else {
			pers.TrustInOthers [index] -= 1;
		}
	}

}
