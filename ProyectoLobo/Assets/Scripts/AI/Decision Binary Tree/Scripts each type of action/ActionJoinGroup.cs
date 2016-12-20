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



        if (t.tag =="Player" && myGroup.IAmTheLeader == false && myGroup.groupLeader != this.gameObject)
        {
            myGroup.ExitGroup();
            myGroup.groupLeader = t;
            myGroup.inGroup = true;
            myGroup.IAmTheLeader = false;
            leadergroup.updateGroups(this.gameObject, myGroup.groupMembers);
            leadergroup.makeLeader();
            myGroup.groupMembers = new List<GameObject>();
            myGroup.groupMembers.AddRange(leadergroup.groupMembers);
            myGroup.groupMembers.Remove(this.gameObject);
            myGroup.addSingleMember(t);
            this.gameObject.GetComponent<SpriteRenderer>().color = leadergroup.getColor();

        }
        else
        {
             myGroup.groupLeader = t;
             myGroup.inGroup = true;
             myGroup.IAmTheLeader = false;
             leadergroup.updateGroups(this.gameObject, myGroup.groupMembers);
             leadergroup.makeLeader();
             myGroup.addSingleMember(t);

             leadergroup.resetMembersOfGroups(t.GetComponent<SpriteRenderer>());

        }

        if (this.gameObject.tag != "Player")
        {

            string[] behaviours = { "Pursue", "Leave", "AvoidWall", "Face" };
            float[] weightedBehavs = { 0.8f, 0.1f, 1, 1 };
            GameObject target = this.GetComponent<DecisionTreeCreator>().target.GetComponent<GroupScript>().groupLeader;
            GameObject[] targets = { target, target, target, target };
            GameObject.FindGameObjectWithTag("GameController").GetComponent<BehaviourAdder>().addBehavioursOver(this.gameObject, targets, behaviours, weightedBehavs);
            base.DestroyTrees();
        }
        t.GetComponent<PersonalityBase>().formacionGrupo(t, leadergroup);


        DecisionTreeNode[] oldNodes = this.gameObject.GetComponents<DecisionTreeNode>();
        foreach (DecisionTreeNode n in oldNodes)
        {
            DestroyImmediate(n);
        }

    }
        


}

