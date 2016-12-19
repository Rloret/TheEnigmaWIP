using UnityEngine;
using System.Collections;

public class ObjectDecision : Decision {

    public ObjectHandler.ObjectType objectWanted;
	public PersonalityBase myPersonality;


    public override DecisionTreeNode GetBranch() {

        if (objectWanted == myPersonality.myObject)
        {
            return nodeTrue;
        }

        return nodeFalse;
            

    }



}
