using UnityEngine;
using System.Collections;

public class DecisionTreeReactionAfterInteraction : DecisionTreeCreator {

    public DecisionActionsEnum root;

        private DecisionBool isMonster; //true Branch1
             private DecisionBool inGroup; // true Branch 2
                private FloatDecision healthBigger70; // true Branch 3
                                                           // false Branch 3 -> Action Attack 
             private FloatDecision fear; //false Branch 2
                 private FloatDecision healthLess40; //false Branch 3


        private DecisionActionsEnum givingMeObject; //false Branch1
            private ActionComparePriorityObjectAgainstPriorityTree comparePriorityTreeAction;

            private FloatDecision trustHimMore6;
                private FloatDecision trustHimLess3;
                     private FloatDecision agresive1;

                     private FloatDecision charismatic;
                     private FloatDecision agresive2;

                          private RandomFloatDecision randomDecision;




    protected override void CreateTree() { 
    // TRUE  branch 1
        root = createDecisionsEnum(ActionsEnum.Actions.ATTACK, myPersonality);

       // isMonster = createDecisionsBool(true, aiPersonality.isMonster);
        isMonster = createDecisionsBool(true, targetPersonality, DecisionBool.BoolDecisionEnum.ISMONSTER);

        givingMeObject = createDecisionsEnum(ActionsEnum.Actions.OFFER, myPersonality);


        root.nodeTrue = isMonster ;
        root.nodeFalse = givingMeObject ;

             //  Debug.Log("creando  branch 2");
           // inGroup = createDecisionsBool(true, aiPersonality.inGroup);
            inGroup = createDecisionsBool(true, myPersonality, DecisionBool.BoolDecisionEnum.INGROUP);
            fear = createDecisionsFloat(3, 10, /*aiPersonality.fear*/myPersonality, FloatDecision.FloatDecisionTypes.FEAR);
            isMonster.nodeTrue = inGroup;
            isMonster.nodeFalse = fear;


                //  Debug.Log("creando  branch 3");

                healthBigger70 = createDecisionsFloat(70, 1000, /*aiPersonality.health*/myPersonality, FloatDecision.FloatDecisionTypes.HEALTH);
                inGroup.nodeTrue = healthBigger70;
                inGroup.nodeFalse = addActionEvade();

                createLeaves(healthBigger70 as Decision, addActionAttack(), addActionEvade());


                healthLess40 = createDecisionsFloat(0, 40, /*aiPersonality.health*/myPersonality, FloatDecision.FloatDecisionTypes.HEALTH);
                fear.nodeTrue = addActionEvade();
                fear.nodeFalse = healthLess40;

               createLeaves(healthLess40, addActionEvade(), addActionAttack());


        // FALSE branch 1

        trustHimMore6 = createDecisionsFloat(6, 10,/* myTrustInOther*/myPersonality, FloatDecision.FloatDecisionTypes.CONFIDENCEINOTHER);

        givingMeObject.nodeTrue = addActionOffer(); //MOCK-ASINES SALTARINES
        givingMeObject.nodeFalse = trustHimMore6;

        // comparePriorityTreeAction = addActionComparePriorityTree();                    DUDAS SERIAS DE COMO HACER ESTO JI
        // comparePriorityTreeAction.nodeFalse = addActionOffer();
        // comparePriorityTreeAction.nodeTrue = comparePriorityTreeAction.nodeFalse;


        trustHimLess3 = createDecisionsFloat(0, 3, /*myTrustInOther*/myPersonality, FloatDecision.FloatDecisionTypes.CONFIDENCEINOTHER);
            trustHimMore6.nodeTrue = addActionJoin();
            trustHimMore6.nodeFalse = trustHimLess3;
       

        agresive1 = createDecisionsFloat(3, 10, /*aiPersonality.selfAssertion*/myPersonality, FloatDecision.FloatDecisionTypes.AGGRESSIVENESS);
        charismatic = createDecisionsFloat(3, 10,/* aiPersonality.charisma*/myPersonality, FloatDecision.FloatDecisionTypes.CHARISMA);
        trustHimLess3.nodeTrue = agresive1;
        trustHimLess3.nodeFalse = charismatic;
        createLeaves(agresive1, addActionAttack(), addActionNothing());

            agresive2= createDecisionsFloat(2.5f, 10, /*aiPersonality.selfAssertion*/myPersonality, FloatDecision.FloatDecisionTypes.AGGRESSIVENESS);
            charismatic.nodeTrue = addActionJoin();
            charismatic.nodeFalse = agresive2;

        //randomDecision = createDecisionsFloat(8, 10,/* Random.Range(1, 10)*/);
                randomDecision = createRandomDecisionFloat(8, 10, 1, 10);
                agresive2.nodeTrue = addActionAttack();
                agresive2.nodeFalse = randomDecision;

                createLeaves(randomDecision,  addActionJoin(),addActionNothing());


        decisionNew = root;

    }


    protected override DecisionTreeNode SonMakeDecision()
    {
        return root.MakeDecision();
    }

    

   

}
