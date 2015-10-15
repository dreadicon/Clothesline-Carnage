using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(Canvas))]
public class Menu : MonoBehaviour {

	protected Canvas menuCanvas;

    public NetworkManager manager;

	protected virtual void Awake () {
		menuCanvas = GetComponent<Canvas> ();
        manager = FindObjectOfType<NetworkManager>();
	}

	protected virtual void Start () {

	}

	protected virtual void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			Back ();
	}


	
	public void ExitGame() {
		Application.Quit ();
	}

	public void Show() {
		menuCanvas.enabled = true;
		enabled = true;
	}

	public void Hide() {
		menuCanvas.enabled = false;
		enabled = false;
	}

	public virtual void Back () {
        Hide();
	}

}
