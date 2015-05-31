using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HeliMovement : MonoBehaviour {

	public GameObject explosion;
	
	public GameObject mainRotor;
	public GameObject rearRotor;

	public Slider healthSlider;
	public Text scoreText;
	
	public float maxRotorForce = 22241.1081f;				//Força en Newtons
	public static float maxRotorVelocity = 4000f;			//Graus per segon
	private float rotorVelocity = 0.0f;						//Valor entre 0 i 1
	private float rotorRotation = 0.0f;						//Graus -> utilitzat per les animacions
	
	public float maxRearRotorForce = 15000.0f;				//Força en Newtons
	public float maxRearRotorVelocity = 2200.0f;			//Graus per segon
	private float rearRotorVelocity = 0.0f;					//Valor entre 0 i 1
	private float rearRotorRotation = 0.0f;					//Graus -> utilizat per les animacions
	
	public float fwdRotorTorqueMultiplier = 0.5f;			//Multiplicador per controlar la sensibilitat del Input
	public float sidewaysRotorTorqueMultiplier = 0.5f;		//Multiplicador per controlar la sensibilitat del Input
	
	public float damping = 5.0f;
	public float horizontalSensibility = 3.0f;
	public int health = 100;

	static bool mainRotorActive = true;	
	static bool rearRotorActive = true;
	
	float mouseX = 0f;
	float mouseY = 0f;
	
	float midX;
	float midY;


	Stack survivorsOnBoard;
	float timeBetweenSurvivor;
	
	float midZone = 5.0f;

	public float level = 1;

	public bool alive;

	int rescued= 0;

	int score = 0;

	void Start() {
		midX = Screen.width / 2;
		midY = Screen.height / 2;
		Cursor.visible = false;
		survivorsOnBoard = new Stack();
		timeBetweenSurvivor = 0.0f;
		alive = true;
		scoreText.text = "Score: " + score;
	}
	
	
	//FixedUpdate s'executa cada volta del bucle de fisica. No depen del framerate. Es el millor lloc per aplicar les forces
	void FixedUpdate() {
		if (alive) {
			//Força final que s'aplicara a l'helicopter
			Vector3 torqueValue = new Vector3 (0, 0, 0); 
			//Calcul de la força per la inclinacio de l'helicopter
			Vector3 controlTorque = new Vector3(-mouseY/10* fwdRotorTorqueMultiplier, 1.0f, mouseX/10 * sidewaysRotorTorqueMultiplier);
			
			//Si el motor principal esta actiu, se suma la forca al Torque Value i s'aplica al Rigid Body de l'helicopter
			if ( mainRotorActive == true ) {
				torqueValue += (controlTorque * maxRotorForce * rotorVelocity);
				
				GetComponent<Rigidbody>().AddRelativeForce( Vector3.up * maxRotorForce * rotorVelocity);
				
				if ( Vector3.Angle( Vector3.up, transform.up ) < 60 ) {
					transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.Euler( 0, transform.rotation.eulerAngles.y, 0 ), Time.deltaTime * rotorVelocity * 2);
				}
			}
			
			if ( rearRotorActive == true ) {
				torqueValue -= (Vector3.up * maxRearRotorForce * rearRotorVelocity);
			}
			
			GetComponent<Rigidbody>().AddRelativeTorque( torqueValue );
		}
	}
	
	void Update() {
		if (alive) {
			mouseX = (Input.mousePosition.x - midX)/Screen.width*100;
			mouseY = (Input.mousePosition.y - midY)/Screen.height*100;
			
			if (Mathf.Abs (mouseX) < midZone) {
				mouseX = 0.0f;
			} else {
				if (mouseX < 0){
					mouseX = Mathf.Max(mouseX+midZone, -20.0f);
				} else {
					mouseX = Mathf.Min(mouseX-midZone, 20.0f);
				}
			}
			
			
			if (Mathf.Abs (mouseY) < midZone) {
				mouseY = 0.0f;
			} else {
				if (mouseY < 0){
					mouseY = Mathf.Max(mouseY+midZone, -30.0f);
				} else {
					mouseY = Mathf.Min(mouseY-midZone, 30.0f);
				}
			}

			GetComponent<AudioSource>().pitch = rotorVelocity * 1.5f;
			
			if ( mainRotorActive == true ) {
				mainRotor.transform.rotation = transform.rotation * Quaternion.Euler( 0, rotorRotation, 180 );
			}
			if ( rearRotorActive == true ) {
				rearRotor.transform.rotation = transform.rotation * Quaternion.Euler( rearRotorRotation, 0, 90 );
			}
			
			rotorRotation += maxRotorVelocity * rotorVelocity/2 * Time.deltaTime;
			rearRotorRotation += maxRearRotorVelocity * rotorVelocity * Time.deltaTime;
			
			
			if (rotorVelocity > 0.5f) {
				damping = 100.0f;
			}
			
			if (rotorVelocity < 0.2f) {
				damping = 5.0f;
			}
			
			if (rotorVelocity <= 0.1f) {
				Cursor.lockState = CursorLockMode.Locked;
			} else {
				Cursor.lockState = CursorLockMode.None;
			}
			
			float hover_Rotor_Velocity = (GetComponent<Rigidbody>().mass * Mathf.Abs (Physics.gravity.y) / maxRotorForce);
			float hover_Tail_Rotor_Velocity = (maxRotorForce * rotorVelocity) / maxRearRotorForce;
			
			if ( Input.GetAxis( "Vertical" ) != 0.0 ) {
				rotorVelocity += Input.GetAxis( "Vertical" ) * 0.001f;
			}else{
				rotorVelocity = Mathf.Lerp( rotorVelocity, hover_Rotor_Velocity, Time.deltaTime * Time.deltaTime * damping);
			}
			rearRotorVelocity = hover_Tail_Rotor_Velocity - Input.GetAxis ("Horizontal");
			
			if ( rotorVelocity > 1.0 ) {
				rotorVelocity = 1.0f;
			}else if ( rotorVelocity < 0.0) {
				rotorVelocity = 0.0f;
			}
			if (health <= 0) {
				alive = false;
				Cursor.lockState = CursorLockMode.None; 
			}
			if (level == 1 && rescued > 2) level++;
			scoreText.text = "Score: " + score;
			if (rescued >= 21)
				Application.LoadLevel(3);
		}
		else {
			Application.LoadLevel(2);
		}
	}
	void OnCollisionEnter (Collision other) {
		GameObject objectCollided = other.gameObject;
		if (objectCollided.tag == "Water") {
			//Debug.Log ("SIZE SURVIVORS: " + survivorsOnBoard.Count);
			health -= 15;
			healthSlider.value = health;
		} else if (objectCollided.tag == "Rocket") {
			health -= 30;
			healthSlider.value= health;
			GameObject aux = Instantiate(explosion,transform.position,transform.rotation) as GameObject	;
			Destroy(aux,2);
			Destroy(objectCollided);
		} else if (objectCollided.tag == "Bullet") {
			health -= 10;
			healthSlider.value = health;
		}
	}

	void OnTriggerEnter (Collider other) {
		//Debug.Log ("ONTRIGGER ENTER" + other.gameObject.name + " TAG: " + other.gameObject.tag);
		GameObject objectCollided = other.gameObject;
		//Si encuentra un superviviente y no ha sido rescatado aun, entonces lo salvamos
		if (objectCollided.tag == "Survivor" && objectCollided.GetComponent<Animator> ().GetBool ("notRescuedYet")) {
			survivorsOnBoard.Push (objectCollided);
			objectCollided.SetActive (false);
			//Destroy (objectCollided);
		} else if (objectCollided.name == "RescuePlatform") {
			health += 30;
			if (health > 100) health = 100;
			healthSlider.value = health;
			while (survivorsOnBoard.Count != 0) {
				timeBetweenSurvivor = Time.time;
				GameObject aux = survivorsOnBoard.Pop () as GameObject;
				Animator anim = aux.GetComponent<Animator> ();
				aux.transform.position = new Vector3 (transform.position.x + (survivorsOnBoard.Count % 3) * 1.25f, objectCollided.transform.position.y - 0.2f, transform.position.z);
				aux.SetActive (true);
				anim.SetBool ("notRescuedYet", false);
				++rescued;
				score += Random.Range(50, 100);
			}
		} else if (objectCollided.name.Split ('-') [0] == "superficie") {
			//Debug.Log (objectCollided.transform.parent.name);
			GameObject michaelSurvivor = GameObject.Find ("Michael-" + objectCollided.name.Split ('-') [1]);
			GameObject adamSurvivor = GameObject.Find ("Adam-" + objectCollided.name.Split ('-') [1]);
			GameObject victoriaSurvivor = GameObject.Find ("Victoria-" + objectCollided.name.Split ('-') [1]);
			if (michaelSurvivor != null)
				michaelSurvivor.GetComponent<Animator> ().SetBool ("helicopterLanded", true);
			if (adamSurvivor != null)
				adamSurvivor.GetComponent<Animator> ().SetBool ("helicopterLanded", true);
			if (victoriaSurvivor != null)
				victoriaSurvivor.GetComponent<Animator> ().SetBool ("helicopterLanded", true);
		
		}
	}

	public void AddPunctuation(int value) {
		score += value;
	}

	public void Die() {
		alive = false;
	}
}
