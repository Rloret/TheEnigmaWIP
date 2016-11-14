using UnityEngine;
using System.Collections;

public class ShortCutsButtons : MonoBehaviour {



    public void OnPlayButton()
    {
        GameObject.Find("FSM_GameManager").GetComponent<StateManager>().OnPlayButton();
    }

    public void OnQuitButton()
    {
        GameObject.Find("FSM_GameManager").GetComponent<StateManager>().OnQuitButton();
    }

    public void OnMenuButton()
    {
        GameObject.Find("FSM_GameManager").GetComponent<StateManager>().OnMenuButton();
    }


}
