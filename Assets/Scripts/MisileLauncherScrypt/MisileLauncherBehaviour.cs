using UnityEngine;
using System.Collections;

public class MisileLauncherBehaviour : MonoBehaviour {
	public GameObject rocket; 
	public float fireRate;
	public float maxMisileDuration;
	public GameObject explosion;
	float initialTime; 
	float nextFire;
	bool shootMisile;

	Queue missileInstances, missileInstancesTime;



	// Use this for initialization
	void Start () {
		nextFire = Time.time + fireRate;
		initialTime = Time.time;
		missileInstances = new Queue();
		missileInstancesTime = new Queue ();
		shootMisile = false;
	}


	void Update(){
		if (Time.time > nextFire) {
			missileInstancesTime.Enqueue(nextFire);
			GameObject missile = Instantiate (rocket, transform.position + new Vector3 (0,3,0) , transform.rotation) as GameObject;
			missile.transform.localScale = new Vector3(0.021049f, 0.021049f, 0.021049f);
			missileInstances.Enqueue(missile);
			GameObject helicopter = GameObject.Find ("Helicopter");
			missile.transform.LookAt(helicopter.transform);

			//Physics.IgnoreCollision(GetComponent<Collider>(), transform.parent.gameObject.GetComponent<Collider>());

			//Modificar velocidad para que parezca que le estan disparando al helicoptero
			missile.GetComponent<Rigidbody>().velocity = (helicopter.transform.position - missile.transform.position).normalized * 15f;
			nextFire = Time.time + fireRate;
		}
		if (missileInstancesTime.Count != 0) {
			float missileTime = (float)missileInstancesTime.Peek() ;
			if (Time.time - missileTime > maxMisileDuration) {
				missileInstancesTime.Dequeue();
				GameObject misileToDelete = missileInstances.Dequeue () as GameObject;
				GameObject explosionInstance = Instantiate (explosion, misileToDelete.transform.position + new Vector3(0.0f,1.0f,0.0f), misileToDelete.transform.rotation) as GameObject;
				Destroy (explosionInstance, 3);
				Destroy (misileToDelete);
			}
		}
	}
}
