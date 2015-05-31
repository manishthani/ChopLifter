using UnityEngine;
using System.Collections;

public class ShipBehaviour : MonoBehaviour {
	public float translate;
	public float rotate;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Translate(0.0f,0.0f,translate);
		transform.Rotate(0.0f,rotate, 0.0f);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Bullet") {
			Destroy (gameObject);
		}
	}

}
