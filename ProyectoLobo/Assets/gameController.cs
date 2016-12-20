using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class gameController : MonoBehaviour
{

    public GameObject[] spawnPoints;
    public Sprite SpriteBase;
    public List<GameObject> spawnersLeft;
    public Gradient gradient;

    public int numberOfIAs = 0;
    public GameObject mockIA;
    public GameObject LifeImage;

    public bool allCharisma = false;
    public Text textMonster;


    private GameObject currentIA;
    private float percentColor;

    public int numberOfMonsters = 1;
    public int numberOfHumans;

    // Use this for initialization
    void Start()
    {

        float iterator = 1f;
        Color currentColor;
        float randomColor = 0f;
        spawnersLeft = new List<GameObject>();
        spawnersLeft.AddRange(spawnPoints);

        numberOfHumans = numberOfIAs;

        if (numberOfIAs > spawnPoints.Length && spawnPoints.Length > 6)
        {
            numberOfIAs = spawnPoints.Length - 3;
        }
        numberOfIAs += 1;


        AIPersonality.Personalities type = (AIPersonality.Personalities)Random.Range(0, 1);
        int spawnPos = Random.Range(0, spawnersLeft.Count - 1);


        if (!allCharisma)
        {
            int i = 3;
            randomColor = Random.Range((iterator - 1) / numberOfIAs, iterator / numberOfIAs);
            currentColor = gradient.Evaluate(randomColor);
            instantiateIA(type, spawnersLeft[spawnPos].transform.position, 0, currentColor);
            spawnersLeft.Remove(spawnersLeft[spawnPos]);
            iterator++;
            //Debug.Log(randomColor);


            type = (AIPersonality.Personalities)Random.Range(2, 3);
            spawnPos = Random.Range(0, spawnersLeft.Count - 1);
            randomColor = Random.Range((iterator - 1) / numberOfIAs, iterator / numberOfIAs);
            currentColor = gradient.Evaluate(randomColor);
            instantiateIA(type, spawnersLeft[spawnPos].transform.position, 1, currentColor);
            spawnersLeft.Remove(spawnersLeft[spawnPos]);
            iterator++;
            //Debug.Log(randomColor);

            type = (AIPersonality.Personalities)Random.Range(4, 5);
            spawnPos = Random.Range(0, spawnersLeft.Count - 1);
            randomColor = Random.Range((iterator - 1) / numberOfIAs, iterator / numberOfIAs);
            currentColor = gradient.Evaluate(randomColor);
            instantiateIA(type, spawnersLeft[spawnPos].transform.position, 2, currentColor);
            spawnersLeft.Remove(spawnersLeft[spawnPos]);
            iterator++;
            //Debug.Log(randomColor);

            while (i < numberOfIAs - 1)
            {
                spawnPos = Random.Range(0, spawnersLeft.Count - 1);
                type = (AIPersonality.Personalities)Random.Range(0, 5);
                randomColor = Random.Range((iterator - 1) / numberOfIAs, iterator / numberOfIAs);
                currentColor = gradient.Evaluate(randomColor);
                instantiateIA(type, spawnersLeft[spawnPos].transform.position, i, currentColor);
                spawnersLeft.Remove(spawnersLeft[spawnPos]);
                i++;
                iterator++;
                //Debug.Log(randomColor);    

            }

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerPersonality personality = player.GetComponent<PlayerPersonality>();
            personality.configurePlayer(new Color(250, 250, 250, 255) / 255);
        }
        else
        {
            int i = 0;

            while (i < numberOfIAs - 1)
            {

                spawnPos = Random.Range(0, spawnersLeft.Count - 1);
                type = AIPersonality.Personalities.SFA;
                currentColor = gradient.Evaluate(Random.Range((i - 1) / numberOfIAs, i / numberOfIAs));
                instantiateIA(type, spawnersLeft[spawnPos].transform.position, i, currentColor);
                spawnersLeft.Remove(spawnersLeft[spawnPos]);
                i++;
            }

        }

        determineMonster();

        /*personality.health = 100;
        personality.attack = 10;

        personality.MyOwnIndex = numberOfIAs - 1;*/

        //Al final del start se activan los colliders de las habitaciones para que se puedan guardar las IAs las habitaciones donde spawneen
        //List<GameObject> Rooms = new List<GameObject>();
        //Rooms.AddRange(GameObject.FindGameObjectsWithTag("RoomsAndDoors"))
        foreach ( GameObject room in GameObject.FindGameObjectsWithTag("RoomsAndDoors") ) {
            room.GetComponent<LocationDiscovery>().enabled = true;
        }

    }
    private void determineMonster()
    {
        int r = Random.Range(0, 2);

        if (r == 1)
        {
            textMonster.text = "YOU ARE THE MONSTER!";
            GameObject.FindGameObjectWithTag("Player").GetComponent<PersonalityBase>().theThing = true;

        }
        else
        {
            textMonster.text = "YOU ARE NOT THE MONSTER!";
            GameObject.FindGameObjectWithTag("IA").GetComponent<PersonalityBase>().theThing = true;
        }

        this.gameObject.GetComponent<PlayerMenuController>().CheckIfMonster();

        Invoke("deleteText", 2f);
    }

    private void deleteText()
    {
        textMonster.enabled = false;
    }

    private void instantiateIA(AIPersonality.Personalities type, Vector3 pos, int number, Color color)
    {
        currentIA = Instantiate(mockIA);
        currentIA.GetComponent<SpriteRenderer>().sprite = SpriteBase;
        currentIA.name = "IA";
        currentIA.name += number;
        currentIA.GetComponent<SpriteRenderer>().color = color;
        currentIA.transform.position = pos;
        AIPersonality Aipers = currentIA.GetComponent<AIPersonality>();
        Aipers.SetMyOwnIndex(number);
        Aipers.health = 100;
        Aipers.attack = 10;
        Aipers.configurePersonality(type);
        GameObject image = Instantiate(LifeImage);
        Aipers.HealthImage = image;
        image.transform.SetParent(Aipers.panel.transform);
        image.transform.position = currentIA.transform.position;
        image.transform.localScale = Vector3.one;

        var collider = currentIA.GetComponent<BoxCollider2D>();
        //esto tiene que ser asi

        collider.size = new Vector2(9.7f, 12f);
        VisibleElements.visibleGameObjects.Add(currentIA);
        currentIA.transform.Rotate(Vector3.up * Random.Range(0f, 1f) * 360);
    }

    public bool CheckPlayerWin()
    {

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PersonalityBase>().theThing)
        {   // si eres monstruo
            if (numberOfHumans <= 0)
            {
                Debug.Log("return true ");

                return true;
            }

        }
        else
        {
            if (numberOfMonsters <= 0)
            {
                Debug.Log("return true ");

                return true;
            }

        }
        Debug.Log("return false ");

        return false;
    }

    public bool CheckPlayerLost()
    {

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PersonalityBase>().theThing)
        {
            // si eres monstruo

        }
        else
        {
            if (numberOfHumans == 1)
            {
                Debug.Log("return true, solo quedas tu como humano ");

                return true;
            }

        }
        Debug.Log("return false ");

        return false;
    }

    public void youWin(bool b)
    {
        if (b)
        {
            Debug.Log("HAS GANADO ");
            textMonster.text = "YOU WIN";
        }
        else
        {
            Debug.Log("HAS PERDIDO ");
            textMonster.text = "YOU LOST";
        }
        textMonster.enabled = true;

        Invoke("replay", 2f);

    }

    private void replay()
    {
        SceneManager.LoadScene(0);

    }

    public void decreaseHumans()
    {
        numberOfHumans--;

        Debug.Log("he disminuido n humanos: " + numberOfHumans + " monstruos: " + numberOfMonsters);

        if (CheckPlayerWin())
        {
            //Debug.Log ("ha devuelto true en win");
            youWin(true);
        }
        else if (CheckPlayerLost())
        {
            //Debug.Log ("ha devuelto true en lost");
            youWin(false);

        }
    }
    void Update()
    {
        var Ias = GameObject.FindGameObjectsWithTag("IA");

        foreach (var single in Ias)
        {

            var group = single.GetComponent<GroupScript>();
            if (!group.IAmTheLeader && !group.inGroup && !single.GetComponent<VisibilityConeCycleIA>().isActiveAndEnabled)
            {

                single.GetComponent<VisibilityConeCycleIA>().enabled = true;
            }
        }

    }
}
