using UnityEngine;
using System.Collections;

public class PriorityTree : MonoBehaviour {

    private int priority;

    public int GetPriority (GameObject target, AIPersonality personality)
    {
        if (target.name == "botiquin") // Es botiquin
        {
            if (personality.health < 50)
                priority = 4;

            else
                priority = 0;
        }
        else // No es botiquin
        {
            if (personality.charisma > personality.selfAssertion && personality.charisma > personality.fear) //Es carismática
            {
                if (target.name == "persona") // Es una persona
                {
                    if (personality.confidence > 50) // Confía en ella
                        priority = 3;
                    else // No confía en ella
                        priority = 0;
                }
                else // No es una persona
                {
                    if (target.name == "hacha") // es un hacha FALTA condicion y no llevo
                    {
                        if (personality.selfAssertion > personality.fear) // más agresivo que miedoso
                            priority = 2;
                        else // más miedoso que agresivo
                            priority = 1;
                    }
                    else if (target.name == "mono") // es un mono// no es hacha
                    {
                        if (personality.selfAssertion > personality.fear) // es agresivo más que miedoso
                            priority = 1;
                        else 
                            priority = 2;
                    }
                    else // es otro objeto
                        priority = 1;
                }
            }

            else if (personality.fear > personality.selfAssertion)
            {
                if (target.name == "persona")
                {
                    if (personality.confidence > 50)
                        priority = 1;
                    else
                        priority = 0;
                }
                else
                {
                    if (target.name == "mono")
                        priority = 2;
                    else if (target.name == "hacha")
                    {
                        if (personality.selfAssertion > personality.charisma)
                            priority = 2;
                        else
                            priority = 1;
                    }
                    else
                        priority = 1;
                }
            }
            else
            {
                if (target.name == "persona")
                {
                    if (personality.confidence > 50)
                    {
                        if (personality.charisma > personality.fear)
                            priority = 3;
                        else
                            priority = 2;
                    }
                    else
                        priority = 0;
                }
                else if (target.name == "hacha")
                    priority = 3;
                else if (personality.fear > personality.charisma)
                {
                    if (target.name == "mono")
                        priority = 2;
                    else
                        priority = 1;
                }
                else
                {
                    if (target.name == "botas")
                        priority = 2;
                    else
                        priority = 1;
                }
            }

        }

        return priority;
    }
}
