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
		if (other.gameObject.name != "MisileLauncher-0" && other.gameObject.name != "Rocket" && other.gameObject.name != "Rocket(Clone)" && other.gameObject.name != "Island1Terrain" && other.gameObject.name != "Aircraft carrier 1" ) {
			Debug.Log ("Se destruye por : " + other.gameObject.name);
			AudioSource.PlayClipAtPoint (audioExplosion, transform.position, volume);
			GameObject explosionInstance = Instantiate (explosion, transform.position + new Vector3(0.0f,1.0f,0.0f), transform.rotation) as GameObject;
			Destroy (explosionInstance, 3);
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}