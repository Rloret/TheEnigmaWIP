using UnityEngine;
using System.Collections;

public class GroupScript : MonoBehaviour {

    public bool inGroup = false;
    public bool IAmTheLeader = false;

    public GameObject groupLeader;

    public GameObject[] groupMembers;

    private int numberMembersGroup = 0;

    public void FollowTheLeaderAttack()
    {
        for (int i = 0; i < numberMembersGroup; i++) {
            groupMembers[i].AddComponent<ActionAttack>();
        }

    }

}
