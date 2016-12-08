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
    public void addSingleMember(GameObject component)
    {
        groupMembers.Add(component);
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
    }
}
