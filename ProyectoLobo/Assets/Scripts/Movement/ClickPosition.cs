using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ClickPosition : MonoBehaviour {

    public Vector2 clickPos;
    public enum SteeringBehaviour
    {
        NOTHING,SEEK,FLEE,ARRIVE,LEAVE,PURSUE,EVADE,ALIGN,FACE,WANDER
    };

    [System.Serializable]
    public class WeightedBehaviours
    {
       [SerializeField]
        public SteeringBehaviour Behaviour;
        [SerializeField]
        [Range(0, 1)]
        public float weight;

        
    }
    
    public WeightedBehaviours[] WeightedBehavioursArray;
 


    void Update ()
    {
        if (Input.GetMouseButtonDown(0)) //Si clicamos
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(mouseWorldPos.x, mouseWorldPos.y); //Esto coje la posición en la pantalla
            
            Vector2 dir = Vector2.zero;
            RaycastHit2D hit = Physics2D.Raycast(mousePos, dir);
            int counter = 0;

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);

                GameObject aux = hit.collider.gameObject;
                aux.GetComponent<SpriteRenderer>().color = Color.red;

                foreach (var comportamiento in this.GetComponents<AgentBehaviour>())
                {
                    DestroyImmediate(comportamiento);
                }

                foreach (var comportamiento in WeightedBehavioursArray)
                {

                    addBehaviour(comportamiento.Behaviour,aux,comportamiento.weight);
                    counter++;
                }

            }
        }
    }

    void addBehaviour(SteeringBehaviour comportamiento,GameObject aux,float weight)
    {
        
        switch (comportamiento)
        {
            case SteeringBehaviour.SEEK:
                this.gameObject.AddComponent<Seek>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.FLEE:
                this.gameObject.AddComponent<Flee>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.ARRIVE:
                this.gameObject.AddComponent<Arrive>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.LEAVE:
                this.gameObject.AddComponent<Leave>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.PURSUE:
                this.gameObject.AddComponent<Pursue>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.EVADE:
                this.gameObject.AddComponent<Evade>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.ALIGN:
                this.gameObject.AddComponent<Align>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.FACE:
                this.gameObject.AddComponent<Face>().setTarget(aux).setWeight(weight);
                break;
            case SteeringBehaviour.WANDER:
                this.gameObject.AddComponent<Wander>().setTarget(aux).setWeight(weight);
                break;
            default:
                break;
        }
     
    }
}