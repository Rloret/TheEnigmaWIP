using UnityEngine;
using System.Collections;
using Assets.Scripts.States;
using Assets.Scripts.Interfaces;

public class StateManager : MonoBehaviour {

    private const int MENUSCENE = 0;
    private const int PLAYSCENE = 1;
    private const int GAMEOVERSCENE = 2;

    private IStateBase activeState;
	private static StateManager instanceRef;


	void Awake(){
        //Este trozo es para que el gameObject que contiene este script (que deberia ser el gamemanager) no se destruya en la transición de escenas.
        if (instanceRef == null) {
			instanceRef = this;
			DontDestroyOnLoad (gameObject);
		} else {
			DestroyImmediate (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		activeState = new BeginState(this);
		//Debug.Log("El estado activo es: " + activeState);
	}
	
	// Update is called once per frame
	void Update () {
		if (activeState != null) {
			activeState.StateUpdate ();
		}
	}

	void OnGUI(){
		if (activeState != null)
			activeState.ShowIt ();
	}

	public void SwitchState(IStateBase newState){
		activeState = newState;
	}

    public void OnPlayButton() {
        Debug.Log("PULSAMOS JUGAR");
        activeState.SwitchScene(PLAYSCENE);
    }

    public void OnQuitButton() {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void OnMenuButton() {
        activeState.SwitchScene(MENUSCENE);
    }

}
