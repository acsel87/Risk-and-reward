using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basic_UI
{
    public class GameManager_Master : MonoBehaviour
    {
        public delegate void GameManagerEventHandler();
        public event GameManagerEventHandler MenuToggleEvent;
        public event GameManagerEventHandler RestartLevelEvent;
        public event GameManagerEventHandler GoToMenuEvent;
        public event GameManagerEventHandler GameOverEvent;

        public bool isGameOver;
        public bool isInventoryUIOn;
        public bool isMenuOn;

        private void Start()
        {
            if (!isMenuOn)
            {
                Time.timeScale = 1f;
            }
        }

        public void CallMenuToggleEvent()
        {
            if (MenuToggleEvent != null)
            {
                MenuToggleEvent();
            }
        }

        public void CallRestartLevelEvent()
        {
            if (RestartLevelEvent != null)
            {
                RestartLevelEvent();
            }
        }

        public void CallGoToMenuEvent()
        {
            if (GoToMenuEvent != null)
            {
                GoToMenuEvent();
            }
        }

        public void CallGameOverEvent()
        {
            if (GameOverEvent != null)
            {
                isGameOver = true;
                GameOverEvent();
            }
        }
    }
}
