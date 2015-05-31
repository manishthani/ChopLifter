using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FollowPath : MonoBehaviour {

	public GameObject PathFolder;
	[Range(1.0f,50.0f)]
	public float speed = 3.0f;
	public bool closedLoop = false;
	public bool showPaths = false;
	[Range(10f,100f)]
	public int SlerpSegments = 20;
	public bool useSlerp = false;
	[Range(0.01f,10.0f)]
	public float slerpPoint = 0.1f;

	private int numpoints = 0;
	private int currindex;
	private int once = 0;
	private ArrayList purpleArray = new ArrayList();
	private float ppoint = 1.0f;
	private int ptimes  = 0;

	private Vector3[] waypoints;
	private Vector3 target;
	private Vector3 dir;
	private float disttotarget;
	private float distofline;
	private float distnormal;
	private Quaternion rot;
	private float turnam;
	private bool stop = false;
	

	// Use this for initialization
	void Start () {
	
	}
	

	// Update is called once per frame
	void Update () {
		if (!Application.isPlaying) {
			RebuildWaypointList ();	DrawWaypoints (); return;
		}
		//if(showPaths) DrawWaypoints (); // remark this out later to speed up the program
		if(once==0) RebuildWaypointList ();
		MoveForward();
		turnam=0.01f;  // for no slerping = almost at zero
		if (useSlerp)turnam=slerpPoint; 
		if (distnormal<turnam)NextWayPoint();
	}
	
	//============= subroutines =============
	
	void  MoveForward(){
		FindDistance();	
		dir = target - transform.position;

		//==========================
		if(useSlerp){
			Quaternion rot = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed/10 * Time.deltaTime);
		} else {
			transform.LookAt(target);  // no Slerping
		}
		// move in the current forward direction at specified speed:
		// to keep it grounded , use Rigidbody and check "Use Gravity".
		transform.Translate(new Vector3(0f, 0f, speed * Time.deltaTime));
	}
	
	
	void NextWayPoint(){
		/*if((currindex+1) < numpoints){  	// normal going forward
			currindex+=1;
		}else{
			currindex=0; 
		}*/
		currindex = Random.Range(0,waypoints.Length);

		Vector3 from = target - transform.position;
		Vector3 to = waypoints[currindex] - target;
		float angle = Vector3.Angle(from,to);
		int index = 0;
		while ( ((angle > 45.0f && angle < 145.0f) || Physics.Linecast(from,to)) && index < 100 ) {
			currindex = Random.Range(0,waypoints.Length);
			to = waypoints[currindex] - target;
			angle = Vector3.Angle(from,to);
			++index;
		}
		index = 0;
		// ========= if no loop then jump to beginning =========
		/*if(!closedLoop && currindex==0){
			currindex=1; target=waypoints[1];  
			// go to first waypoint and exit subroutine
			transform.position=waypoints[0]; 
			transform.LookAt(target); 
			return;
		}*/
		target=waypoints[currindex];   // else update target
	}
	
	
	void FindDistance(){
		int tempindex = currindex;
		if((tempindex-1) > -0.5){
			tempindex-=1;
		}else{
			tempindex=numpoints-1; 
		}
		distofline = Vector3.Distance(target,waypoints[tempindex]);
		disttotarget= Vector3.Distance(target,transform.position);
		distnormal= disttotarget/distofline;  // make a percentage  0.0 to 1.0
	}
	
	
	// =========================================
	void RebuildWaypointList () {
		Component[] allpoints = PathFolder.GetComponentsInChildren<Transform>();
		waypoints = new Vector3[allpoints.Length -1];
		for(int i = 1; i < allpoints.Length; i++){
			Transform aux = allpoints[i] as Transform;
			waypoints[i-1] = aux.position; 
		}
		numpoints=allpoints.Length-1;
		once=1;   // flag to do this coroutine only once
		target=waypoints[currindex];  // starts at 0
	}

	// =========================================
	void DrawWaypoints () {
		//if (Application.isPlaying)  return;
		for (int i=0; i < numpoints; i++){
			if (i<numpoints-1){  // open path  -1
				if (Physics.Linecast(waypoints[i],waypoints[i+1])) {  // true if collider
					Debug.DrawLine (waypoints[i],waypoints[i+1], Color.red);
				}else{
					Debug.DrawLine (waypoints[i],waypoints[i+1], Color.green);
				}
			}
			else if(closedLoop)	{  // closed loop
				if (Physics.Linecast(waypoints[i],waypoints[0])) {  // true if collider
					Debug.DrawLine (waypoints[i],waypoints[0], Color.red);
				}else{
					Debug.DrawLine (waypoints[i],waypoints[0], Color.blue);
				}
			}
		}
		
		if (Application.isPlaying) {  // game is running ... draw purple path
			ppoint -=Time.deltaTime;   // one second?
			
			if(ppoint<0){
				//print("ptimes1 =" + ptimes + " : purpArray ="+purpleArray);
				purpleArray.Add(this.transform.position); ptimes +=1;
				if(ptimes>SlerpSegments){
					purpleArray.RemoveAt(0);  // remove the first index
					ptimes -=1; 
				}
				//print("ptimes2 =" + ptimes + " : purpArray ="+purpleArray);
				ppoint=(1f/speed)*.75f;
			}

			
			if(ptimes<1 || !useSlerp)return;
			for (int i=0; i < ptimes-1; i++){
				//Debug.Log("parray" +i+"=" +purpleArray[i]+ " parray"+(i+1)+"=" + purpleArray[i+1]);
				Vector3 a = (Vector3) purpleArray[i]; 
				Vector3 b = (Vector3) purpleArray[i+1];
				//Debug.Log(">>" + a + b);
				Debug.DrawLine (a,b,Color.magenta);
			}
		}
	}
}