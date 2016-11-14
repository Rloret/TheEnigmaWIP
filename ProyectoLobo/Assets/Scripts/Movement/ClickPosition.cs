using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ClickPosition : MonoBehaviour {

    public Vector2 clickPos;
    public OnObjectClickedController clickController;
    public GameObject fogOfWar;

    void Start()
    {
        //fogOfWar = GameObject.FindGameObjectWithTag("FogOfWar");
    }
    void Update ()
    {
        if (Input.GetMouseButtonDown(0)) //Si clicamos
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(mouseWorldPos.x, mouseWorldPos.y); //Esto coje la posición en la pantalla
            
            Vector2 dir = Vector2.zero;
            RaycastHit2D hit = Physics2D.Raycast(mousePos, dir);

            //int counter = 0;

            if (hit.collider != null)
            {

                GameObject aux = hit.collider.gameObject;

                clickController.DetermineAction(this.gameObject,aux);

            }
        }
    }

   
}