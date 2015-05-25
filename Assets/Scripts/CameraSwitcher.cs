using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {

	public Camera rearCam;
	public Camera driverCam;

	// Use this for initialization
	void Start () {
		rearCam.enabled = true;
		driverCam.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.F1)) {
			rearCam.enabled = true;
			driverCam.enabled = false;
		}
		if (Input.GetKey (KeyCode.F2)) {
			rearCam.enabled = false;
			driverCam.enabled = true;
		}
	}
}
