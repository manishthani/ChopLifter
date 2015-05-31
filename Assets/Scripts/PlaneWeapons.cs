using UnityEngine;
using System.Collections;

public class PlaneWeapons : MonoBehaviour {

	public GameObject bulletPrefab;

	public GameObject gun;
	
	private float maxDistance = 80.0f;
	private AudioSource audio;
	public float weaponFireDelay = 0.2f;

	private float weaponTimer = 0.0f;
	private ParticleEmitter particleEmmiter;


	// Use this for initialization
	void Awake () {
		audio = gun.GetComponent<AudioSource>();
		particleEmmiter = gun.GetComponent<ParticleEmitter> ();
	}
	
	// Update is called once per frame
	void Update () {
		particleEmmiter.emit = false;
		RaycastHit hit;
		if (weaponTimer > weaponFireDelay) {
			Vector3 fwd = transform.TransformDirection(Vector3.forward);
			if (Physics.Raycast(transform.position,fwd, out hit, maxDistance)) {	
				if (hit.collider.gameObject.transform.parent.gameObject.tag == "Helicopter") {
					weaponTimer = 0.0f;
					audio.Play();
					particleEmmiter.emit = true;
					Instantiate(bulletPrefab,hit.point, transform.rotation);
				}
			}
		}
		weaponTimer += Time.deltaTime;
	}
}
