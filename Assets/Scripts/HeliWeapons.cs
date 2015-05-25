using UnityEngine;
using System.Collections;

public class HeliWeapons : MonoBehaviour {
	
	public GameObject rightGun;
	public GameObject leftGun;
	
	float weaponFireDelay = 0.2f;
	
	public GameObject bulletImpactPrefab;
	
	private float leftWeaponFireTimer = 0.0f;
	private float rightWeaponFireTimer = 0.0f;
	
	public int rightWeaponBullets = 400;
	public int leftWeaponBullets = 400;
	
	// Update is called once per frame
	void Update () {
		leftGun.GetComponent<ParticleEmitter>().emit = false;
		if (Input.GetButton("Fire1") && leftWeaponFireTimer >= weaponFireDelay) {
			leftWeaponFireTimer = 0.0f;
			leftGun.GetComponent<AudioSource>().Play();
			leftGun.GetComponent<ParticleEmitter>().emit = true;
			
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(leftGun.transform.position, leftGun.transform.forward, out hit)) {
				Instantiate(bulletImpactPrefab, hit.point,Quaternion.LookRotation(hit.normal));
			}
			--leftWeaponBullets;
		}
		leftWeaponFireTimer += Time.deltaTime;
		
		rightGun.GetComponent<ParticleEmitter> ().emit = false;
		if (Input.GetButton("Fire2") && rightWeaponFireTimer >= weaponFireDelay) {
			rightWeaponFireTimer = 0.0f;
			rightGun.GetComponent<AudioSource>().Play();
			rightGun.GetComponent<ParticleEmitter>().emit = true;
			
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(rightGun.transform.position, rightGun.transform.forward, out hit)) {
				Instantiate(bulletImpactPrefab, hit.point,Quaternion.LookRotation(hit.normal));
			}
			--rightWeaponBullets;
		}
		rightWeaponFireTimer += Time.deltaTime;
	}
}
