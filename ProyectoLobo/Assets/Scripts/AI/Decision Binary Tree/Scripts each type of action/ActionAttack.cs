using UnityEngine;
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
        
        
        if (this!=null && this.gameObject.tag != "Player") {
			GameObject target;
			if (this.GetComponent<DecisionTreeCreator> () != null) {
				target= this.GetComponent<DecisionTreeCreator>().target;
			}
			else{
				target = targetAttack;
			}
            base.DestroyTrees ();
            if (target==null ||target.GetComponent<PersonalityBase>().health > 0) 
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
            Destroy(n);
        }

    }

    void Attack(int a)
    {

        var arbol = this.GetComponent<DecisionTreeCreator>();
        if (arbol != null)
        {
            try
            {
                if (!triggered)
                {
                    targetAttack = arbol.target;
                }
            }
            catch
            {
                Debug.LogError("Ha ocurrido error en " + this.name);
            }

            PersonalityBase targetPers = targetAttack.GetComponent<PersonalityBase>();

            GroupScript targetGroup = targetPers.gameObject.GetComponent<GroupScript>();
            if (targetGroup.groupLeader == this.gameObject.GetComponent<GroupScript>().groupLeader)
            {
                if (targetGroup.groupMembers.Count > 0)
                {
                    if (targetGroup.IAmTheLeader)
                    {
                        var members = targetGroup.groupMembers;
                        targetGroup.ExitGroup();
                        foreach (var m in members)
                        {
                            GroupScript memberGroup = m.GetComponent<GroupScript>();
                            memberGroup.groupLeader = members[0];
                        }
                    }
                    else
                    {
                        targetGroup.ExitGroup();
                    }
                }
            }
            updateTrust(false, targetPers, this.GetComponent<PersonalityBase>().GetMyOwnIndex());
            targetPers.takeDamage(a, this.GetComponent<PersonalityBase>());

            //HAY QUE RECORRER EL GRUPO DEL TARGET Y REDUCIR LA CONFIANZA DE TODOS
        }
    }
}
