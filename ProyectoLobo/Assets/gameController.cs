using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

    public GameObject[] spawnPoints;
    public Sprite[] Colors;
    public List<GameObject> spawnersLeft;
    
    public int numberOfIAs = 0;
    public GameObject mockIA;
    public GameObject LifeImage;

    private GameObject currentIA;
	// Use this for initialization
	void Start () {
        spawnersLeft = new List<GameObject>();
        spawnersLeft.AddRange(spawnPoints);
        if (numberOfIAs > spawnPoints.Length && spawnPoints.Length>6)
        {
            numberOfIAs = spawnPoints.Length - 3;
        }
        int i = 3;
        AIPersonality.Personalities type = (AIPersonality.Personalities)Random.Range(0,1);
        int spawnPos = Random.Range(0, spawnersLeft.Count - 1);
        int colorSprite = Random.Range(0, Colors.Length-1);
        instantiateIA(type, spawnersLeft[spawnPos].transform.position,0,Colors[colorSprite]);
        spawnersLeft.Remove(spawnersLeft[spawnPos]);
        type = (AIPersonality.Personalities)Random.Range(2,3);
        spawnPos = Random.Range(0, spawnersLeft.Count - 1);
        colorSprite = Random.Range(0, Colors.Length - 1);
        instantiateIA(type, spawnersLeft[spawnPos].transform.position, 1, Colors[colorSprite]);
        spawnersLeft.Remove(spawnersLeft[spawnPos]);
        type = (AIPersonality.Personalities)Random.Range(4, 5);
        spawnPos = Random.Range(0, spawnersLeft.Count - 1);
        colorSprite = Random.Range(0, Colors.Length - 1);
        instantiateIA(type, spawnersLeft[spawnPos].transform.position, 2, Colors[colorSprite]);
        spawnersLeft.Remove(spawnersLeft[spawnPos]);

        while (i < numberOfIAs)
        {
            spawnPos = Random.Range(0, spawnersLeft.Count - 1);
            type = (AIPersonality.Personalities)Random.Range(0, 5);
            colorSprite = Random.Range(0, Colors.Length - 1);
            instantiateIA(type, spawnersLeft[spawnPos].transform.position, i, Colors[colorSprite]);
            spawnersLeft.Remove(spawnersLeft[spawnPos]);
            i++;
        }

	}

    private void instantiateIA(AIPersonality.Personalities type,Vector3 pos,int number,Sprite color)
    {
        currentIA = Instantiate(mockIA);
        currentIA.name = "IA";
        currentIA.name += number;
        currentIA.GetComponent<SpriteRenderer>().sprite = color;
        currentIA.transform.position = pos; 
        AIPersonality Aipers = currentIA.GetComponent<AIPersonality>();
        Aipers.MyOwnIndex = number;
        Aipers.health = 100;
        Aipers.attack = 10;
        Aipers.configurePersonality(type);
        GameObject image = Instantiate(LifeImage);
        Aipers.HealthImage = image;
        image.transform.parent = Aipers.panel.transform;
        image.transform.position = currentIA.transform.position;
        image.transform.localScale = Vector3.one;

        VisibleElements.visibleGameObjects.Add(currentIA);
    }

    
}
