using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	public CanvasGroup menu;

	bool isPaused = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)) {
			isPaused = !isPaused;
			if (isPaused) {
				Time.timeScale = 0;
				Cursor.lockState = CursorLockMode.None;
				menu.alpha = 1;
			} else {
				Time.timeScale = 1;
				Cursor.lockState = CursorLockMode.Locked;
				menu.alpha = 0;
			}
		}

	}

	public void BackToMenu() {
		Application.LoadLevel(0);
	}

	public void Restart() {
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Cancel() {
		isPaused = false;
	}

	void OnGUI () {
		
		//GUI.skin = guiSkin;
		
		if(isPaused){
			
			   
			
			if (GUI.Button (new Rect (Screen.width/2 - 100,Screen.height/2 - 120, 200, 100), "Menu")) {
				BackToMenu();
			}
			
			if (GUI.Button (new Rect (Screen.width/2 - 100,Screen.height/2,200,100), "Restart")) {
				Restart();
			}
			
			if (GUI.Button (new Rect (Screen.width/2 - 100,Screen.height/2 + 120,200,100), "Cancel")) {
				Cancel ();
			}
		}
	}
}
