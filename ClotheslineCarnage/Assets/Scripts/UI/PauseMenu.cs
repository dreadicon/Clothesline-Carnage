using UnityEngine;
using System.Collections;

public class PauseMenu : Menu {



	protected override void Awake () {
		base.Awake ();
		menuCanvas.enabled = false;
		GetComponent<PauseMenu> ().enabled = false;
	}

	protected override void Update () {
		base.Update ();
	}

	public override void Back () {
		
	}


	public void ReturnToMainMenu () {
		Application.LoadLevel ("MainMenu");
	}
	
}
