using UnityEngine;
using System.Collections;

public class HumanoidScript : MonoBehaviour {
	Animator anim; 
	float initialTime;
	bool onceWalk, onceStop;
	float walkingTime;
	public float secondsMustWalk;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.gameObject.transform.Translate(new Vector3 (0.0f, 0.8f,0.0f));
		initialTime = Time.time + 5.0f;
		onceWalk = onceStop = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > initialTime && onceWalk) {
			anim.gameObject.transform.Translate(new Vector3 (0.0f, -0.8f,0.0f));
			onceWalk = false;
			anim.SetBool ("stop", true);
			walkingTime = Time.time;
			Debug.Log ("WALKING TIME: " +  walkingTime);
		}
		if (anim.GetBool ("stop")) {
			Debug.Log (" ENTRA EN STOP == TRUE " +  walkingTime);
			if(Time.time > walkingTime + secondsMustWalk && onceStop){
				anim.speed = 0.0f;
				//onceStop = false;
				//anim.SetBool("stop", false);
			}
		}
	}

	//void OnAnimatorMove(){
	//	gameObject.GetComponent<Animator>().applyRootMotion = true;
	//}
}
