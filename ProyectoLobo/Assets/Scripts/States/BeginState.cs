using UnityEngine;
using Assets.Scripts.Interfaces;
using UnityEngine.SceneManagement;
using System;

namespace Assets.Scripts.States
{
	public class BeginState : IStateBase
	{
		private StateManager manager;
        private int sceneIndex;

		public BeginState (StateManager managerRef)
		{
			manager = managerRef;
			Debug.Log ("Begin state");
		}

		public void StateUpdate(){

		}

		public void ShowIt(){

		}

		public void StateFixedUpdate(){

		}

        public void SwitchScene(int sceneIndex)
        {
            this.sceneIndex = sceneIndex;
            if (sceneIndex == 1)
            {
                Debug.Log("Entramos al Juego");
                manager.SwitchState(new PlayState(manager));
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}

