using UnityEngine;
using System.Collections;

public class DecisionTreeISeeSomeoneWhatShouldIDo : DecisionTreeCreator
{
    DecisionTreeReactionAfterInteraction reaction;

    [HideInInspector] public DistanceDecision root;

         private DecisionBool iAmMonster;
             private DecisionBool targetIsHuman;
               private DecisionBool targetInGroup;

                 private DecisionBool isHeMonster;
                    private DecisionBool iamInGroup;
                        private FloatDecision healthBigger70;
        private FloatDecision TrustHimMore7;
            private DecisionBool isInMyGroup;
                private FloatDecision hisHealthLess40;
                    private ObjectDecision iHaveBotiquin;
                        private ObjectDecision iHaveMono;
                            private ObjectDecision iHaveShield;
                    private FloatDecision TrustHimMore8;
                        private ObjectDecision MyObjectIsNothing1;
                        private FloatDecision CharismBigger3;
                            private ObjectDecision MyObjectIsNothing2;
                                private RandomFloatDecision random1;
            private FloatDecision TrustHimmore5;
                private FloatDecision hisHealthLess45;
                    private FloatDecision Agressive;
                         private RandomFloatDecision random2;
                         private ObjectDecision iHaveBotiquin2;
                            private FloatDecision CharismaBigger3_2;
                                private ObjectDecision iHaveMono2;
                                    private ObjectDecision iHaveShield2;
                    private FloatDecision CharismBigger3_3;
                        private FloatDecision Fear;
                            private RandomFloatDecision random3;
                            private RandomFloatDecision random4;

                private FloatDecision TrustHimLess3;
                    private FloatDecision Agressive2;
                        private FloatDecision CharismaBigger3_4;
                            private ObjectDecision iHaveAxe;
                              private FloatDecision myHealthBigger80;
                     private FloatDecision Agressive3;
                        private FloatDecision hisHealthLess45_2;
                              private RandomFloatDecision random5;
                        private FloatDecision CharismaBigger3_5;
                            private RandomFloatDecision random6;

    
                        
