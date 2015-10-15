using UnityEngine;
using System.Collections;

namespace ClotheslineCarnage
{
    public class MainMenu : Menu
    {
        [SerializeField]
        protected JoinGame JoinGameMenu;
        [SerializeField]
        protected HostGame HostGameMenu;


        protected override void Awake()
        {
            base.Awake();

        }

        public void HostGame()
        {
            manager.StartHost();
            Time.timeScale = 1.0f;

        }
        public void JoinGame()
        {

        }

    }

}

