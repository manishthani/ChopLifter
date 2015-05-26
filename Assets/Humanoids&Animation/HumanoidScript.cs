using UnityEngine;
using System.Collections;

public class HumanoidScript : MonoBehaviour {
	Animator anim; 
	float initialTime;
	bool once;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		initialTime = Time.time + 10.0f;
		once = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > initialTime && once) {
			once = false;
			Debug.Log ("ENTRA EN EL IF DE MICHAEL");
			anim.SetBool ("stop", true);
		}
	}

	//void OnAnimatorMove(){
	//	gameObject.GetComponent<Animator>().applyRootMotion = true;
	//}
}
