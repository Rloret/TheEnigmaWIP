using UnityEngine;
using System.Collections;

public class ActionAttack : Action {


	public GameObject targetAttack;
	public bool triggered = false; //Para saber cuando se fuerza la accion

    public override void DoAction()
    {

        Reaction.spawnReaction(ResponseController.responseEnum.ATTACK, ResponseController.responseEnum.ATTACK, this.gameObject);
       // Debug.Log("voy a atacar . Soy "+this.gameObject.name);


		GroupScript myGroup = this.GetComponent<GroupScript>();
		int totalAttack = 0;

		if (this.gameObject.tag == "Player") {
			totalAttack = this.GetComponent<PlayerPersonality>().attack;

			
		} else {
			totalAttack = this.GetComponent<AIPersonality>().attack;

		}


		foreach (var member in myGroup.groupMembers) {

			totalAttack += member.GetComponent<PersonalityBase>().attack;
			//animacion numeritos
		}

		Attack(totalAttack);
		//END ATTACK

		if (this.gameObject.tag != "Player") {
			base.DestroyTrees ();

			Invoke ("EnableCone", 1f);
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
		//Debug.Log ("ataco y hago : " + a );
		if(!triggered)targetAttack=this.GetComponent<DecisionTreeCreator> ().target;

		PersonalityBase targetPers = targetAttack.GetComponent<PersonalityBase> ();
        targetPers.takeDamage(a);

		/*if (this.gameObject.tag == "Player") {
			
			targetPers.TrustInOthers[this.gameObject.GetComponent<PlayerPersonality>().GetMyOwnIndex()]-=1;
			Debug.Log("Soy player, reduzco mi confi y me atacan un total de " + a);


		} else {
			
			targetPers.TrustInOthers[this.gameObject.GetComponent<AIPersonality>().GetMyOwnIndex()]-=1;
			Debug.Log("Soy IA, reduzco mi confi y me atacan un total de " + a);

		}*/

		updateTrust (false, targetPers, this.GetComponent<PersonalityBase> ().GetMyOwnIndex ());



		//HAY QUE RECORRER EL GRUPO DEL TARGET Y REDUCIR LA CONFIANZA DE TODOS 
        
    }

}
