﻿using UnityEngine;
using System.Collections;

public class MissileTurretBehaviour : MonoBehaviour {
	public float fireRate;
	public GameObject shotMissile;
	float nextFire;
	// Use this for initialization
	void Start () {
		nextFire = Time.time + fireRate;
	}

	void shootMissile() {
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate; 
			GameObject missiles = Instantiate (shotMissile, transform.position , transform.rotation) as GameObject;
			missiles.transform.localScale = new Vector3(0.021049f, 0.021049f, 0.021049f);

			//Debug.Log ("ANGULO : " + transform.rotation.eulerAngles.y ) ;
			//Aqui es donde se pone la velocidad y direccion donde quieres lanzar el misil, creo que hay que usar senos y cosenos para arreglarlo
			missiles.GetComponent<Rigidbody>().velocity = new Vector3 (0.0f, 30.0f, 20.0f /** Mathf.Cos(transform.rotation.eulerAngles.y)*/);
		}
	}

	// Update is called once per frame
	void Update () {
		shootMissile ();
	}

	void OnCollisionEnter(Collision collision) {
		Destroy (collision.collider.gameObject);
		Destroy (gameObject);
		Debug.Log ("My Name is : " + collision.gameObject.name);
	}
}
