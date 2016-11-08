using UnityEngine;
using System.Collections;

public class DecisionTreeISeeSomeoneWhatShouldIDo : DecisionTreeCreator
{


    public DistanceDecision root;

        private DecisionBool iAmMonster;
            private DecisionBool targetIsHuman;
              private DecisionBool targetInGroup;



        protected override void CreateTree()
    {
        root = createDistanceDecisionFloat(myTransform, targetTransform, 5);

        iAmMonster = createDecisionsBool(true, myPersonality, DecisionBool.BoolDecisionEnum.ISMONSTER);
        root.nodeTrue = iAmMonster;
       // root.nodeFalse...

       targetIsHuman= createDecisionsBool(false, targetPersonality, DecisionBool.BoolDecisionEnum.ISMONSTER);
        iAmMonster.nodeTrue = targetIsHuman;
       // iAmMonster.nodeFalse...
        targetInGroup = createDecisionsBool(true, targetPersonality, DecisionBool.BoolDecisionEnum.INGROUP);
        targetIsHuman.nodeTrue = targetInGroup;
        //targetIsHuman.nodeFalse...

        createLeaves(targetInGroup, addActionEvade(), addActionAttack());




        //  decisionNew = root;


    }



}
