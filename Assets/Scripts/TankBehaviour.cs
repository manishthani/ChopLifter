using UnityEngine;
using System.Collections;

public class TankBehaviour : MonoBehaviour {
	public GameObject turret;
	public GameObject gun;
	public float turnSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Aplica rotacion a la torreta del tanque y a la metralladora
		turret.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
		gun.transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
		//Aplica traslacion al objeto en general
		//transform.Translate (0.05f, 0.0f, 0.0f);

		// Para activar / desactivar el objeto
		//gameObject.SetActive (false);
	}
}
