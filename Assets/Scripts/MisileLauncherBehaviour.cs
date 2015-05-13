using UnityEngine;
using System.Collections;

public class MisileLauncherBehaviour : MonoBehaviour {
	//TODO: Hacer que sea teledirigido y que persiga al helicoptero
	public GameObject rocket; 
	float speed = 1;
	// Use this for initialization
	void Start () {

	}


	void ActivateMisile(){
		if(speed < 100) ++speed;
		rocket.transform.Translate (new Vector3(0.0f, 0.5f, 0.5f) * speed * Time.deltaTime);
	}

	// Update is called once per frame
	void Update () {
		ActivateMisile ();
	}
}
