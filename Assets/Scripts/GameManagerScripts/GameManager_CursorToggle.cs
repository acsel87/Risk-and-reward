using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basic_UI
{
    public class GameManager_CursorToggle : MonoBehaviour
    {
        private GameManager_Master gameManager_Master;

        [SerializeField]
        private GameObject inGameCursor;       

        private void Start()
        {
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            SetInitialReferences();
            gameManager_Master.MenuToggleEvent += CursorToggleState;
        }

        private void OnDisable()
        {
            gameManager_Master.MenuToggleEvent -= CursorToggleState;
        }

        private void SetInitialReferences()
        {
            gameManager_Master = GetComponent<GameManager_Master>();
        }

        void CursorToggleState()
        {
            Cursor.visible = !Cursor.visible;

            if (inGameCursor != null)
            {                
                inGameCursor.SetActive(!inGameCursor.activeSelf);
            }
        }
    }
}
