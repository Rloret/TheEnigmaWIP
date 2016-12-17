using UnityEngine;
using System.Collections;

public class FloatDecision : Decision {

	public PersonalityBase targetPersonality;
    public float maxValue=10000;
    public float minvalue=0;
	public PersonalityBase characterPersonality;

    public enum FloatDecisionTypes { HEALTH, FEAR, AGGRESSIVENESS,CHARISMA, CONFIDENCEINOTHER  };
    public FloatDecisionTypes actualDecisionType;

    public override DecisionTreeNode GetBranch()
    {
       // targetPersonality = this.GetComponent<DecisionTreeCreator>().target.GetComponent<AIPersonality>();
        switch (actualDecisionType) {

            case FloatDecisionTypes.HEALTH:
                if (maxValue >= characterPersonality.health && characterPersonality.health >= minvalue)
                {
                    return nodeTrue;

                }
                break;

            case FloatDecisionTypes.FEAR:
                if (maxValue >= characterPersonality.fear && characterPersonality.fear >= minvalue)
                {
                    return nodeTrue;

                }
                break;

            case FloatDecisionTypes.AGGRESSIVENESS:
                if (maxValue >= characterPersonality.selfAssertion && characterPersonality.selfAssertion >= minvalue)
                {
                    return nodeTrue;

                }
                break;

            case FloatDecisionTypes.CONFIDENCEINOTHER:
                int value = characterPersonality.TrustInOthers[targetPersonality.GetMyOwnIndex()];

                if (maxValue >=value  && value >= minvalue)
                {
                    return nodeTrue;

                }
                break;

            case FloatDecisionTypes.CHARISMA:
                if (maxValue >= characterPersonality.charisma && characterPersonality.charisma >= minvalue)
                {
                    return nodeTrue;

                }
                break;

          

        }
        return nodeFalse;



    }
}
