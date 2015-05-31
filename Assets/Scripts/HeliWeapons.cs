using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeliWeapons : MonoBehaviour {
	
	public GameObject rightGun;
	public GameObject leftGun;

	public Slider rightGunSlider;
	public Slider leftGunSlider;

	public float weaponFireDelay = 0.2f;
	
	public GameObject bulletImpactPrefab;

	public int rightWeaponBullets = 200;
	public int leftWeaponBullets = 200;

	private float leftWeaponFireTimer = 0.0f;
	private float rightWeaponFireTimer = 0.0f;

	private ParticleEmitter leftParticleEmitter;
	private ParticleEmitter rightParticleEmitter;

	private AudioSource leftAudioSource;
	private AudioSource rightAudioSource;

	private HeliMovement heliMovement;

	void Awake() {
		heliMovement = leftGun.transform.parent.transform.parent.GetComponent<HeliMovement>();
		rightGunSlider.maxValue = leftGunSlider.maxValue = rightWeaponBullets;
		rightGunSlider.value = leftGunSlider.value = leftWeaponBullets;
		leftParticleEmitter = leftGun.GetComponent<ParticleEmitter> ();
		rightParticleEmitter = rightGun.GetComponent<ParticleEmitter> ();
		leftAudioSource = leftGun.GetComponent<AudioSource> ();
		rightAudioSource = rightGun.GetComponent<AudioSource> ();
	}
	// Update is called once per frame
	void Update () {
		if (heliMovement.alive) {
			leftParticleEmitter.emit = false;
			if (Input.GetButton("Fire1") && leftWeaponFireTimer >= weaponFireDelay && leftWeaponBullets > 0) {
				leftWeaponFireTimer = 0.0f;
				leftAudioSource.Play();
				leftParticleEmitter.emit = true;
				
				RaycastHit hit = new RaycastHit();
				if (Physics.Raycast(leftGun.transform.position, leftGun.transform.forward, out hit)) {
					Instantiate(bulletImpactPrefab, hit.point,Quaternion.LookRotation(hit.normal));
				}
				--leftWeaponBullets;
				leftGunSlider.value = leftWeaponBullets;
			}
			leftWeaponFireTimer += Time.deltaTime;
			
			rightParticleEmitter.emit = false;
			if (Input.GetButton("Fire2") && rightWeaponFireTimer >= weaponFireDelay && rightWeaponBullets > 0) {
				rightWeaponFireTimer = 0.0f;
				rightAudioSource.Play();
				rightParticleEmitter.emit = true;
				
				RaycastHit hit = new RaycastHit();
				if (Physics.Raycast(rightGun.transform.position, rightGun.transform.forward, out hit)) {
					Instantiate(bulletImpactPrefab, hit.point,Quaternion.LookRotation(hit.normal));
				}
				--rightWeaponBullets;
				rightGunSlider.value = rightWeaponBullets;
			}
			rightWeaponFireTimer += Time.deltaTime;
		}
	}

}
