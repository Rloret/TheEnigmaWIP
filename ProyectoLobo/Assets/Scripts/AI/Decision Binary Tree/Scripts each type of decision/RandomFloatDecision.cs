using UnityEngine;
using System.Collections;

public class RandomFloatDecision : Decision
{

    public float maxValue ;
    public float minvalue ;
    public float randomRangeMin ;
    public float randomRangeMax ;


    public override DecisionTreeNode GetBranch()
    {
        float r = Random.Range(randomRangeMin, randomRangeMax);
        if (maxValue >= r && r >= minvalue)
        {
            return nodeTrue;

        }
        return nodeFalse;
        
    }
}
