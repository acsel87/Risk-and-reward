using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Basic_UI
    {
    public class GameManager_RestartLevel : MonoBehaviour
    {
        private GameManager_Master gameManager_Master;
        
        private void OnEnable()
        {
            SetInitialReferences();
            gameManager_Master.RestartLevelEvent += RestartLevel;
        }

        private void OnDisable()
        {
            gameManager_Master.RestartLevelEvent -= RestartLevel;
        }

        private void SetInitialReferences()
        {
            gameManager_Master = GetComponent<GameManager_Master>();
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
