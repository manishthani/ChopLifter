using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public GUIStyle customButton;

	void OnGUI(){

		if (GUI.Button(new Rect (Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Start Game", customButton)) {
			Application.LoadLevel(1);
		}

		if (GUI.Button(new Rect (Screen.width / 2 - 100, Screen.height / 2  + 200, 200, 50), "Exit", customButton)) {
			Application.Quit();
		}
	}
}
