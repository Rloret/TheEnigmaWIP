using UnityEngine;
using System.Collections;

public class PriorityObjectDecision : Decision
{

    public AIPersonality characterPersonality;
    public AIPersonality targetPersonality; // who gives the object to me


    private DecisionTarget decisiontargetScript;
    private PriorityTree prioTree;





    public override DecisionTreeNode GetBranch()
    {
        decisiontargetScript = GetComponent<DecisionTarget>();
        prioTree = GetComponent<PriorityTree>();

        string objectName = decisiontargetScript.objectTraduction(this.GetComponent<DecisionTreeCreator>().target.GetComponent<AIPersonality>()/*targetPersonality*/);

        GameObject aux = new GameObject();

        aux.name = objectName;

        if (prioTree.GetPriority(aux, characterPersonality) == 3)
        {
            return nodeTrue;
        }
        else return nodeFalse;



    }
}
