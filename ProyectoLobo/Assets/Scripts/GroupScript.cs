using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroupScript : MonoBehaviour {

    public bool inGroup = false;
    public bool IAmTheLeader = false;
    public GameObject crown;

    public GameObject groupLeader;

    public List<GameObject> groupMembers;

    private int numberMembersGroup = 0;

    private float maxDistGroup = 500f;

    private Color originalColor;

    void Start()
    {
        groupMembers = new List<GameObject>();
        groupLeader = this.gameObject;
        originalColor = this.gameObject.GetComponent<SpriteRenderer>().color;

    }

    public Color getColor()
    {
        return originalColor;
    }
    public void setOriginalColor(Color newCol)
    {
        originalColor = newCol;
    }

    public void FollowTheLeaderAttack()
    {
        for (int i = 0; i < numberMembersGroup; i++) {
            groupMembers[i].AddComponent<ActionAttack>();
        }

    }

    public void updateGroups(GameObject elQueseUne, List<GameObject> ysugrupo)
    {
        List<GameObject> auxList = new List<GameObject>();
        auxList.AddRange(ysugrupo);
        foreach (var members in auxList)
        {
            addSingleMember(members);
            var currentGroup = members.GetComponent<GroupScript>();
            currentGroup.groupLeader = this.gameObject;
        }
            addSingleMember(elQueseUne);
            if (elQueseUne.tag != "Player")
            {
                elQueseUne.GetComponent<VisibilityConeCycleIA>().enabled = false;
            }

        if (groupLeader.GetComponent<PersonalityBase>().health < 30)
        {
            ExitGroup();
            this.GetComponent<VisibilityConeCycleIA>().enabled = true;
            string[] behaviours = { "Wander", "AvoidWall", "LookWhereYouAreGoing" };
            float[] weightedBehavs = { 0.7f, 1, 1 };
            GameObject[] targets = { this.gameObject, this.gameObject, this.gameObject };
            GameObject.FindGameObjectWithTag("GameController").GetComponent<BehaviourAdder>().addBehavioursOver(this.gameObject,targets,behaviours,weightedBehavs);
        }
    }

    public void resetMembersOfGroups(SpriteRenderer renderer)
    {
        List<GameObject> auxList = new List<GameObject>();
        auxList.AddRange(groupMembers);
        foreach (var member in auxList)
        {
            var currentGroup = member.GetComponent<GroupScript>();
            currentGroup.groupMembers.Clear();
            currentGroup.groupMembers.AddRange(copyGroup());
            currentGroup.groupMembers.Remove(member);
            currentGroup.addSingleMember(this.gameObject);
            member.GetComponent<SpriteRenderer>().color = renderer.color;
        }

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

    void Update()
    {
        if (this != null && groupLeader!=null)
        {
            if (Vector2.Distance(this.gameObject.transform.position, groupLeader.transform.position) > maxDistGroup)
            {
                ExitGroup();
            }
            if (IAmTheLeader && groupLeader == this.gameObject)
            {
                crown.SetActive(true);
            }
            else
            {
                crown.SetActive(false);
            }
        }
        else if(groupLeader == null)
        {
            this.GetComponent<VisibilityConeCycleIA>().enabled = true;
            this.makeLeader();
        }

    }

    public void ExitGroup() {
        List<GameObject> members = groupLeader.GetComponent<GroupScript>().groupMembers;
        foreach(var m in members)
        {
			
            m.GetComponent<GroupScript>().groupMembers.Remove(this.gameObject);

        }
        GroupScript leaderGroup = groupLeader.GetComponent<GroupScript>();
        leaderGroup.groupMembers.Remove(this.gameObject);
        if (leaderGroup.groupMembers.Count==0)
        {
            leaderGroup.groupLeader = leaderGroup.gameObject;
            leaderGroup.inGroup = false;
            leaderGroup.IAmTheLeader = false;
        }
        groupLeader = this.gameObject;
        inGroup = false;
        groupMembers.Clear();
        this.gameObject.GetComponent<SpriteRenderer>().color = originalColor;

        if (this.gameObject.tag != "Player")
        {
            foreach (var comp in this.GetComponents<AgentBehaviour>())
            {
                DestroyImmediate(comp);
            } 
            GetComponent<VisibilityConeCycleIA>().enabled = true;
        }


    }
}
