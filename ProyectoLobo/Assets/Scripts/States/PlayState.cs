using System;
using UnityEngine;
using Assets.Scripts.Interfaces;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.States
{
	public class PlayState : IStateBase
	{
		private StateManager manager;
        private int sceneIndex;

        public PlayState (StateManager managerRef)
		{
			manager = managerRef;
			Debug.Log ("Play state");
		}

		public void StateUpdate(){

			if (Input.GetKeyUp (KeyCode.Space)) // CUANDO TERMINE LA PARTIDA ESTABLECER CONDICION DE CAMBIO DE ESTADO AQUI
            {
                SwitchScene(2);
            }

        }

		public void ShowIt(){
		}

		public void StateFixedUpdate(){

		}

        public void SwitchScene(int sceneIndex)
        {
            this.sceneIndex = sceneIndex;
            if (sceneIndex == 2)
            {
                manager.SwitchState(new EndGameState(manager));
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}

