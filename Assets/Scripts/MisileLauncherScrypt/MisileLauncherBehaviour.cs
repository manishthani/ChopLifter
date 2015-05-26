using UnityEngine;
using System.Collections;

public class MisileLauncherBehaviour : MonoBehaviour {
	public GameObject rocket; 
	public float fireRate;
	public float maxMisileDuration;
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
			Debug.Log ("MISIIILLLL");
			GameObject missile = Instantiate (rocket, transform.position  + new Vector3(0.0f,10.0f,0.0f), transform.rotation) as GameObject;
			missile.transform.localScale = new Vector3(0.021049f, 0.021049f, 0.021049f);
			missileInstances.Enqueue(missile);
			missile.GetComponent<Rigidbody>().velocity = missile.transform.TransformDirection(new Vector3 (0.0f, 10.0f, 25.0f));
			nextFire = Time.time + fireRate;
		}
		if (missileInstancesTime.Count != 0) {
			float missileTime = (float)missileInstancesTime.Peek() ;
			if (Time.time - missileTime > maxMisileDuration) {
				missileInstancesTime.Dequeue();
				GameObject misileToDelete = missileInstances.Dequeue () as GameObject;
				Destroy (misileToDelete);
			}
		}
	}
}
