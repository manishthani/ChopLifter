﻿using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	public GameObject target;
	public float damping = 5f;
	Vector3 offset;
	float rotationOffset;
	
	void Start() {
		offset = target.transform.position - transform.position;
		rotationOffset  = target.transform.rotation.eulerAngles.y;
	}
	
	void LateUpdate() {
		float currentAngle = transform.eulerAngles.y;
		float desiredAngle = target.transform.eulerAngles.y;
		float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);
		
		Quaternion rotation = Quaternion.Euler(0, desiredAngle-rotationOffset, 0);
		transform.position = target.transform.position - (rotation * offset);
		
		transform.LookAt(target.transform);
	}
}
