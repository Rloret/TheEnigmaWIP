﻿using UnityEngine;
using System.Collections;

public class ActionAttack : Action {


    public GameObject targetAttack;
	public bool triggered = false; //Para saber cuando se fuerza la accion
    private PlayerMenuController playerMenuController;

    public override void DoAction()
    {

        GameObject target;
        if (this.GetComponent<DecisionTreeCreator>() != null)
        {
            target = this.GetComponent<DecisionTreeCreator>().target;
        }
        else
        {
            target = targetAttack;
        }



        GroupScript myGroup = this.gameObject.GetComponent<GroupScript>();
        // GameObject target = this.GetComponent<DecisionTreeCreator>().target;
        GroupScript attackedGroup = target.gameObject.GetComponent<GroupScript>(); ;
        if (myGroup.groupLeader != attackedGroup.groupLeader && myGroup.inGroup)
        {
            myGroup.groupLeader.GetComponent<PersonalityBase>().formacionAtaque(target, myGroup.groupLeader.GetComponent<GroupScript>());
            Invoke("waitForAttack", 2f);
        }
        else
        {
//            Debug.Log("no tengo que formar ataco");
            waitForAttack();
        }



    }
    private void waitForAttack()
    {
        Reaction.spawnReaction(ResponseController.responseEnum.ATTACK, ResponseController.responseEnum.ATTACK, this.gameObject);
        // Debug.Log("voy a atacar . Soy "+this.gameObject.name);


        GroupScript myGroup = this.GetComponent<GroupScript>();
        int totalAttack = 0;

        if (this.gameObject.tag == "Player")
        {
            totalAttack = this.GetComponent<PlayerPersonality>().attack;


        }
        else
        {
            totalAttack = this.GetComponent<AIPersonality>().attack;

        }


        foreach (var member in myGroup.groupMembers)
        {

            totalAttack += member.GetComponent<PersonalityBase>().attack;
            //animacion numeritos
        }

        Attack(totalAttack);
        //END ATTACK

        //GameObject target = this.GetComponent<DecisionTreeCreator>().target;
        GameObject leader = myGroup.groupLeader;
        leader.GetComponent<PersonalityBase>().formacionGrupo(leader, leader.GetComponent<GroupScript>());
        
        if (this.gameObject.tag != "Player") {
			GameObject target;
			if (this.GetComponent<DecisionTreeCreator> () != null) {
				target= this.GetComponent<DecisionTreeCreator>().target;
			}
			else{
				target = targetAttack;
			}
            base.DestroyTrees ();
            if (target.GetComponent<PersonalityBase>().health > 0) 
			    Invoke ("EnableCone", 1f);
		}
      
    }

    private void EnableCone()
    {

//        this.GetComponent<AgentPositionController>().orientation += 180;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerMenuController>().CloseAttackMenu();

        GetComponent<VisibilityConeCycleIA>().enabled = true;
        base.visibiCone.IDecided = false;

        foreach (DecisionTreeNode n in this.gameObject.GetComponent<AIPersonality>().oldNodes)
        {
            DestroyImmediate(n);
        }

    }

    void Attack(int a)
    {
        if (!triggered) targetAttack = this.GetComponent<DecisionTreeCreator>().target;

		PersonalityBase targetPers = targetAttack.GetComponent<PersonalityBase> ();

		GroupScript myGroup = targetPers.gameObject.GetComponent<GroupScript> ();
		if (myGroup.groupMembers.Count > 0) {
			if (myGroup.IAmTheLeader) {
				foreach (var m in myGroup.groupMembers) {
					m.GetComponent<GroupScript> ().ExitGroup ();
				}

			} else {
				myGroup.ExitGroup ();
			}
		}
        targetPers.takeDamage(a, this.GetComponent<PersonalityBase>());
        updateTrust(false, targetPers, this.GetComponent<PersonalityBase>().GetMyOwnIndex());
        //HAY QUE RECORRER EL GRUPO DEL TARGET Y REDUCIR LA CONFIANZA DE TODOS
    }
}
