using UnityEngine;
using System.Collections;

public class HumanoidScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Animator>().applyRootMotion = true;
	}

	void OnAnimatorMove(){

	}
}
