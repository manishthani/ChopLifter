using UnityEngine;
using System.Collections;

public class HeliMovement : MonoBehaviour {
	
	public GameObject mainRotor;
	public GameObject rearRotor;
	
	public float maxRotorForce = 22241.1081f;				//Força en Newtons
	public static float maxRotorVelocity = 3600f;			//Graus per segon
	private float rotorVelocity = 0.0f;						//Valor entre 0 i 1
	private float rotorRotation = 0.0f;						//Graus -> utilitzat per les animacions
	
	public float maxRearRotorForce = 15000.0f;				//Força en Newtons
	public float maxRearRotorVelocity = 2200.0f;			//Graus per segon
	private float rearRotorVelocity = 0.0f;					//Valor entre 0 i 1
	private float rearRotorRotation = 0.0f;					//Graus -> utilizat per les animacions
	
	public float fwdRotorTorqueMultiplier = 5f;			//Multiplicador per controlar la sensibilitat del Input
	public float sidewaysRotorTorqueMultiplier = 0.5f;		//Multiplicador per controlar la sensibilitat del Input
	
	public float damping = 5.0f;
	public float horizontalSensibility = 1.0f;
	
	static bool mainRotorActive = true;	
	static bool rearRotorActive = true;

	float mouseX;
	float mouseY;

	void Start() {
	}


	//FixedUpdate s'executa cada volta del bucle de fisica. No depen del framerate. Es el millor lloc per aplicar les forces
	void FixedUpdate() {

		//Força final que s'aplicara a l'helicopter
		Vector3 torqueValue = new Vector3 (0, 0, 0); 
		//Calcul de la força per la inclinacio de l'helicopter
		Vector3 controlTorque = new Vector3(-mouseY* fwdRotorTorqueMultiplier, 1.0f, mouseX * sidewaysRotorTorqueMultiplier);
		
		//Si el motor principal esta actiu, se suma la forca al Torque Value i s'aplica al Rigid Body de l'helicopter
		if ( mainRotorActive == true ) {
			torqueValue += (controlTorque * maxRotorForce * rotorVelocity);
			
			GetComponent<Rigidbody>().AddRelativeForce( Vector3.up * maxRotorForce * rotorVelocity);
			
			if ( Vector3.Angle( Vector3.up, transform.up ) < 80 ) {
				transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.Euler( 0, transform.rotation.eulerAngles.y, 0 ), Time.deltaTime * rotorVelocity * 4 );
			}
		}
		
		if ( rearRotorActive == true ) {
			torqueValue -= (Vector3.up * maxRearRotorForce * rearRotorVelocity);
		}
		
		GetComponent<Rigidbody>().AddRelativeTorque( torqueValue );
	}
	
	void Update() {

		GetComponent<AudioSource>().pitch = rotorVelocity * 1.5f;

		mouseX = Input.GetAxis ("Mouse X");
		mouseY = Input.GetAxis ("Mouse Y");

		if ( mainRotorActive == true ) {
			mainRotor.transform.rotation = transform.rotation * Quaternion.Euler( 0, -rotorRotation, 180 );
		}
		if ( rearRotorActive == true ) {
			rearRotor.transform.rotation = transform.rotation * Quaternion.Euler( rearRotorRotation, 0, 90 );
		}
		
		rotorRotation += maxRotorVelocity * rotorVelocity/2 * Time.deltaTime;
		rearRotorRotation += maxRearRotorVelocity * rotorVelocity * Time.deltaTime;

		//Debug.Log ("MouseX: " + mouseX + ", mouseY: " + mouseY);

		if (rotorVelocity > 0.5f) {
			damping = 100.0f;
		}

		if (rotorVelocity < 0.2f) {
			damping = 5.0f;
		}
		
		float hover_Rotor_Velocity = (GetComponent<Rigidbody>().mass * Mathf.Abs (Physics.gravity.y) / maxRotorForce);
		float hover_Tail_Rotor_Velocity = (maxRotorForce * rotorVelocity) / maxRearRotorForce;
		
		if ( Input.GetAxis( "Vertical2" ) != 0.0 ) {
			rotorVelocity += Input.GetAxis( "Vertical2" ) * 0.001f;
		}else{
			rotorVelocity = Mathf.Lerp( rotorVelocity, hover_Rotor_Velocity, Time.deltaTime * Time.deltaTime * damping);
		}
		rearRotorVelocity = hover_Tail_Rotor_Velocity - Input.GetAxis ("Horizontal2") * horizontalSensibility;
		
		if ( rotorVelocity > 1.0 ) {
			rotorVelocity = 1.0f;
		}else if ( rotorVelocity < 0.0) {
			rotorVelocity = 0.0f;
		}
	}
	void OnTriggerEnter (Collider other) {
		//Aqui controlaremos que el helicoptero no pueda ir al mapa grande sin pasarse el pequeno
		Debug.Log ("I AM HELI COLLIDING WITH SPHERE!");
	}
	
}
