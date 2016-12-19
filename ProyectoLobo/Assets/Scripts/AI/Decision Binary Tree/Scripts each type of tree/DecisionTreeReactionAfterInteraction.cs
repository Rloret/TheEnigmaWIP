using UnityEngine;
using System.Collections;

public class DecisionTreeReactionAfterInteraction : DecisionTreeCreator {

    //   public AIPersonality targetpers; TESTING


    [HideInInspector] public DecisionActionsEnum root;
        private DecisionBool iAmMonster;
             private DecisionBool heIsInGroup;
                private DecisionBool isMonster; //true Branch1
                     private DecisionBool inGroup; // true Branch 2
                        private FloatDecision healthBigger70; // true Branch 3
                                                                   // false Branch 3 -> Action Attack 
                     private FloatDecision fear; //false Branch 2
                         private FloatDecision healthLess40; //false Branch 3


        private DecisionActionsEnum givingMeObject; //false Branch1
            private PriorityObjectDecision comparePriorityTreeAction;

            private FloatDecision trustHimMore6;
                private FloatDecision trustHimLess3;
                     private FloatDecision agresive1;

                     private FloatDecision charismatic;
                     private FloatDecision agresive2;

                          private RandomFloatDecision randomDecision;

    private DecisionActionsEnum Join;



    private bool treeCreated = false;

    protected override void CreateTree() {

		if (target.tag == "Player") {
			targetPersonality = this.GetComponent<DecisionTreeCreator>().target.GetComponent<PlayerPersonality>();

		} else {
			targetPersonality = this.GetComponent<DecisionTreeCreator>().target.GetComponent<AIPersonality>();

		}     

		root = createDecisionsEnum(ActionsEnum.Actions.ATTACK, myPersonality);
        iAmMonster= createDecisionsBool(true, myPersonality, DecisionBool.BoolDecisionEnum.ISMONSTER);
        heIsInGroup= createDecisionsBool(true, targetPersonality, DecisionBool.BoolDecisionEnum.INGROUP);
        isMonster = createDecisionsBool(true, targetPersonality, DecisionBool.BoolDecisionEnum.ISMONSTER);
        givingMeObject = createDecisionsEnum(ActionsEnum.Actions.OFFER, myPersonality);
        inGroup = createDecisionsBool(true, myPersonality, DecisionBool.BoolDecisionEnum.INGROUP);
        fear = createDecisionsFloat(3, 10, /*aiPersonality.fear*/myPersonality, FloatDecision.FloatDecisionTypes.FEAR);
        healthBigger70 = createDecisionsFloat(70, 1000, /*aiPersonality.health*/myPersonality, FloatDecision.FloatDecisionTypes.HEALTH);
        healthLess40 = createDecisionsFloat(0, 40, /*aiPersonality.health*/myPersonality, FloatDecision.FloatDecisionTypes.HEALTH);
        trustHimMore6 = createDecisionsFloat(6, 10,/* myTrustInOther*/myPersonality, FloatDecision.FloatDecisionTypes.CONFIDENCEINOTHER);
        trustHimLess3 = createDecisionsFloat(0, 3, /*myTrustInOther*/myPersonality, FloatDecision.FloatDecisionTypes.CONFIDENCEINOTHER);
        agresive1 = createDecisionsFloat(3, 10, /*aiPersonality.selfAssertion*/myPersonality, FloatDecision.FloatDecisionTypes.AGGRESSIVENESS);
        charismatic = createDecisionsFloat(3, 10,/* aiPersonality.charisma*/myPersonality, FloatDecision.FloatDecisionTypes.CHARISMA);
        agresive2 = createDecisionsFloat(2.5f, 10, /*aiPersonality.selfAssertion*/myPersonality, FloatDecision.FloatDecisionTypes.AGGRESSIVENESS);
        randomDecision = createRandomDecisionFloat(8, 10, 1, 10);
        Join = createDecisionsEnum(ActionsEnum.Actions.JOIN, myPersonality);


        root.nodeTrue = iAmMonster;
        root.nodeFalse = givingMeObject ;

            iAmMonster.nodeTrue = heIsInGroup;
            iAmMonster.nodeFalse = isMonster;

                     createLeaves(heIsInGroup, addActionEvade(), addActionAttack());
                isMonster.nodeTrue = inGroup;
                isMonster.nodeFalse = fear;

                inGroup.nodeTrue = healthBigger70;
                inGroup.nodeFalse = addActionEvade();

                createLeaves(healthBigger70 as Decision, addActionAttack(), addActionEvade());

                fear.nodeTrue = addActionEvade();
                fear.nodeFalse = healthLess40;

               createLeaves(healthLess40, addActionEvade(), addActionAttack());

        comparePriorityTreeAction = createPriorityObjectDecision(myPersonality, targetPersonality);

		givingMeObject.nodeTrue = comparePriorityTreeAction;
        givingMeObject.nodeFalse = Join;

        Join.nodeTrue = trustHimMore6;
        Join.nodeFalse = addActionNothing();

         comparePriorityTreeAction.nodeFalse = addActionNothing();
        comparePriorityTreeAction.nodeTrue = addActionAcceptObjectOffered();

        trustHimMore6.nodeTrue = addActionJoin();
            trustHimMore6.nodeFalse = trustHimLess3;
       

        
        trustHimLess3.nodeTrue = agresive1;
        trustHimLess3.nodeFalse = charismatic;
        createLeaves(agresive1, addActionAttack(), addActionNothing());

            charismatic.nodeTrue = addActionJoin();
            charismatic.nodeFalse = agresive2;

                agresive2.nodeTrue = addActionAttack();
                agresive2.nodeFalse = randomDecision;

                createLeaves(randomDecision,  addActionJoin(),addActionNothing());
        DecisionCompleted = true;
        treeCreated = true;


		//Debug.Log ("fin de creando arbol reaction");
		StartTheDecision ();
    }


    protected override DecisionTreeNode SonMakeDecision()
    {
        return root.MakeDecision();
    }

    

    public override void StartTheDecision()
    {
       
		//Debug.Log(this.gameObject.name+ " Reaction Tree: empiezxo a decidir. mi accion recibida es "+ this.GetComponent<AIPersonality>().interactionFromOtherCharacter);

        decisionNew = root;

        base.DecisionCompleted = false;


    }

}
