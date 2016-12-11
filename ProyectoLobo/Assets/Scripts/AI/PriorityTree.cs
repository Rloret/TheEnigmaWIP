﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PriorityTree : MonoBehaviour {

    private int priority;

    public int GetPriority (GameObject target, AIPersonality Personality)
    {
        if (target.name == "MedicalAid") // Es botiquin
        {
            if (Personality.health < 50)
                priority = 4;

            else
                priority = 0;
        }
        else // No es botiquin
        {
            if (Personality.charisma > Personality.selfAssertion && Personality.charisma > Personality.fear) //Es carismática
            {
                //Debug.Log("SUperCarismática");
				Debug.Log ("carisma: " + Personality.charisma + " agresividad: " + Personality.selfAssertion + " miedo: " + Personality.fear);
                if (target.name == "MockIA") // Es una MockIAa
                {
                    if (Personality.confidence > 50) // Confía en ella
                        priority = 3;
                    else // No confía en ella
                        priority = 0;
                }
                else // No es una MockIAa
                {
                    if (target.name == "Axe") // es un hacha FALTA condicion y no llevo
                    {
                        if (Personality.selfAssertion > Personality.fear) // más agresivo que miedoso
                            priority = 2;
                        else // más miedoso que agresivo
                            priority = 1;
                    }
                    else if (target.name == "Shield") // es un mono// no es hacha
                    {
                        if (Personality.selfAssertion > Personality.fear) // es agresivo más que miedoso
                            priority = 1;
                        else 
                            priority = 2;
                    }
                    else // es otro objeto
                        priority = 1;
                }
            }

            else if (Personality.fear > Personality.selfAssertion)
            {
				//Debug.Log ("soy miedoso");
                if (target.name == "MockIA")
                {
                   if (Personality.confidence > 50)
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
                        if (Personality.selfAssertion > Personality.charisma)
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
				//Debug.Log ("Soy agresivo");
				if (target.name == "MockIA") {
					if (Personality.confidence > 50) {
						if (Personality.charisma > Personality.fear)
							priority = 3;
						else
							priority = 2;
					} else
						priority = 0;
				} else if (target.name == "Axe") {
					priority = 3;
				}
                else if (Personality.fear > Personality.charisma)
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
//        Debug.Log("La prioridad es: " + priority);

        return priority;
    }
}
