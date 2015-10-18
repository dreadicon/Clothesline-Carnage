using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;


namespace ClotheslineCarnage
{
    public class JoinGame : Menu
    {
        string directConnectAddress = "127.0.0.1";
        string directConnectPort = "8001";

        public void SetDirectConnect(string address)
        {
            directConnectAddress = address;
        }
        public void ConnectToGame()
        {
            LobbyManager.instance.networkAddress = directConnectAddress;
            LobbyManager.instance.StartClient();
        }
    }
}


