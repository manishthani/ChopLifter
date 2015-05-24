using UnityEngine;
using System.Collections;

public class MisileBehaviour : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision collision){
		Destroy (collision.collider.gameObject);
		Destroy (gameObject);
	}
}