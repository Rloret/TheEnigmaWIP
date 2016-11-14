using System;
using UnityEngine;
using Assets.Scripts.Interfaces;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.States
{
	public class EndGameState : IStateBase
	{
		private StateManager manager;
        private int sceneIndex;

        public EndGameState (StateManager managerRef)
		{
			manager = managerRef;
			Debug.Log ("Endgame state");
		}

        public void StateUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Space)) // 1 = GameScene - AQUI ESTABLECER SI SE EMPIEZA OTRA PARTIDA O SE VA AL MENU
            {
                SwitchScene(0);
            }
        }

        public void ShowIt(){
		}

		public void StateFixedUpdate(){
		}

        public void SwitchScene(int sceneIndex)
        {
            this.sceneIndex = sceneIndex;

            if (sceneIndex == 0) {
                manager.SwitchState(new BeginState(manager));
            } else if(sceneIndex == 1){
                manager.SwitchState(new PlayState(manager));
            }

            SceneManager.LoadScene(sceneIndex);


        }
    }
}

