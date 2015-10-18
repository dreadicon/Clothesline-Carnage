using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace ClotheslineCarnage
{
    public class Menu : MonoBehaviour
    {

        protected virtual void Awake()
        {
            
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Back();
        }

        public virtual void ExitGame()
        {
            Application.Quit();
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            enabled = true;
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            enabled = false;
        }

        public virtual void Back()
        {
            Hide();
        }

    }
}


