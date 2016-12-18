using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Tester : MonoBehaviour {

    public GameObject mockIA;
    public InputField input;
    public GameObject currentIA;
    public bool fixIt=false;
    public bool isTextBoxOpen=false;
    private string code;

    private List<GameObject> IAs;
    void Start()
    {
        IAs = new List<GameObject>();
    }
	void Update () {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isTextBoxOpen)
            {
                isTextBoxOpen = false;
                code = input.text;
                executeCode(code);
                input.enabled = false;
                fixIt = true;
            }
            else
            {
                isTextBoxOpen = true;
                input.enabled = true;
                input.text = "";
                fixIt = false;
 
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            fixIt = !fixIt;
        }
        if (fixIt)
        {
            if (currentIA != null)
            {
                fixIA(currentIA); 
            }
        }
       /* if (Input.GetKey(KeyCode.LeftShift) && !fixIt)
        {

            if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        instantiateIA(AIPersonality.Personalities.SAF);
                    }
                }
                if (Input.GetKey(KeyCode.F))
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                       instantiateIA(AIPersonality.Personalities.SFA);
                    }
                       
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("hellA");
                if (Input.GetKey(KeyCode.S))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        Debug.Log("hellF");
                        instantiateIA(AIPersonality.Personalities.ASF);
                    }
                }
                if (Input.GetKey(KeyCode.F))
                {
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        instantiateIA(AIPersonality.Personalities.AFS);
                    }
                }
            }
            if (Input.GetKey(KeyCode.F))
            {
                Debug.Log("hellF");
                if (Input.GetKey(KeyCode.A))
                {
                    Debug.Log("hellA");
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        Debug.Log("hellS");
                        instantiateIA(AIPersonality.Personalities.FAS);
                    }
                }
                if (Input.GetKey(KeyCode.S))
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        instantiateIA(AIPersonality.Personalities.FSA);

                    }
                }
            }
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("UP");
            IAs.Add(currentIA);
            fixIt = false;
        }
        if (fixIt) fixIA(currentIA);


        if (Input.GetKeyDown(KeyCode.D))
        {
            if (IAs.Count > 0)
            {
                GameObject firstIA = IAs[0];
                IAs.RemoveAt(0);
                Destroy(firstIA);
            }
        }*/
    }

    private void fixIA(GameObject IA)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        IA.GetComponent<AgentPositionController>().position = mouseWorldPos;

    }

    private void instantiateIA(AIPersonality.Personalities type)
    {
        currentIA = Instantiate(mockIA);
        currentIA.name = "IA";
        currentIA.name += Time.time*100;
        AIPersonality Aipers = currentIA.GetComponent<AIPersonality>();
        Aipers.configurePersonality(type);
        VisibleElements.visibleGameObjects.Add(currentIA);
fixIt = true;
    }

    private void executeCode(string Code)
    {
        Debug.Log("el codigo es : " + code);
        if (code.Length == 0)
        {
            Debug.Log("EmptyChain");
            return;
        }
        switch (code)
        {
            case "SAF":
                instantiateIA(AIPersonality.Personalities.SAF);
                break;
            case "SFA":
                instantiateIA(AIPersonality.Personalities.SFA);
                break;
            case "AFS":
                instantiateIA(AIPersonality.Personalities.AFS);
                break;
            case "ASF":
                instantiateIA(AIPersonality.Personalities.ASF);
                break;
            case "FSA":
                instantiateIA(AIPersonality.Personalities.FSA);
                break;
            case "FAS":
                instantiateIA(AIPersonality.Personalities.FAS);
                break;
            default:
                Debug.LogError("NOT A VALID COMMAND: " +code);
                break;
        }

    }
}
