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

    void Awake() { }
    public override void Start() {
        aiPersonality = this.gameObject.GetComponent<AIPersonality>();
        createTree();
    }

    private void createTree() {
       // Debug.Log("creando  branch 1");
        root = createDecisionsEnum(ActionsEnum.Actions.ATTACK);
       // Debug.Log("root es "+root) ;

        isMonster = createDecisionsBool(true, aiPersonality.isMonster);
        //Debug.Log("isMonster es " +isMonster);

        givingMeObject = createDecisionsEnum(ActionsEnum.Actions.OFFER);
        //  Debug.Log("givingmeObej es "+ givingMeObject);

        /////// createBranches(root , isMonster , givingMeObject );
        root.nodeTrue = isMonster ;
        root.nodeFalse = givingMeObject ;


        if (isMonster == null) Debug.Log("ismonster es nmulo");
        else Debug.Log(" es " + isMonster);

       if(root.nodeTrue ==null) Debug.Log("nodeTrue " + root.nodeTrue); ;



        //  Debug.Log("creando  branch 2");
        inGroup = createDecisionsBool(true, aiPersonality.inGroup);
          //  Debug.Log("inGroup es " + inGroup);

            fear = createDecisionsFloat(0, 40, aiPersonality.health);
          //  Debug.Log("fear es " + fear);*/

        /*
            createBranches(isMonster as Decision, inGroup as Decision, fear as Decision);

                healthBigger70 = createDecisionsFloat(70, 1000, aiPersonality.health);
                createBranches(inGroup as Decision, healthBigger70 as Decision, addActionEvade() as Action as DecisionTreeNode as Decision);

                     createLeaves(inGroup as Decision, addActionAttack(), addActionEvade());*/

        /* createDecisionsEnum(out root, ActionsEnum.Actions.ATTACK);
         createDecisionsBool(out isMonster, true, aiPersonality.isMonster);
         createDecisionsEnum(out givingMeObject, ActionsEnum.Actions.OFFER);
         createBranches(root, isMonster, givingMeObject);*/


        createLeaves(isMonster , addActionAttack(), addActionOffer());
    }

    public override DecisionTreeNode MakeDecision()
    {
        Debug.Log("llamo a root.makeDecision()");
        return root.MakeDecision();
    }

    private ActionsEnum.Actions getEnumState() {return aiPersonality.GetInteraction(); }

    private ActionAttack addActionAttack() { return gameObject.AddComponent<ActionAttack>(); }
    private ActionOffer addActionOffer() { return gameObject.AddComponent<ActionOffer>(); }
    private ActionEvade addActionEvade() { return gameObject.AddComponent<ActionEvade>(); }



    void Update() {
        if (!DecisionCompleted)
        {
            Debug.Log("Entro en update");
            if (actionNew != null)
            {
                actionNew.activated = false;
                actionOld = actionNew;
            }

            Debug.Log("root antes es " + root);

            DecisionTreeNode actualDecision = root.MakeDecision() ;
            Debug.Log("decsision actual  " + actualDecision);

            actionNew = actualDecision as Decision as DecisionTreeNode as Action;

            if (actionNew == null)
                Debug.Log("actionNew despues es null");
            else Debug.Log("actionNew despues es "+ actionNew);


            if (actionNew == null)
            {
                actionNew = actionOld;
            }
            actionNew.activated = true;
            Debug.Log("fin update");
        }

    }

    private void createLeaves(Decision d,Action nt, Action nf) {
        d.nodeTrue = nt;
        d.nodeFalse = nf;
    }

    private void createBranches(Decision d, DecisionTreeNode nt, DecisionTreeNode nf)
    {
        d.nodeTrue = nt as Action;
        d.nodeFalse =nf as Action;
    }


    private DecisionActionsEnum createDecisionsEnum( ActionsEnum.Actions vDecision/*, ActionsEnum.Actions vTest*/) {
           DecisionTreeNode d;
           d = gameObject.AddComponent<DecisionActionsEnum>();
           DecisionActionsEnum dae = d as DecisionActionsEnum;
           dae.valueDecision = vDecision;
           dae.valueTest = getEnumState();

        return dae;
    }


    private DecisionBool createDecisionsBool( bool vDecision, bool vTest)
    {
        DecisionTreeNode d;
        d = gameObject.AddComponent<DecisionBool>();
        DecisionBool db = d as DecisionBool;
        db.valueDecision = vDecision;
        db.valueTest = vTest;

        return db;

    }



//   /* private void createDecisionsEnum(out DecisionTreeNode d, ActionsEnum.Actions vDecision/*, ActionsEnum.Actions vTest*/) {
 //       d = gameObject.AddComponent<DecisionActionsEnum>(); 
 //       d.valueDecision = vDecision;
        // d.valueTest = vTest;
 //       d.valueTest = getEnumState();

        // Debug.Log("d =" + d + "valuedec " + d.valueDecision + " test " + d.valueTest);
    
/*
    private void createDecisionsBool(out DecisionTreeNode d, bool vDecision, bool vTest)
    {
        d = gameObject.AddComponent<DecisionBool>();
        d.valueDecision = vDecision;
        d.valueTest = vTest;

    }*/


    private FloatDecision createDecisionsFloat( float min, float max,float testValue)
    {
        FloatDecision d = gameObject.AddComponent<FloatDecision>() as FloatDecision;
        d.minvalue = min;
        d.maxValue = max;
        d.testValue = testValue;

        return d;
    }
    

}
