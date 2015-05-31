using UnityEngine;
using System.Collections;

public class MisileBehaviour : MonoBehaviour {
	public GameObject explosion;
	public AudioClip audioExplosion;
	public float volume;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter (Collider other) {
		//Debug.Log ("Se destruye por : " + other.gameObject.name);
		AudioSource.PlayClipAtPoint (audioExplosion, transform.position, volume);
		Transform parent = other.gameObject.transform.parent;
		if (parent == null) {
			if (other.gameObject.name != "MisileLauncher" && other.gameObject.name != "Rocket(Clone)"  && other.gameObject.name != "Rocket") {
				GameObject explosionInstance = Instantiate (explosion, transform.position + new Vector3 (0.0f, 1.0f, 0.0f), transform.rotation) as GameObject;
				Destroy (explosionInstance, 3);
				Destroy (gameObject);
			}
			if (other.gameObject.name == "Navyship" || other.gameObject.name == "Plane") {
				Destroy (other.gameObject);
			}
		}
		
	}
}