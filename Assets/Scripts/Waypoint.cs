using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour {

	public bool isGrounded = false;
	[Range(0.5f,2.0f)]
	public float tolerance = 1f;
	[Range(0.0f,1f)]
	public float yOffset = 0.01f;
	private RaycastHit hit;

	void OnDrawGizmos() {
		if (isGrounded) {
			if (Physics.Raycast(transform.position, -Vector3.up, out hit, tolerance)) {
				float y = hit.distance;
				Vector3 aux = transform.position;
				aux.y -= y;
				aux.y += yOffset;
				transform.position = aux;
			}
		}
		Vector3 pos = transform.position;
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(pos,3);
	}
}
