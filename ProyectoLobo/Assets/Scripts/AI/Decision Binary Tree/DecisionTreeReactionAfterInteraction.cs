using UnityEngine;
using System.Collections;

public class DecisionTreeReactionAfterInteraction : DecisionTreeNode {

    // public DecisionTreeNode root;

    public static bool DecisionCompleted=false;

    public DecisionActionsEnum root;

        private DecisionBool isMonster; //true Branch1
             private DecisionBool inGroup; // true Branch 2
                private FloatDecision healthBigger70; // true Branch 3
                                                           // false Branch 3 -> Action Attack 
             private FloatDecision fear; //false Branch 2
        private DecisionActionsEnum givingMeObject; //false Branch1

    private AIPersonality aiPersonality;
    

    private Action actionNew;
    private Action actionOld;

    private Decision decisionNew;
    private Decision decisionOld;

    void Awake() { }
    public override void Start() {
        aiPersonality = this.gameObject.GetComponent<AIPersonality>();
        createTree();
    }

    private void createTree() {
       // Debug.Log("creando  branch 1");
        root = createDecisionsEnum(ActionsEnum.Actions.ATTACK);

        isMonster = createDecisionsBool(true, aiPersonality.isMonster);

        givingMeObject = createDecisionsEnum(ActionsEnum.Actions.OFFER);


        root.nodeTrue = isMonster ;
        root.nodeFalse = givingMeObject ;

             //  Debug.Log("creando  branch 2");
            inGroup = createDecisionsBool(true, aiPersonality.inGroup);
            fear = createDecisionsFloat(0, 40, aiPersonality.health);
            isMonster.nodeTrue = inGroup;
            isMonster.nodeFalse = fear;


                //  Debug.Log("creando  branch 3");

                healthBigger70 = createDecisionsFloat(70, 1000, aiPersonality.health);
                inGroup.nodeTrue = healthBigger70;
                inGroup.nodeFalse = addActionEvade();

                createLeaves(healthBigger70 as Decision, addActionAttack(), addActionEvade());

        /* createDecisionsEnum(out root, ActionsEnum.Actions.ATTACK);
         createDecisionsBool(out isMonster, true, aiPersonality.isMonster);
         createDecisionsEnum(out givingMeObject, ActionsEnum.Actions.OFFER);
         createBranches(root, isMonster, givingMeObject);*/


       // createLeaves(inGroup , addActionAttack(), addActionOffer());

        /*actionNew =root.toAction();
        actionOld = actionNew;
        Debug.Log("actionNew antes es " + actionNew);*/

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
    //  private Decision addActionDecision() { return gameObject.AddComponent<Decision>(); }



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
