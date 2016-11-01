using UnityEngine;
using System.Collections;

public class Decision : DecisionTreeNode {
    public Action nodeTrue;
    public Action nodeFalse;

    public virtual Action GetBranch()
    {
        return null;
    }

    public override DecisionTreeNode MakeDecision()
    {
        Action branch = GetBranch();
        return branch.MakeDecision();
    }
}
