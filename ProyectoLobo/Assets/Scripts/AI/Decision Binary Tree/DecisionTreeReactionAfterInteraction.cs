using UnityEngine;
using System.Collections;

public class DecisionTreeReactionAfterInteraction : DecisionTreeNode {

    // public DecisionTreeNode root;

    public static bool DecisionCompleted=false;

    private int indexCharacterInteractedWithMe;
    private int myTrustInOther;

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

                          private FloatDecision randomDecision;


    private AIPersonality aiPersonality;
    

    private Action actionNew;
    private Action actionOld;

    private Decision decisionNew;
    private Decision decisionOld;

    void Awake() { }
    public override void Start() {
        aiPersonality = this.gameObject.GetComponent<AIPersonality>();

        indexCharacterInteractedWithMe = aiPersonality.GetIndexOther();
        myTrustInOther = aiPersonality.TrustInOthers[indexCharacterInteractedWithMe];

        Debug.Log("confio en el " + indexCharacterInteractedWithMe + " tanto " + myTrustInOther);

        createTree();
    }

    private void createTree() {
       // TRUE  branch 1
        root = createDecisionsEnum(ActionsEnum.Actions.ATTACK);

        isMonster = createDecisionsBool(true, aiPersonality.isMonster);

        givingMeObject = createDecisionsEnum(ActionsEnum.Actions.OFFER);


        root.nodeTrue = isMonster ;
        root.nodeFalse = givingMeObject ;

             //  Debug.Log("creando  branch 2");
            inGroup = createDecisionsBool(true, aiPersonality.inGroup);
            fear = createDecisionsFloat(3, 10, aiPersonality.fear);
            isMonster.nodeTrue = inGroup;
            isMonster.nodeFalse = fear;


                //  Debug.Log("creando  branch 3");

                healthBigger70 = createDecisionsFloat(70, 1000, aiPersonality.health);
                inGroup.nodeTrue = healthBigger70;
                inGroup.nodeFalse = addActionEvade();

                createLeaves(healthBigger70 as Decision, addActionAttack(), addActionEvade());


                healthLess40 = createDecisionsFloat(0, 40, aiPersonality.health);
                fear.nodeTrue = addActionEvade();
                fear.nodeFalse = healthLess40;

               createLeaves(healthLess40, addActionEvade(), addActionAttack());


        // FALSE branch 1

        trustHimMore6 = createDecisionsFloat(6, 10, myTrustInOther);

        givingMeObject.nodeTrue = addActionOffer(); //MOCK-ASINES SALTARINES
        givingMeObject.nodeFalse = trustHimMore6;

        // comparePriorityTreeAction = addActionComparePriorityTree();                    DUDAS SERIAS DE COMO HACER ESTO JI
        // comparePriorityTreeAction.nodeFalse = addActionOffer();
        // comparePriorityTreeAction.nodeTrue = comparePriorityTreeAction.nodeFalse;


        trustHimLess3 = createDecisionsFloat(0, 3, myTrustInOther);
            trustHimMore6.nodeTrue = addActionJoin();
            trustHimMore6.nodeFalse = trustHimLess3;
       

        agresive1 = createDecisionsFloat(3, 10, aiPersonality.selfAssertion);
        charismatic = createDecisionsFloat(3, 10, aiPersonality.charisma);
        trustHimLess3.nodeTrue = agresive1;
        trustHimLess3.nodeFalse = charismatic;
        createLeaves(agresive1, addActionAttack(), addActionNothing());

            agresive2= createDecisionsFloat(2.5f, 10, aiPersonality.selfAssertion);
            charismatic.nodeTrue = addActionJoin();
            charismatic.nodeFalse = agresive2;

                randomDecision = createDecisionsFloat(8, 10, Random.Range(1, 10));
                agresive2.nodeTrue = addActionAttack();
                agresive2.nodeFalse = randomDecision;

                createLeaves(randomDecision,  addActionJoin(),addActionNothing());







        decisionNew = root;


    }

    public override DecisionTreeNode MakeDecision()
    {
        return root.MakeDecision();
    }

    private ActionsEnum.Actions getEnumState() {return aiPersonality.GetInteraction(); }

    private ActionAttack addActionAttack() { return gameObject.AddComponent<ActionAttack>(); }
    private ActionOffer addActionOffer() { return gameObject.AddComponent<ActionOffer>(); }
    private ActionEvade addActionEvade() { return gameObject.AddComponent<ActionEvade>(); }
    private ActionJoinGroup addActionJoin() { return gameObject.AddComponent<ActionJoinGroup>();  }
    private ActionNothing addActionNothing() { return gameObject.AddComponent<ActionNothing>(); }

    //private ActionComparePriorityObjectAgainstPriorityTree addActionComparePriorityTree() { return gameObject.AddComponent<ActionComparePriorityObjectAgainstPriorityTree>(); }



    /* void Update() {
         if (!DecisionCompleted)
         {
             Debug.Log("Entro en update");
           //  if (actionNew != null)
            // {
                 actionNew.activated = false;
                 actionOld = actionNew;
            // }

             Debug.Log("root antes es " + root);

             /*Decision actualDecision = root.MakeDecision() as Decision;
             Debug.Log("decsision actual  " + actualDecision);

             actionNew = actualDecision.toAction();*/
    /*
                actionNew = actionNew.MakeDecision() as Action;

                if (actionNew == null)
                    Debug.Log("actionNew despues es null");
                else Debug.Log("actionNew despues es "+ actionNew);


                if (actionNew == null)
                {
                    actionNew = actionOld;
                    DecisionCompleted = true;
                }
                actionNew.activated = true;
                Debug.Log("fin update");
            }
            Debug.Log("decision completada");

        }*/

    void Update()
    {
        if (!DecisionCompleted)
        {
            decisionNew = decisionNew.MakeDecision() as Decision;

          /* Debug.Log("decisionNew es " + decisionNew);
            if (decisionNew != null) Debug.Log("ramas " + decisionNew.nodeTrue + decisionNew.nodeFalse);*/

            if (decisionNew == null) {

            actionNew = decisionOld.MakeDecision() as Action;
           // Debug.Log("action es " + actionNew);

                actionNew.DoAction();

                DecisionCompleted = true;
            }

            if (decisionNew != null)
            {
                decisionOld = decisionNew;
            }
        }

    }


    private void createLeaves(Decision d,Action nt, Action nf) {
        d.nodeTrue = nt;
        d.nodeFalse = nf;
    }

  /*  private void createBranches(Decision d, DecisionTreeNode nt, DecisionTreeNode nf)
    {
        d.nodeTrue = nt as Action;
        d.nodeFalse =nf as Action;
    }
    */

    private DecisionActionsEnum createDecisionsEnum( ActionsEnum.Actions vDecision/*, ActionsEnum.Actions vTest*/) {
           DecisionActionsEnum d = gameObject.AddComponent<DecisionActionsEnum>();
           d.valueDecision = vDecision;
           d.valueTest = getEnumState();

        return d;
    }


    private DecisionBool createDecisionsBool( bool vDecision, bool vTest)
    {
        DecisionBool d;
        d = gameObject.AddComponent<DecisionBool>();
        d.valueDecision = vDecision;
        d.valueTest = vTest;

        return d;

    }

    private FloatDecision createDecisionsFloat( float min, float max,float testValue)
    {
        FloatDecision d = gameObject.AddComponent<FloatDecision>() as FloatDecision;
        d.minvalue = min;
        d.maxValue = max;
        d.testValue = testValue;

        return d;
    }
    

}
