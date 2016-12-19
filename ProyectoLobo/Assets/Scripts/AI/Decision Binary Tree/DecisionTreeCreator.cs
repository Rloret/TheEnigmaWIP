using UnityEngine;
using System.Collections;

public class DecisionTreeCreator : DecisionTreeNode
{
    [HideInInspector]     public GameObject target;
    [HideInInspector]     public bool TreeCompletelyCreated = false;

    protected Action actionNew;
    protected Action actionOld;

    protected Decision decisionNew;
    protected Decision decisionOld;

	protected PersonalityBase myPersonality;
	protected PersonalityBase targetPersonality;


    protected int indexCharacterInteractedWithMe;
    protected int myTrustInOther;

    protected bool DecisionCompleted = true;


    public override void Start()
    {
     //   Debug.Log("start dec tree creator");

        //target = this.gameObject; //just to create the decisiontree

        myPersonality = this.gameObject.GetComponent<AIPersonality>();
		if (target.tag == "Player") {
			targetPersonality = target.gameObject.GetComponent<PlayerPersonality>();

		} else {
			targetPersonality = target.gameObject.GetComponent<AIPersonality>();

		}

        indexCharacterInteractedWithMe = targetPersonality.GetMyOwnIndex();
        myTrustInOther = myPersonality.TrustInOthers[indexCharacterInteractedWithMe];



        CreateTree();
    }


    protected virtual void CreateTree() { Debug.Log("crear arbol del padre, ha habido un error"); }


    public override DecisionTreeNode MakeDecision()
    {
        return SonMakeDecision();
    }


    protected virtual DecisionTreeNode SonMakeDecision() {
        return null;
    }


    protected ActionAttack addActionAttack() { return gameObject.AddComponent<ActionAttack>(); }
    protected ActionOffer addActionOffer() { return gameObject.AddComponent<ActionOffer>(); }
    protected ActionEvade addActionEvade() { return gameObject.AddComponent<ActionEvade>(); }
    protected ActionJoinGroup addActionJoin() { return gameObject.AddComponent<ActionJoinGroup>(); }
    protected ActionNothing addActionNothing() { return gameObject.AddComponent<ActionNothing>(); }
    protected ActionOfferOtherJoinMyGroup addActionOfferJoinGroup() { return gameObject.AddComponent<ActionOfferOtherJoinMyGroup>(); }
    protected ActionAcceptObjectOffered addActionAcceptObjectOffered() { return gameObject.AddComponent<ActionAcceptObjectOffered>(); }

    //private ActionComparePriorityObjectAgainstPriorityTree addActionComparePriorityTree() { return gameObject.AddComponent<ActionComparePriorityObjectAgainstPriorityTree>(); }


    protected void createLeaves(Decision d, Action nt, Action nf)
    {
        d.nodeTrue = nt;
        d.nodeFalse = nf;
    }

    protected DecisionActionsEnum createDecisionsEnum(ActionsEnum.Actions vDecision/*, ActionsEnum.Actions vTest*/, PersonalityBase pers)
    {
        DecisionActionsEnum d = gameObject.AddComponent<DecisionActionsEnum>();
        d.valueDecision = vDecision;
        d.personality = pers;

        return d;
    }


	protected DecisionBool createDecisionsBool(bool vDecision,/* bool vTest*/ PersonalityBase personality, DecisionBool.BoolDecisionEnum boolType)
    {
        DecisionBool d;
        d = gameObject.AddComponent<DecisionBool>();
        d.valueDecision = vDecision;
        // d.valueTest = vTest;
        d.personalityScript = personality;
        d.actualDecisionenum = boolType;

        return d;

    }

	protected FloatDecision createDecisionsFloat(float min, float max/*,float testValue*/,PersonalityBase personality, FloatDecision.FloatDecisionTypes type)
    {
        FloatDecision d = gameObject.AddComponent<FloatDecision>() as FloatDecision;
        d.minvalue = min;
        d.maxValue = max;
        // d.testValue = testValue;
        d.characterPersonality = personality;
        d.actualDecisionType = type;

        if (type == FloatDecision.FloatDecisionTypes.CONFIDENCEINOTHER) d.targetPersonality = targetPersonality;

        return d;
    }

    protected RandomFloatDecision createRandomDecisionFloat(float min, float max, float minrandom, float maxrandom)
    {
        RandomFloatDecision d = gameObject.AddComponent<RandomFloatDecision>() as RandomFloatDecision;
        d.minvalue = min;
        d.maxValue = max;
        d.randomRangeMin = minrandom;
        d.randomRangeMax = maxrandom;

        return d;
    }

    protected DistanceDecision createDistanceDecisionFloat(Transform myTransf, Transform otherTransf, int mindist)
    {
        DistanceDecision d = gameObject.AddComponent<DistanceDecision>() as DistanceDecision;
        d.MySelfTransform = myTransf;
        d.TargetTransform = otherTransf;
        d.mindistance = mindist;

        return d;
    }

	protected ObjectDecision createObjectDecision(ObjectHandler.ObjectType objecttest, PersonalityBase pers) {
        ObjectDecision d = gameObject.AddComponent<ObjectDecision>() as ObjectDecision;
        d.myPersonality = pers;
        d.objectWanted = objecttest;

        return d;

    }

	protected PriorityObjectDecision createPriorityObjectDecision(PersonalityBase myPers,PersonalityBase yourPers)
    {
        PriorityObjectDecision d = gameObject.AddComponent<PriorityObjectDecision>() as PriorityObjectDecision;
        d.characterPersonality = myPers;
        d.targetPersonality = yourPers;

        return d;

    }

    void Update()
    {
        if (!DecisionCompleted)
        {
//            Debug.Log("Entro en update");

            decisionNew = decisionNew.MakeDecision() as Decision;

       //     Debug.Log("decisionNew es " + decisionNew);
         //   if (decisionNew != null) Debug.Log("ramas " + decisionNew.nodeTrue + decisionNew.nodeFalse);

            if (decisionNew == null)
            {

                actionNew = decisionOld.MakeDecision() as Action;
             //    Debug.Log("action es " + actionNew);

                actionNew.DoAction();

                DecisionCompleted = true;

                CommunicateAction(actionNew);
            }

            if (decisionNew != null)
            {
                decisionOld = decisionNew;
            }
        }

    }

    public virtual void CommunicateAction(Action actionNew) {
    } 

    public virtual void StartTheDecision()
    {

    }


}
