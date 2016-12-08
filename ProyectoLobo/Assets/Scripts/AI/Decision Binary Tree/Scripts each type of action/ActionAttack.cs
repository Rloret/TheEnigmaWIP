using UnityEngine;
using System.Collections;

public class ActionAttack : Action {



    public override void DoAction()
    {


        Debug.Log("voy a atacar y bajo confianza. Soy "+this.gameObject.name);
        base.DestroyTrees();

        // atack()
        // decrease friendship 
        GroupScript myGroup = this.GetComponent<GroupScript>();
        int totalAttack = this.GetComponent<AIPersonality>().attack;

        foreach (var member in myGroup.groupMembers) {

            totalAttack += member.GetComponent<AIPersonality>().attack;
            //animacion numeritos
        }

        Attack(totalAttack);
        Invoke("EnableCone", 10f);
    }

    private void EnableCone()
    {
        GetComponent<VisibilityConeCycleIA>().enabled = true;
        base.visibiCone.IDecided = false;

    }

    void Attack(int a) {
        Debug.Log("atacamos un total de " + a);
    }

}
