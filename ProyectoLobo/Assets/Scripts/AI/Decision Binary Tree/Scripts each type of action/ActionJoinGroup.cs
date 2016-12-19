using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionJoinGroup : Action
{
    public override void DoAction()
    {
        base.visibiCone.IDecided = false;
        Reaction.spawnReaction(ResponseController.responseEnum.GROUP, ResponseController.responseEnum.OK, this.gameObject);
       // Debug.Log("me uno a tu grupo");

        //Debug.Log("mytarget es " + this.GetComponent<DecisionTreeCreator>().target);

        GameObject t = this.GetComponent<DecisionTreeCreator>().target;
        GroupScript myGroup = this.GetComponent<GroupScript>();
        GroupScript leadergroup = t.GetComponent<GroupScript>();
        t = leadergroup.groupLeader;

        myGroup.groupLeader = t;
        myGroup.inGroup = true;
        myGroup.IAmTheLeader = false;
        leadergroup.updateGroups(this.gameObject, myGroup.groupMembers);
        leadergroup.makeLeader();
        myGroup.addSingleMember(t);
       
        leadergroup.resetMembersOfGroups(t.GetComponent<SpriteRenderer>());


        if (this.gameObject.tag != "Player")
        {

            string[] behaviours = { "Pursue", "Leave", "AvoidWall", "Face" };
            float[] weightedBehavs = { 0.8f, 0.1f, 1, 1 };
            GameObject target = this.GetComponent<DecisionTreeCreator>().target;
            GameObject[] targets = { target, target, target, target, target };
            GameObject.FindGameObjectWithTag("GameController").GetComponent<BehaviourAdder>().addBehavioursOver(this.gameObject, targets, behaviours, weightedBehavs);
        }
        t.GetComponent<PersonalityBase>().formacionGrupo(t, leadergroup);
        if (this.gameObject.tag != "Player") base.DestroyTrees();

        DecisionTreeNode[] oldNodes = this.gameObject.GetComponents<DecisionTreeNode>();
        foreach (DecisionTreeNode n in oldNodes)
        {
            DestroyImmediate(n);
        }

    }
        


}

