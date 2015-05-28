using UnityEngine;
using System.Collections;

public class MissileShipBehaviour : MonoBehaviour {

	public GameObject explosionTerrain;
	public GameObject explosionWater;
	public AudioClip audioExplosionTerrain;
	public AudioClip audioExplosionWater;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision){
		//Debug.Log ("My Name is : " + collision.gameObject.name);
		GameObject otherGameObject = collision.collider.gameObject;
		if(otherGameObject.name != "MisileTurret"){
			if(otherGameObject.name != "WaterPlane" && otherGameObject.name != "Island1Terrain" && otherGameObject.name != "MainIslandTerrain"){
				Destroy (collision.collider.gameObject);
			}
			Destroy (gameObject);

			GameObject aux; 
			float volume = 0.1f;
			if (otherGameObject.name != "WaterPlane") {
				aux = explosionTerrain; 
				AudioSource.PlayClipAtPoint (audioExplosionTerrain, transform.position, volume);
			}
			else {
				aux = explosionWater;
				AudioSource.PlayClipAtPoint (audioExplosionWater, transform.position, volume);

			}
			GameObject explosionInstance = Instantiate (aux, transform.position + new Vector3(0.0f,1.0f,0.0f), transform.rotation) as GameObject;

			//Como destruir un sistema de particulas?
			Destroy (explosionInstance, 3);
		}
	}
}
