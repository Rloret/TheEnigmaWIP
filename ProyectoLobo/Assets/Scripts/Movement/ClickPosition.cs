using UnityEngine;
using System.Collections;

public class ClickPosition : MonoBehaviour {

    public Vector2 clickPos;
    public enum SteeringBehaviour
    {
        NOTHING,SEEK,FLEE,ARRIVE,LEAVE,PURSUE,EVADE,ALIGN,FACE,WANDER
    };

    public SteeringBehaviour Comportamiento;

    void Update ()
    {
        if (Input.GetMouseButtonDown(0)) //Si clicamos
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(mouseWorldPos.x, mouseWorldPos.y); //Esto coje la posición en la pantalla

            //clickPos = mousePos; //ATENCIÓN, si usamos colliders hay que quitar esta asignación
          //  Debug.Log(mousePos);

            //TileMap.getTileMapInstance.tilesDataMap[(int)clickPos.x, (int)clickPos.y].Holder.GetComponent<SpriteRenderer>().color = Color.red;

            
            Vector2 dir = Vector2.zero;
            RaycastHit2D hit = Physics2D.Raycast(mousePos, dir);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);

                GameObject aux = hit.collider.gameObject;
                aux.GetComponent<SpriteRenderer>().color = Color.red;
                addBehaviour(Comportamiento, aux);
                if(GetComponent<AgentBehaviour>()!= null) GetComponent<AgentBehaviour>().target = aux;
                clickPos = mousePos;
            }
        }
    }

    void addBehaviour(SteeringBehaviour comportamiento,GameObject target)
    {
        if (this.GetComponent<AgentBehaviour>() != null)
        {
           
           DestroyImmediate(GetComponent<AgentBehaviour>());
            // this.gameObject.AddComponent<Align>(); 
        }

        switch (comportamiento)
        {
            case SteeringBehaviour.SEEK:
                this.gameObject.AddComponent<Seek>();
                break;
            case SteeringBehaviour.FLEE:
                this.gameObject.AddComponent<Flee>();
                break;
            case SteeringBehaviour.ARRIVE:
                this.gameObject.AddComponent<Arrive>();
                break;
            case SteeringBehaviour.LEAVE:
                this.gameObject.AddComponent<Leave>();
                break;
            case SteeringBehaviour.PURSUE:
                this.gameObject.AddComponent<Pursue>();
                break;
            case SteeringBehaviour.EVADE:
                this.gameObject.AddComponent<Evade>();
                break;
            case SteeringBehaviour.ALIGN:
                this.gameObject.AddComponent<Align>();
                break;
            case SteeringBehaviour.FACE:
                this.gameObject.AddComponent<Face>();
                break;
            case SteeringBehaviour.WANDER:
                this.gameObject.AddComponent<Wander>();
                break;
            default:
                break;
        }



    }
}