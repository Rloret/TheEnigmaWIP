using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionJoinGroup : Action
{
    public override void DoAction()
    {
        base.visibiCone.IDecided = false;
        Reaction.spawnReaction(ResponseController.responseEnum.GROUP, ResponseController.responseEnum.OK, this.gameObject);
        Debug.Log("me uno a tu grupo");

        string[] behaviours = { "Pursue","Leave", "AvoidWall", "Face" };
        float[] weightedBehavs = { 0.8f,0.1f, 1, 1 };
        GameObject.FindGameObjectWithTag("GameController").GetComponent<BehaviourAdder>().addBehavioursOver(this.gameObject, this.GetComponent<DecisionTreeCreator>().target, behaviours, weightedBehavs);


        Debug.Log("mytarget es " + this.GetComponent<DecisionTreeCreator>().target);
        GameObject t = this.GetComponent<DecisionTreeCreator>().target;
        GroupScript myGroup = this.GetComponent<GroupScript>();
        GroupScript leadergroup = t.GetComponent<GroupScript>();

        myGroup.groupLeader = t ;
        myGroup.inGroup = true;
        myGroup.IAmTheLeader = false;
        myGroup.groupMembers.Clear();
        myGroup.groupMembers.AddRange(leadergroup.copyGroup());
        myGroup.addSingleMember(t);
        leadergroup.updateGroups(this.gameObject);
        leadergroup.makeLeader();

        base.DestroyTrees();

    }
        


}

