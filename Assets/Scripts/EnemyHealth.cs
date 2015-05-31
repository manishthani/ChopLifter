﻿using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public GameObject explosion;

	private float health = 100.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0.0f) {
			GameObject clone = Instantiate(explosion,transform.position,transform.rotation) as GameObject;
			Destroy (clone, 2);
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log("NAME: " +collision.gameObject.name);
		if (collision.gameObject.tag == "Bullet") {
			Debug.Log("ASASDASASDFASDFASDFASDF HIT!");
			health -= 5.0f;
		}
	}
}
