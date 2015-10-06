using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class JoinGame : MonoBehaviour {


	public void ConnectToGame()
    {
        Network.Connect(transform.Find("Text").GetComponent<Text>().text);
    }
}
