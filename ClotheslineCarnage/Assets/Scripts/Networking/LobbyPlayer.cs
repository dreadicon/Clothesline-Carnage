using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

using System.Collections;
using UnityEngine.EventSystems;

namespace ClotheslineCarnage
{
    public class LobbyPlayer : NetworkLobbyPlayer
    {

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void ReadyStart()
        {
            if (readyToBegin)
                SendNotReadyToBeginMessage();
            else if (!readyToBegin)
                SendReadyToBeginMessage();
        }

        [Command]
        public void CmdExitToLobby()
        {
            var lobby = NetworkManager.singleton as LobbyManager;
            if (lobby != null)
            {
                lobby.ServerReturnToLobby();
            }
        }
    }
}


