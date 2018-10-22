using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basic_UI
{
    public class GameManager_PauseToggle : MonoBehaviour
    {
        private GameManager_Master gameManager_Master;
        private bool isPaused;

        private void OnEnable()
        {
            SetInitialReferences();
            gameManager_Master.MenuToggleEvent += PauseToggle;            
        }

        private void OnDisable()
        {
            gameManager_Master.MenuToggleEvent -= PauseToggle;
        }

        private void SetInitialReferences()
        {
            gameManager_Master = GetComponent<GameManager_Master>();
        }

        void PauseToggle()
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }
    }
}
