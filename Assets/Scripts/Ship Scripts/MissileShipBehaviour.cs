using UnityEngine;
using System.Collections;

public class MissileShipBehaviour : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}


	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision){
		Debug.Log ("My Name is : " + collision.gameObject.name);

		GameObject otherGameObject = collision.collider.gameObject;
		if(otherGameObject.name != "MisileTurret"){
			if(otherGameObject.name != "WaterPlane" ) Destroy (collision.collider.gameObject);
			Destroy (gameObject);
		}
	}
}
