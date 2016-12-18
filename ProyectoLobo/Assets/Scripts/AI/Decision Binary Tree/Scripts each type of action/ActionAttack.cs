using UnityEngine;
using System.Collections;

public class ActionAttack : Action {


    public override void DoAction()
    {

        Reaction.spawnReaction(ResponseController.responseEnum.ATTACK, ResponseController.responseEnum.ATTACK, this.gameObject);
        Debug.Log("voy a atacar y bajo confianza. Soy "+this.gameObject.name);

		//ATTACK

		// decrease friendship 
		GroupScript myGroup = this.GetComponent<GroupScript>();
		int totalAttack = 0;

		if (this.gameObject.tag == "Player") {
			totalAttack = this.GetComponent<PlayerPersonality>().attack;

			
		} else {
			totalAttack = this.GetComponent<AIPersonality>().attack;

		}


		foreach (var member in myGroup.groupMembers) {

			totalAttack += member.GetComponent<AIPersonality>().attack;
			//animacion numeritos
		}

		Attack(totalAttack);
		//END ATTACK

		if (this.gameObject.tag != "Player") {
			base.DestroyTrees ();

        
			Invoke ("EnableCone", 3f);
		}
    }

    private void EnableCone()
    {
		this.GetComponent<AgentPositionController> ().orientation += 180;
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerMenuController> ().CloseAttackMenu ();

        GetComponent<VisibilityConeCycleIA>().enabled = true;
        base.visibiCone.IDecided = false;

		foreach (DecisionTreeNode n in this.gameObject.GetComponent<AIPersonality>().oldNodes) {
			DestroyImmediate (n);
		}

    }

    void Attack(int a) {
        this.GetComponent<DecisionTreeCreator>().target.GetComponent<AIPersonality>().takeDamage(a);
        Debug.Log("atacamos un total de " + a);
        
    }

}
