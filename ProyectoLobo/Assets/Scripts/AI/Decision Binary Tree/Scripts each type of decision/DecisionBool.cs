using UnityEngine;
using System.Collections;

public class DecisionBool : Decision
{
    public bool valueDecision;
    //public bool valueTest;

    public AIPersonality personalityScript;

    public enum BoolDecisionEnum { ISMONSTER, INGROUP };
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

        }
       
        return nodeFalse;
    }
}
