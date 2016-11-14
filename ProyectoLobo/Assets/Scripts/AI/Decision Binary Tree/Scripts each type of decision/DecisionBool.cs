using UnityEngine;
using System.Collections;

public class DecisionBool : Decision
{
    public bool valueDecision;
    //public bool valueTest;

    public AIPersonality personalityScript;

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
                if (valueDecision == personalityScript.inGroup)
                {
                    return nodeTrue;

                }
                break;

            case BoolDecisionEnum.IAMGROUPLEADER:
                if (this.gameObject == personalityScript.groupLeader) //check if the group leader is me
                {
                    Debug.Log(personalityScript.gameObject.name);
                    return nodeTrue;

                }
                break;

        }
       
        return nodeFalse;
    }
}
