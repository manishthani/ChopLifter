using UnityEngine;
using System.Collections;

public class GameHUD : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BackToMenu() {
		Application.LoadLevel(0);
	}
}
