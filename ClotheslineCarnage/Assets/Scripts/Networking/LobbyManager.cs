using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

namespace ClotheslineCarnage
{
    public class LobbyManager : NetworkLobbyManager
    {

        public MainMenu mainMenu;
        public GameLobby hostGame;
        public JoinGame joinGame;

        public void StartMatch()
        {
            
            if(Network.isServer)
                CheckReadyToBegin();
        }
        protected string targetLevel = "TestLevelScene";
        
        public static LobbyManager instance;

        public void SetGameMode(int key)
        {
            if(Network.isServer)
            {
                if (GameMode.current != null)
                    Destroy(GameMode.current);
                if (key == 1)
                {
                    GameMode.current = gameObject.AddComponent<SumoMode>();
                }
                else if (key == 2)
                {
                    GameMode.current = gameObject.AddComponent<SumoMode>(); //Note: change this to time mode later
                }
            }
        }

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);
            instance = this;
            GameMode.current = gameObject.AddComponent<SumoMode>();
            mainMenu.Show();
        }

        public override NetworkClient StartHost()
        {
            hostGame.Show();
            return base.StartHost();
        }

        public override void OnLobbyClientDisconnect(NetworkConnection conn)
        {
            if (hostGame != null && mainMenu != null)
            {
                hostGame.Hide();
                mainMenu.Show();
            }
        }

        public override void OnLobbyStopHost()
        {
            if(hostGame != null && mainMenu != null)
            {
                hostGame.Hide();
                mainMenu.Show();
            }

        }

        public override void OnLobbyStartClient(NetworkClient client)
        {
            if (matchInfo != null)
            {
                //joinGame.Show(matchInfo.address);
            }
            else
            {
                //joinGame.Show(networkAddress);
            }
        }

        public override void OnLobbyClientEnter()
        {
            hostGame.Show();
            //onlineCanvas.Show(onlineStatus);

            //exitToLobbyCanvas.Hide();

        }

        public override void OnLobbyClientExit()
        {
            if(hostGame != null)
                hostGame.Hide();
        }

        
    }
}


