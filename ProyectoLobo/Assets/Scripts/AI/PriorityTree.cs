using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PriorityTree : MonoBehaviour
{

    private int priority;

    public int GetPriority(GameObject target, PersonalityBase Personality)
    {
        if (target.name == "Medicalaid") // Es botiquin
        {
            if (Personality.health < 20)
                priority = 4;

            else
                priority = 0;
        }
        else // No es botiquin
        {
            if (Personality.charisma > Personality.selfAssertion && Personality.charisma > Personality.fear) //Es carismática
            {
                //Debug.Log("SUperCarismática");
                if (target.tag == "IA" || target.tag == "player") // Es una MockIAa
                {
                    GroupScript iagroup = target.GetComponent<GroupScript>();
                    int confidence = Personality.TrustInOthers[target.GetComponent<PersonalityBase>().GetMyOwnIndex()];

                    if (confidence > 5) // Confía en ella
                        if (iagroup.checkIAInGroup(this.gameObject))
                        {
                            priority = 0;
                        }
                        else
                        {
                            if (iagroup.groupLeader == target && iagroup.inGroup)
                                priority = 3;
						else if (!iagroup.inGroup )
                            {
                                priority = 0;
                            }
                            else
                                priority = -1;
                        }
                    else // No confía en ella
                        priority = 1;
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
                /*  Debug.Log("Miedo: " + Personality.fear);
                  Debug.Log("Agresividad: " + Personality.selfAssertion);
                  Debug.Log("Miedoso");*/
                if (target.tag == "IA" || target.tag == "player")
                {
                    GroupScript iagroup = target.GetComponent<GroupScript>();
                    int confidence = Personality.TrustInOthers[target.GetComponent<PersonalityBase>().GetMyOwnIndex()];
                    if (confidence > 5)
                    {
                        if (target.GetComponent<GroupScript>().checkIAInGroup(this.gameObject))
                        {
                            priority = 0;
                        }
                        else
                        {
                            if (iagroup.groupLeader == target && iagroup.inGroup)
                                priority = 1;
                            else if (!iagroup.inGroup)
                            {
                                priority = 0;
                            }
                            else
                                priority = -1;
                        }
                    }
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
                /* Debug.Log("Miedo: " + Personality.fear);
                 Debug.Log("Agresividad: " + Personality.selfAssertion);
                 Debug.Log("Agresivo");*/
                if (target.tag == "IA" || target.tag == "player")
                {
                    GroupScript iagroup = target.GetComponent<GroupScript>();
                    int confidence = Personality.TrustInOthers[target.GetComponent<PersonalityBase>().GetMyOwnIndex()];
                    if (confidence > 5)
                    {
                        if (target.GetComponent<GroupScript>().checkIAInGroup(this.gameObject))
                        {
                            priority = 0;
                        }
                        else
                        {
                            if (iagroup.groupLeader == target && iagroup.inGroup)
                            {
                                if (Personality.charisma > Personality.fear)
                                    priority = 3;
                                else
                                    priority = 2;
                            }
                            else if (!iagroup.inGroup)
                            {
                                priority = 0;
                            }
                            else
                                priority = -1;

                        }
                    }
                    else
                        priority = 1;
                }
                else if (target.name == "Axe")
                    priority = 3;
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
		//Debug.Log("Soy : "+ this.gameObject.name+ " y veo a T : " + target + " y la prio es " +priority);

        return priority;
    }
}
