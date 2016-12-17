using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroupScript : MonoBehaviour {

    public bool inGroup = false;
    public bool IAmTheLeader = false;

    public GameObject groupLeader;

    public List<GameObject> groupMembers;

    private int numberMembersGroup = 0;

    void Start()
    {
        groupMembers = new List<GameObject>();
        groupLeader = this.gameObject;

    }
    void OnDrawGizmos()
    {
        if (groupMembers.Count > 0)
        {
            Gizmos.color = Color.black;
            for (int i = 1; i < groupMembers.Count; i++)
            {
                Gizmos.DrawLine(groupMembers[i - 1].transform.position+Vector3.up*3, groupMembers[i].transform.position + Vector3.up * 3);
            }
        }
    }
    public void FollowTheLeaderAttack()
    {
        for (int i = 0; i < numberMembersGroup; i++) {
            groupMembers[i].AddComponent<ActionAttack>();
        }

    }

    public void updateGroups( GameObject component)
    {
        foreach (var members in groupMembers)
        {
            members.GetComponent<GroupScript>().addSingleMember(component);
        }
        addSingleMember(component);
    }
    public void leaveGroup(GameObject component)
    {
      /*  foreach (var members in groupMembers)
        {
            members.GetComponent<GroupScript>().removeSingleMember(component);
        }
        if (groupMembers.Count > 0)
        {
            groupMembers[0].GetComponent<GroupScript>().makeLeader();
        }
        removeSingleMember(component);
        groupLeader = null;
        inGroup =IAmTheLeader= false;
        groupMembers.Clear();*/

    }
    public void addSingleMember(GameObject component)
    {
        groupMembers.Add(component);
    }

    public void removeSingleMember(GameObject component)
    {
        groupMembers.Remove(component);
    }

    public bool checkIAInGroup(GameObject IA)
    {
        return groupMembers.Contains(IA);
    }

    public List<GameObject> copyGroup()
    {
        return groupMembers;
    }

    public void makeLeader()
    {
        if (!inGroup) inGroup = true;
        if (!IAmTheLeader)
        {
            IAmTheLeader = true;
            groupLeader = this.gameObject;
        }
        foreach (var members in groupMembers)
        {
            members.GetComponent<GroupScript>().groupLeader = this.gameObject;
            members.GetComponent<GroupScript>().IAmTheLeader = false;
        }
    }
}
