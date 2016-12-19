﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

    public GameObject[] spawnPoints;
    public Sprite Colors;
    public List<GameObject> spawnersLeft;
    public Gradient gradient;
    
    public int numberOfIAs = 0;
    public GameObject mockIA;
    public GameObject LifeImage;

	public bool allCharisma=false;
	public Text textMonster;


    private GameObject currentIA;
    private float percentColor;

	// Use this for initialization
	void Start () {

        float iterator = 1f;
        Color currentColor;
        float randomColor = 0f;
        spawnersLeft = new List<GameObject>();
        spawnersLeft.AddRange(spawnPoints);
        if (numberOfIAs > spawnPoints.Length && spawnPoints.Length>6)
        {
            numberOfIAs = spawnPoints.Length - 3;
        }
        numberOfIAs += 1;


		AIPersonality.Personalities type = (AIPersonality.Personalities)Random.Range (0, 1);
		int spawnPos = Random.Range (0, spawnersLeft.Count - 1);


		if (!allCharisma) {
			int i = 3;
            randomColor = Random.Range((iterator - 1) / numberOfIAs, iterator / numberOfIAs);
            currentColor = gradient.Evaluate(randomColor);
			instantiateIA (type, spawnersLeft [spawnPos].transform.position, 0,currentColor );
			spawnersLeft.Remove (spawnersLeft [spawnPos]);
            iterator++;
            Debug.Log(randomColor);

            type = (AIPersonality.Personalities)Random.Range (2, 3);
			spawnPos = Random.Range (0, spawnersLeft.Count - 1);
            randomColor = Random.Range((iterator - 1) / numberOfIAs, iterator / numberOfIAs);
            currentColor = gradient.Evaluate(randomColor);
            instantiateIA (type, spawnersLeft [spawnPos].transform.position, 1,currentColor);
			spawnersLeft.Remove (spawnersLeft [spawnPos]);
            iterator++;
            Debug.Log(randomColor);

            type = (AIPersonality.Personalities)Random.Range (4, 5);
			spawnPos = Random.Range (0, spawnersLeft.Count - 1);
            randomColor = Random.Range((iterator - 1) / numberOfIAs, iterator / numberOfIAs);
            currentColor = gradient.Evaluate(randomColor);
            instantiateIA (type, spawnersLeft [spawnPos].transform.position, 2,currentColor );
			spawnersLeft.Remove (spawnersLeft [spawnPos]);
            iterator++;
            Debug.Log(randomColor);

            while (i < numberOfIAs - 1) {
				spawnPos = Random.Range (0, spawnersLeft.Count - 1);
				type = (AIPersonality.Personalities)Random.Range (0, 5);
                randomColor = Random.Range((iterator - 1) / numberOfIAs, iterator / numberOfIAs);
                currentColor = gradient.Evaluate(randomColor);
                instantiateIA (type, spawnersLeft [spawnPos].transform.position, i, currentColor);
				spawnersLeft.Remove (spawnersLeft [spawnPos]);
				i++;
                iterator++;
                //Debug.Log(randomColor);    
            }

			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			PlayerPersonality personality = player.GetComponent<PlayerPersonality> ();
			personality.configurePlayer (new Color(151,76,154,255)/255);
		} else {
			int i = 0;

			while (i < numberOfIAs - 1) {
				
				spawnPos = Random.Range (0, spawnersLeft.Count - 1);
				type = AIPersonality.Personalities.SFA;
                currentColor = gradient.Evaluate(Random.Range((i - 1) / numberOfIAs, i / numberOfIAs));
                instantiateIA (type, spawnersLeft [spawnPos].transform.position, i,currentColor);
				spawnersLeft.Remove (spawnersLeft [spawnPos]);
				i++;
			}

		}

		determineMonster ();

        /*personality.health = 100;
        personality.attack = 10;

        personality.MyOwnIndex = numberOfIAs - 1;*/

    }
	private void determineMonster(){
		int r = Random.Range (0, 1);

		if (r == 1) {
			textMonster.text = "YOU ARE THE MONSTER!";
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PersonalityBase> ().theThing = true;

		} else {
			textMonster.text = "YOU ARE NOT THE MONSTER!";
			GameObject.FindGameObjectWithTag ("IA").GetComponent<PersonalityBase> ().theThing = true;
		}
		Invoke ("deleteText", 2f);
	}

	private void deleteText(){
		textMonster.enabled = false;
	}

    private void instantiateIA(AIPersonality.Personalities type,Vector3 pos,int number,Color color)
    {
        currentIA = Instantiate(mockIA);
        currentIA.name = "IA";
        currentIA.name += number;
        currentIA.GetComponent<SpriteRenderer>().color = color;
        currentIA.transform.position = pos; 
        AIPersonality Aipers = currentIA.GetComponent<AIPersonality>();
		Aipers.SetMyOwnIndex( number);
        Aipers.health = 100;
        Aipers.attack = 10;
        Aipers.configurePersonality(type);
        GameObject image = Instantiate(LifeImage);
        Aipers.HealthImage = image;
        image.transform.SetParent( Aipers.panel.transform);
        image.transform.position = currentIA.transform.position;
        image.transform.localScale = Vector3.one;

        var collider = currentIA.GetComponent<BoxCollider2D>();
        //esto tiene que ser asi

        collider.size = new Vector2(9.7f,12f);
        VisibleElements.visibleGameObjects.Add(currentIA);
    }

    
}