    protected override void CreateTree()
    {

        //  base.targetPersonality = targetpers; //TESTING
		//Debug.Log("creando arbol what to do ");
		if (target.tag == "Player") {
			targetPersonality = this.GetComponent<DecisionTreeCreator>().target.GetComponent<PlayerPersonality>();

		} else {
			targetPersonality = this.GetComponent<DecisionTreeCreator>().target.GetComponent<AIPersonality>();

		}
        //Esto puede ser necesario en algun momento, pensamos que como se modifica desde fuera no es necesario
        //target = targetPersonality.gameObject;
        root = createDistanceDecisionFloat(this.gameObject.transform,target.transform, 60);

        iAmMonster = createDecisionsBool(true, myPersonality, DecisionBool.BoolDecisionEnum.ISMONSTER);
        targetIsHuman = createDecisionsBool(false, targetPersonality, DecisionBool.BoolDecisionEnum.ISMONSTER);
        isHeMonster = createDecisionsBool(true, targetPersonality, DecisionBool.BoolDecisionEnum.ISMONSTER);
        targetInGroup = createDecisionsBool(true, targetPersonality, DecisionBool.BoolDecisionEnum.INGROUP);
        iamInGroup = createDecisionsBool(true, myPersonality, DecisionBool.BoolDecisionEnum.INGROUP);
        healthBigger70 = createDecisionsFloat(70, 100, myPersonality, FloatDecision.FloatDecisionTypes.HEALTH);
        TrustHimMore7 = createDecisionsFloat(7, 10, myPersonality, FloatDecision.FloatDecisionTypes.CONFIDENCEINOTHER);
        isInMyGroup = createDecisionsBool(true, targetPersonality, DecisionBool.BoolDecisionEnum.IAMGROUPLEADER);
        hisHealthLess40 = createDecisionsFloat(0, 40, targetPersonality, FloatDecision.FloatDecisionTypes.HEALTH);
        iHaveBotiquin = createObjectDecision(ObjectHandler.ObjectType.MEDICALAID, myPersonality);
        iHaveMono = createObjectDecision(ObjectHandler.ObjectType.JUMPSUIT, myPersonality);
        iHaveShield = createObjectDecision(ObjectHandler.ObjectType.SHIELD, myPersonality);
        TrustHimMore8 = createDecisionsFloat(8, 10, myPersonality, FloatDecision.FloatDecisionTypes.CONFIDENCEINOTHER);
        MyObjectIsNothing1 = createObjectDecision(ObjectHandler.ObjectType.NONE, myPersonality);
        CharismBigger3 = createDecisionsFloat(3, 10, myPersonality, FloatDecision.FloatDecisionTypes.CHARISMA);
        MyObjectIsNothing2 = createObjectDecision(ObjectHandler.ObjectType.NONE, myPersonality);
        random1 = createRandomDecisionFloat(1, 3, 1, 2);
        TrustHimmore5 = createDecisionsFloat(5, 10, myPersonality, FloatDecision.FloatDecisionTypes.CONFIDENCEINOTHER);
        hisHealthLess45= createDecisionsFloat(0, 45,targetPersonality, FloatDecision.FloatDecisionTypes.HEALTH);
        Agressive = createDecisionsFloat(3, 10, myPersonality, FloatDecision.FloatDecisionTypes.AGGRESSIVENESS);
        random2 = random1 = createRandomDecisionFloat(1, 3, 1, 2);
        iHaveBotiquin2= createObjectDecision(ObjectHandler.ObjectType.MEDICALAID, myPersonality);
        CharismaBigger3_2= createDecisionsFloat(3, 10, myPersonality, FloatDecision.FloatDecisionTypes.CHARISMA);
        iHaveBotiquin2 = createObjectDecision(ObjectHandler.ObjectType.MEDICALAID, myPersonality);
        iHaveMono2 = createObjectDecision(ObjectHandler.ObjectType.JUMPSUIT, myPersonality);
        iHaveShield2 = createObjectDecision(ObjectHandler.ObjectType.SHIELD, myPersonality);
        CharismBigger3_3 = createDecisionsFloat(3, 10, myPersonality, FloatDecision.FloatDecisionTypes.CHARISMA);
        Fear = createDecisionsFloat(3, 10, myPersonality, FloatDecision.FloatDecisionTypes.FEAR);
        random3 = createRandomDecisionFloat(1, 10, 8, 10);
        random4 = createRandomDecisionFloat(1, 5, 1, 2);
        TrustHimLess3= createDecisionsFloat(0, 3, myPersonality, FloatDecision.FloatDecisionTypes.CONFIDENCEINOTHER); ;
        Agressive2= createDecisionsFloat(3, 10, myPersonality, FloatDecision.FloatDecisionTypes.AGGRESSIVENESS);
        CharismaBigger3_4 = createDecisionsFloat(3, 10, myPersonality, FloatDecision.FloatDecisionTypes.CHARISMA);
        iHaveAxe = createObjectDecision(ObjectHandler.ObjectType.AXE, myPersonality);
        myHealthBigger80= createDecisionsFloat(80, 100, myPersonality, FloatDecision.FloatDecisionTypes.HEALTH);
        Agressive3 = createDecisionsFloat(3, 10, myPersonality, FloatDecision.FloatDecisionTypes.AGGRESSIVENESS);
        hisHealthLess45_2 = createDecisionsFloat(0, 45, targetPersonality, FloatDecision.FloatDecisionTypes.HEALTH);
        random5 = createRandomDecisionFloat(1, 10, 1, 8);
        CharismaBigger3_5 = createDecisionsFloat(3, 10, myPersonality, FloatDecision.FloatDecisionTypes.CHARISMA);
        random6= createRandomDecisionFloat(1, 10, 1,4);




        root.nodeTrue = iAmMonster;
            root.nodeFalse = root;

               
                iAmMonster.nodeTrue = targetIsHuman;
                iAmMonster.nodeFalse = isHeMonster;

                    createLeaves(targetInGroup, addActionEvade(), addActionAttack());

                    targetIsHuman.nodeTrue = targetInGroup;
                    targetIsHuman.nodeFalse = addActionNothing();


                        isHeMonster.nodeTrue=iamInGroup;
                        isHeMonster.nodeFalse = TrustHimMore7;
                        iamInGroup.nodeTrue = healthBigger70;
                        iamInGroup.nodeFalse = addActionEvade();
                        createLeaves(healthBigger70, addActionAttack(), addActionEvade());


                        TrustHimMore7.nodeTrue = isInMyGroup;
                        TrustHimMore7.nodeFalse = TrustHimmore5;

                            isInMyGroup.nodeTrue = hisHealthLess40;
                            isInMyGroup.nodeFalse = addActionOfferJoinGroup(); // OFREZCO unirse a mi grupo
                                hisHealthLess40.nodeTrue = iHaveBotiquin;
                                hisHealthLess40.nodeFalse = TrustHimMore8;
                                    iHaveBotiquin.nodeTrue = addActionOffer();
                                    iHaveBotiquin.nodeFalse = iHaveMono;

                                    TrustHimMore8.nodeTrue = MyObjectIsNothing1;
                                    TrustHimMore8.nodeFalse = CharismBigger3;

                                        CharismBigger3.nodeTrue = MyObjectIsNothing2;
                                        CharismBigger3.nodeFalse = addActionNothing();
                                        iHaveMono.nodeTrue = addActionOffer();
                                        iHaveMono.nodeFalse = iHaveShield;
                                        MyObjectIsNothing1.nodeTrue = addActionNothing();
                                        MyObjectIsNothing1.nodeFalse = addActionOffer();
                                        MyObjectIsNothing2.nodeTrue = addActionNothing();
                                        MyObjectIsNothing2.nodeFalse = random1;
                                             createLeaves(iHaveShield, addActionOffer(), addActionNothing());
                                             createLeaves(random1, addActionOffer(), addActionNothing());

                            TrustHimmore5.nodeTrue = hisHealthLess45;
                            TrustHimmore5.nodeFalse = TrustHimLess3;

                                hisHealthLess45.nodeTrue = Agressive;
                                hisHealthLess45.nodeFalse = CharismBigger3_3;

                                    Agressive.nodeTrue = random2;
                                    Agressive.nodeFalse = iHaveBotiquin2;

                                     createLeaves(random2, addActionAttack(), addActionNothing());

                                        iHaveBotiquin2.nodeTrue = addActionOffer();
                                        iHaveBotiquin2.nodeFalse = CharismaBigger3_2;

                                            CharismaBigger3_2.nodeTrue = iHaveMono2;
                                            CharismaBigger3_2.nodeFalse = addActionNothing();

                                                iHaveMono2.nodeTrue = addActionOffer();
                                                iHaveMono2.nodeFalse = iHaveShield2;

                                                     createLeaves(iHaveShield2, addActionOffer(), addActionNothing());


                                    CharismBigger3_3.nodeTrue = addActionOfferJoinGroup();
                                    CharismBigger3_3.nodeFalse = Fear;

                                        Fear.nodeTrue = random3;
                                        Fear.nodeFalse = random4;
                                            createLeaves(random3, addActionOfferJoinGroup(), addActionNothing());
                                            createLeaves(random4, addActionAttack(), addActionOfferJoinGroup());


                            TrustHimLess3.nodeTrue = Agressive2;
                            TrustHimLess3.nodeFalse = Agressive3;

                                Agressive2.nodeTrue = addActionAttack();
                                Agressive2.nodeFalse = CharismaBigger3_4;

                                    CharismaBigger3_4.nodeTrue = iHaveAxe;
                                    CharismaBigger3_4.nodeFalse = addActionNothing();

                                        iHaveAxe.nodeTrue = myHealthBigger80;
                                        iHaveAxe.nodeFalse = addActionNothing();

                                          createLeaves(myHealthBigger80, addActionAttack(), addActionNothing());

                                Agressive3.nodeTrue = hisHealthLess45;
                                Agressive3.nodeFalse = CharismaBigger3_5;

                                    hisHealthLess45.nodeTrue = addActionAttack();
                                    hisHealthLess45.nodeFalse = random5;

                                        CharismaBigger3_5.nodeTrue = random6;
                                        CharismaBigger3_5.nodeFalse = addActionNothing();

                                             createLeaves(random5, addActionAttack(), addActionNothing());
                                             createLeaves(random6, addActionOfferJoinGroup(), addActionNothing());
        DecisionCompleted = true;
        reaction = this.GetComponent<DecisionTreeReactionAfterInteraction>();
        StartTheDecision();

    }

