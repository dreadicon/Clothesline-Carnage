using UnityEngine;
using System;
using System.Collections.Generic;

namespace ClotheslineCarnage
{
    public class GameLobby : Menu
    {
        public void StartGame()
        {
            if (Network.isServer)
                LobbyManager.instance.StartMatch();
        }

        public void HostGame()
        {
            this.Show();
            LobbyManager.instance.StartHost();
        }

        public void SetMode(int mode)
        {
            LobbyManager.instance.SetGameMode(mode);
        }
    }
}