using UnityEngine;
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

	void OnTriggerEnter(Collider collider) {
		Debug.Log("NAME: " +collider.gameObject.name);
		if (collider.gameObject.tag == "Bullet") {
			Debug.Log("ASASDASASDFASDFASDFASDF HIT!");
			health -= 5.0f;
		}
	}
}
