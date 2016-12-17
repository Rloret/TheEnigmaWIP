using UnityEngine;
using System.Collections;

public class Decision : DecisionTreeNode {


    public virtual DecisionTreeNode GetBranch()
    {
        return null;
    }

    public override DecisionTreeNode MakeDecision()
    {
        DecisionTreeNode branch = GetBranch();
     //  Debug.Log("branch es " + branch);

        return branch;
    }

    public Action toAction()
    {
         DecisionTreeNode nt=this.nodeTrue;
         DecisionTreeNode nf=this.nodeFalse;

     /*   Debug.Log("nt antes es " + nt);
        Debug.Log("nf antes es " + nf);*/


      /*  Debug.Log("this es " + this);
        DecisionTreeNode d = this as DecisionTreeNode;
        Debug.Log("this as DTN es " + d);*/

        Action a = gameObject.AddComponent<Action>();

        Debug.Log("a es " + a);

         a.nodeFalse = nf;
         a.nodeTrue = nt;

         Debug.Log("a.nt  es " + a.nodeTrue);
         Debug.Log("a.nf  es " + a.nodeFalse);

         a.activated = false;

         return a;

    }
}
