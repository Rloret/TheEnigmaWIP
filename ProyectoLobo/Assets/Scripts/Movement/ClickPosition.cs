using UnityEngine;
using System.Collections;

public class ClickPosition : MonoBehaviour {

    public Vector2 clickPos;

    // Update is called once per frame
    void Update ()
    { 
        if (Input.GetMouseButtonDown(0)) //Si clicamos
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(mouseWorldPos.x, mouseWorldPos.y); //Esto coje la posición en la pantalla

            clickPos = mousePos; //ATENCIÓN, si usamos colliders hay que quitar esta asignación

            /*
            Vector2 dir = Vector2.zero;
            RaycastHit2D hit = Physics2D.Raycast(mousePos, dir);
            
            if(hit != null && hit.collider != null)
            {
                clickPos = mousePos;
            }
            */ //Este trozo la coje sólo cuando ha clicado en un collider
        }
    }
}