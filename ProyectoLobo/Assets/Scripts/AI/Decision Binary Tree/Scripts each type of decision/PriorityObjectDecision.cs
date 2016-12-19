using UnityEngine;
using System.Collections;

public class PriorityObjectDecision : Decision
{

	public PersonalityBase characterPersonality;
	public PersonalityBase targetPersonality; // who gives the object to me


    private DecisionTarget decisiontargetScript;
    private PriorityTree prioTree;





    public override DecisionTreeNode GetBranch()
    {
        decisiontargetScript = GetComponent<DecisionTarget>();
        prioTree = GetComponent<PriorityTree>();

		string objectName = decisiontargetScript.objectTraduction( this.GetComponent<DecisionTreeCreator>().target.GetComponent<PersonalityBase>());
		Debug.Log ("el target es : " + this.GetComponent<DecisionTreeCreator>().target);

		Debug.Log ("el objeto es : " + objectName);
        GameObject aux = new GameObject();

        aux.name = objectName;

		int priority = prioTree.GetPriority (aux, characterPersonality);
		Debug.Log ("La prioridd del objeto es: " + priority);

		if ( priority>=2)
        {
            return nodeTrue;
        }
        else return nodeFalse;



    }
}
