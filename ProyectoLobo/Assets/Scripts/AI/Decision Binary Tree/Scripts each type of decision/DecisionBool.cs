using UnityEngine;
using System.Collections;

public class DecisionBool : Decision
{
    public bool valueDecision;
    //public bool valueTest;

	public PersonalityBase personalityScript;
    private GroupScript groupScript;


    public enum BoolDecisionEnum { ISMONSTER, INGROUP, IAMGROUPLEADER };
    public BoolDecisionEnum actualDecisionenum;

    public override DecisionTreeNode GetBranch()
    {
        
        switch (actualDecisionenum)
        {
            case BoolDecisionEnum.ISMONSTER:
                if (valueDecision == personalityScript.isMonster)
                {
                    return nodeTrue;

                }
                break;

            case BoolDecisionEnum.INGROUP:
                groupScript = personalityScript.gameObject.GetComponent<GroupScript>();
                if (valueDecision == groupScript.inGroup)
                {
                    return nodeTrue;

                }
                break;

            case BoolDecisionEnum.IAMGROUPLEADER:
                groupScript = personalityScript.gameObject.GetComponent<GroupScript>();
               // Debug.Log("target es: " + personalityScript.gameObject.name+ "yo soy" + this.gameObject.name);

                if (groupScript.groupLeader == this.gameObject) //check if the group leader is me
                {
                    Debug.Log( "soy el lider del grupo proceedo por nodo true " +personalityScript.gameObject.name);
                    return nodeTrue;

                }
                break;

        }
       
        return nodeFalse;
    }
}
