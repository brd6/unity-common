using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace Common
{
    public class GameManager : Singleton<GameManager>
    {
        public GameState currentState;

        private GameStateManager gameStateManager;

        public SoundManager SoundManager { get; private set; }

        [SerializeField]
        private GameState defaultState = GameState.MENU;

        void Start()
        {
            TryInitManagers();
            SetGameState(defaultState);
        }

        private void TryInitManagers()
        {
            gameStateManager = GetComponent<GameStateManager>();
            SoundManager = GetComponent<SoundManager>();
            if (gameStateManager == null ||
                SoundManager == null)
            {
                Debug.LogError("Init Manager fail !");
            }
        }

        public void SetGameState(GameState state)
        {
            gameStateManager.SetState(state);
        }

        public void QuitGame()
        {
            if (GameStateManager.CurrentState != defaultState)
                SetGameState(defaultState);
            else
                Application.Quit();
        }

        protected virtual void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }
        }

    }
}