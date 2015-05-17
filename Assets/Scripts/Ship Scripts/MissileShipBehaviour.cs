using UnityEngine;
using System.Collections;

public class MissileShipBehaviour : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}


	// Update is called once per frame
	void Update () {

	}

	void OnColissionEnter(Collision collision){
		Debug.Log ("My Name is : " + collision.gameObject.name);
		//Destroy (collision.collider.gameObject);
		//Destroy (gameObject);
	}
}