    public override void StartTheDecision()
    {
       //Debug.Log("Empiezo a decidir"+ this.gameObject.name);

        decisionNew = root;

        base.DecisionCompleted = false;

    }
 
    public override void CommunicateAction(Action actionNew)
    {
       // Debug.Log("He acabado y comunico accion");

		if (target.gameObject.tag == "IA" ||target.tag == "Player")
        {  //avisar de que vamos a interactuar con él
            ActionAttack attack = new ActionAttack();
            ActionOffer offer = new ActionOffer();
            ActionOfferOtherJoinMyGroup join= new ActionOfferOtherJoinMyGroup();

            // Debug.Log(actionNew.GetType() + ", " + attack.GetType());

            bool decision = true;

            if (Object.ReferenceEquals(actionNew.GetType(), attack.GetType())) // compare classes 
            {
				if (target.tag == "Player") {
					target.GetComponent<PlayerPersonality>().interactionFromOtherCharacter = ActionsEnum.Actions.ATTACK;
					GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerMenuController>().OpenMenu(PlayerMenuController.MenuTypes.MENU_ATTACKED,target);
					GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerMenuController> ().SetTargetIA (this.gameObject);

				} else {
					target.GetComponent<AIPersonality>().interactionFromOtherCharacter = ActionsEnum.Actions.ATTACK;
				}

//                Debug.Log("Le he dicho que le ataco");

            }
            else if (Object.ReferenceEquals(actionNew.GetType(), offer.GetType())) // compare classes 
            {			
					if (target.tag == "Player") {				
						target.GetComponent<PlayerPersonality> ().interactionFromOtherCharacter = ActionsEnum.Actions.OFFER;
						GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerMenuController> ().OpenMenu (PlayerMenuController.MenuTypes.MENU_OFFERED_OBJECT, target);
						GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerMenuController> ().SetTargetIA (this.gameObject);
					} else {
						target.GetComponent<AIPersonality> ().interactionFromOtherCharacter = ActionsEnum.Actions.OFFER;

					}
					
            }

            else if (Object.ReferenceEquals(actionNew.GetType(), join.GetType())) // compare classes 
            {
				
				if (this.gameObject.GetComponent<GroupScript> ().groupMembers.Count
				    + target.GetComponent<GroupScript> ().groupMembers.Count< 2) {

					if (target.tag == "Player") {
						target.GetComponent<PlayerPersonality> ().interactionFromOtherCharacter = ActionsEnum.Actions.JOIN;
						GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerMenuController> ().OpenMenu (PlayerMenuController.MenuTypes.MENU_OFFERED_JOIN, target);
						GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerMenuController> ().SetTargetIA (this.gameObject);



					} else {
						target.GetComponent<AIPersonality> ().interactionFromOtherCharacter = ActionsEnum.Actions.JOIN;

					}
					//Debug.Log("Le he dicho que se una a mi grupo");

				} else {
					//Debug.Log ("El grupo es muy grande te jodes");
					decision = false;

				}


            }
            else {
                //Debug.Log("Mi accion es NOTHING y NO le digo nada");
                decision = false;
            }



            if (decision)
            {

				if (target.tag != "Player") {
					if (reaction != null) {
						Destroy (reaction);
					}
					reaction = target.AddComponent<DecisionTreeReactionAfterInteraction> ();
                    if (this != null) { reaction.target = this.gameObject; }
					target.GetComponent<VisibilityConeCycleIA>().enabled = false;


				}
              

            }
        }
    }

}
