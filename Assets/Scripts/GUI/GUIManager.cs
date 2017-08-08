using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common
{

    public class GUIManager : MonoBehaviour
    {
        [System.Serializable]
        public struct StateToGUI
        {
            public GameState state;
            public GameObject screen;
        }

        [SerializeField]
        private List<StateToGUI> GameUI;

        private void Start()
        {
            DisableAllScreen();
            GameUI.Find(x => x.state == GameState.MENU).screen.SetActive(true);
            GameStateManager.StateChangedEvent += ChangeGUI;
        }

        private void ChangeGUI(GameState previousState)
        {
            foreach (StateToGUI ui in GameUI)
            {
                ui.screen.SetActive(ui.state == previousState);
            }
        }

        private void OnDestroy()
        {
            GameStateManager.StateChangedEvent -= ChangeGUI;
        }

        private void DisableAllScreen()
        {
            foreach (var ui in GameUI)
            {
                ui.screen.SetActive(false);
            }
        }
    }
}