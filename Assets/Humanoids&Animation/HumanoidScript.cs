using UnityEngine;
using System.Collections;

public class HumanoidScript : MonoBehaviour {
	Animator anim; 
	bool onceWalk, onceStop, notRescuedYet;
	float walkingTime;
	public float secondsMustWalk;
	public GameObject empty;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.gameObject.transform.Translate(new Vector3 (0.0f, 0.8f,0.0f));
		onceWalk = onceStop = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Animations when survivors are in the base
		if (anim.GetBool ("notRescuedYet")) { 
			if (anim.GetBool ("helicopterLanded") && onceWalk) {
				anim.gameObject.transform.Translate (new Vector3 (0.0f, -0.8f, 0.0f));
				onceWalk = false;
				anim.SetBool ("stop", true); // Walk State activated
				walkingTime = Time.time;
				Debug.Log ("WALKING TIME: " + walkingTime);
			} else if (anim.GetBool ("stop")) {
				Debug.Log (" ENTRA EN STOP == TRUE " + walkingTime);
				if (Time.time > walkingTime + secondsMustWalk && onceStop) {
					anim.speed = 0.0f;
					//onceStop = false;
					//anim.SetBool("stop", false);
				}
			}
		}
		else {
			anim.SetBool ("stop", true);
			anim.speed = 1.0f;
			anim.gameObject.transform.LookAt(empty.transform);
			Debug.Log ("ELSE STATEMENT");
		}
	}
}
