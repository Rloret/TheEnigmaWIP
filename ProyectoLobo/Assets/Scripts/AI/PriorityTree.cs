using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
                Debug.Log("SUperCarismática");
                if (target.name == "persona") // Es una persona
                {
                    if (personality.confidence > 50) // Confía en ella
                        priority = 3;
                    else // No confía en ella
                        priority = 0;
                }
                else // No es una persona
                {
                    if (target.name == "Axe") // es un hacha FALTA condicion y no llevo
                    {
                        if (personality.selfAssertion > personality.fear) // más agresivo que miedoso
                            priority = 2;
                        else // más miedoso que agresivo
                            priority = 1;
                    }
                    else if (target.name == "Shield") // es un mono// no es hacha
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
                Debug.Log("Miedo: " + personality.fear);
                Debug.Log("Agresividad: " + personality.selfAssertion);
                Debug.Log("Miedoso");
                if (target.name == "persona")
                {
                   if (personality.confidence > 50)
                        priority = 1;
                    else
                        priority = 0;
                }
                else
                {
                    if (target.name == "Shield")
                        priority = 2;
                    else if (target.name == "Axe")
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
                Debug.Log("Miedo: " + personality.fear);
                Debug.Log("Agresividad: " + personality.selfAssertion);
                Debug.Log("Agresivo");
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
                else if (target.name == "Axe")
                    priority = 3;
                else if (personality.fear > personality.charisma)
                {
                    if (target.name == "Shield")
                        priority = 2;
                    else
                        priority = 1;
                }
                else
                {
                    if (target.name == "Boots")
                        priority = 2;
                    else
                        priority = 1;
                }
            }

        }
        Debug.Log("La prioridad es: " + priority);

        return priority;
    }
}
