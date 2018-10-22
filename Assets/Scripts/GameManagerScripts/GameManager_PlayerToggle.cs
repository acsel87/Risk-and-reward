using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Basic_UI
{
    public class GameManager_PlayerToggle : MonoBehaviour
    {
        public List<MonoBehaviour> playerControllers;
        private GameManager_Master gameManager_Master;

        private void OnEnable()
        {
            SetInitialReferences();
            gameManager_Master.MenuToggleEvent += PlayerControllerToggle;
        }

        private void OnDisable()
        {
            gameManager_Master.MenuToggleEvent += PlayerControllerToggle;
        }

        private void SetInitialReferences()
        {
            gameManager_Master = GetComponent<GameManager_Master>();
        }

        private void PlayerControllerToggle()
        {
            if (playerControllers != null)
            {
                foreach (MonoBehaviour monoBehaviour in playerControllers)
                {
                    monoBehaviour.enabled = !monoBehaviour.enabled;
                }
            }
        }
    }    
}
