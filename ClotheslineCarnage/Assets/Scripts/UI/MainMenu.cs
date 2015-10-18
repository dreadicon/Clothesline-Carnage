using UnityEngine;
using System.Collections;

namespace ClotheslineCarnage
{
    public class MainMenu : Menu
    {
        [SerializeField]
        protected JoinGame JoinGameMenu;
        [SerializeField]
        protected GameLobby GameLobbyMenu;


        protected override void Awake()
        {
            base.Awake();
            gameObject.SetActive(true);
        }

        public void HostGame()
        {
            GameLobbyMenu.HostGame();
            this.Hide();
        }
        public void JoinGame()
        {
            JoinGameMenu.Show();
            this.Hide();
        }

    }

}

