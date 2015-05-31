using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageAlert : MonoBehaviour {

	public Text message;
	public GameObject island1;

	public Text okMessage;

	private float speed = 2.0f;

	HeliMovement heliMovement;

	float  deadtimer = 0.0f;

	int timer = 0;

	// Use this for initialization
	void Start () {
		heliMovement = GetComponent<HeliMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (heliMovement.level == 1) {
			float dist = Vector3.Distance(transform.position, island1.transform.localPosition);
			if (dist >= 400) {
				if (deadtimer == 0.0f)
					deadtimer = Time.time;
				else
					if (Time.time - deadtimer >= 15.0f)
						heliMovement.Die();
				message.enabled = true;
				Color aux = message.color;
				aux.a = Mathf.PingPong(Time.time * speed, 1.0f);
				message.color = aux;
			}
			else {
				message.enabled = false;
				deadtimer = 0.0f;
			}
		}
		else if (timer < 100) {
			okMessage.enabled = true;
			++timer;
		}
		else {
			okMessage.enabled = false;
		}
	}
	
}
