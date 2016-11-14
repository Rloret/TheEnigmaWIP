using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    private GameObject player;
    private Camera mainCam;
    private int tilesize = 64;
    private int mapWidth = 39;
    private int mapheight = 29;
    private int mapWidthHalf = 40;
    private int mapheightHalf = 30;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = Camera.main;
        mapWidth *= tilesize;
        mapheight *= tilesize;
        mapWidthHalf = mapWidth / 2;
        mapheightHalf = mapheight / 2;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 From = this.transform.position;
        Vector3 To = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
       /* 
        //Debug.Log(mapheightHalf);
        if (player.transform.position.x + mainCam.pixelWidth / 2 >=  mapWidthHalf)

        {
            Debug.Log("limite Derecho");
            To = new Vector3(mapWidth - mapWidthHalf -mainCam.pixelWidth/2, From.y, From.z);
        }

        else if (player.transform.position.x - mainCam.pixelWidth / 2 <= -mapWidthHalf)
        {
            Debug.Log("limite iaquierdo");
            To = new Vector3(-mapWidthHalf + mainCam.pixelWidth / 2, From.y, From.z);
        }


        if(player.transform.position.y + mainCam.pixelHeight/2 >= mapheightHalf)
        {
            To = new Vector3(From.x,  mapheightHalf- mainCam.pixelHeight / 2, From.z);
        }
        else if(player.transform.position.y + mainCam.pixelHeight / 2 <= -mapheightHalf)
        {
            To = new Vector3(From.x, - mapheightHalf+mainCam.pixelHeight / 2, From.z);
        }*/
          
        this.transform.position = Vector3.Lerp(From, To, 0.9f * Time.deltaTime);
	}
}
