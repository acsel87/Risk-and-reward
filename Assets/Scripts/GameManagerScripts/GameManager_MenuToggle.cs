using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basic_UI
{
    public class GameManager_MenuToggle : MonoBehaviour
    {
        private GameManager_Master gameManager_Master;

        public GameObject menu;

        private void Start()
        {
            //MenuToggle();
        }

        private void Update()
        {
            CheckForMenuToggleRequest();
        }

        private void OnEnable()
        {
            SetInitialReferences();
            gameManager_Master.GameOverEvent += MenuToggle;
        }

        private void OnDisable()
        {
            gameManager_Master.GameOverEvent -= MenuToggle;
        }

        private void SetInitialReferences()
        {
            gameManager_Master = GetComponent<GameManager_Master>();
        }

        void CheckForMenuToggleRequest()
        {
            if (Input.GetKeyUp(KeyCode.P) && !gameManager_Master.isGameOver)
            {
                MenuToggle();
            }
        }

        void MenuToggle()
        {
            if (menu != null)
            {
                menu.SetActive(!menu.activeSelf);
                gameManager_Master.isMenuOn = !gameManager_Master.isMenuOn;
                gameManager_Master.CallMenuToggleEvent();
            }
            else
            {
                Debug.LogWarning("Missing Menu gameobject in the Toggle Menu script.");
            }
        }
    }
}
