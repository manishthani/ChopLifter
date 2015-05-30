using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public CanvasGroup main;
	public CanvasGroup options;
	public CanvasGroup about;

	bool mainMenu = true;
	bool optionsMenu = false;
	bool aboutMenu = false;
	bool escPressed = false;

	void Start() {
	}

	void OnGUI(){

		if (mainMenu) {

			if (Input.GetKey(KeyCode.Escape) && !escPressed) {
				escPressed = true;
				GetComponent<QuitScript>().DoQuit();
			}
			else if (!Input.GetKey(KeyCode.Escape)) {
				escPressed = false;
			}

		} else if (optionsMenu || aboutMenu) {
			if (Input.GetKey(KeyCode.Escape) && !escPressed) {
				escPressed = true;
				ChangeMain();
			}
			else if (!Input.GetKey(KeyCode.Escape)) {
				escPressed = false;
			}
		}
	}

	public void StartGame() {
		Application.LoadLevel(1);
	}

	public void ChangeOptions() {
		main.alpha = 0;
		main.interactable = false;
		main.blocksRaycasts = false;
		mainMenu = false;

		options.alpha = 1;
		options.interactable = true;
		options.blocksRaycasts = true;
		optionsMenu = true;
	}

	public void ChangeAbout() {
		main.alpha = 0;
		main.interactable = false;
		main.blocksRaycasts = false;
		mainMenu = false;
		
		about.alpha = 1;
		about.interactable = true;
		about.blocksRaycasts = true;
		aboutMenu = true;
	}

	public void ChangeMain() {
		options.alpha = 0;
		options.interactable = false;
		options.blocksRaycasts = false;
		about.alpha = 0;
		about.interactable = false;
		about.blocksRaycasts = false;
		aboutMenu = optionsMenu = false;

		main.alpha = 1;
		main.interactable = true;
		main.blocksRaycasts = true;
		mainMenu = true;
	}
	
}
