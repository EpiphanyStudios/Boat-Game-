using UnityEngine;
using System.Collections;

public class Bismarck : MonoBehaviour {
	
	//These inputs relate to ship's movement
	public float shipSpeed = 1f;
	public float shipHeading;
	public float courseToSteer;
	public float rudderRate = 2f;
	
	//These variable are for re-buggering around with the steering
	float internalCourseToSteer;
	float internalShipHeading;
	
	//Not used yet, but this will tell the system how high up the lookout is from sea level 
	//in order to determine how easily he/they can see targets over the horizon. 
	float lookoutHeight;
	
	//This will help determine from how far away the ship can be seen in conjunction with the 
	//lookoutHeight on the spotting ship.
	float superstructureHeight;
	
	//These variables deal with the camera's ship-following behaviour. Debugging mainly for now. 
	public bool cameraFollow = true;
	Camera mainCamera;
	Vector3 cameraOffset;
	
	GunneryControl[] gunneryControl;
	// Use this for initialization
	void Start () {
		gunneryControl = GetComponentsInChildren<GunneryControl>();
		mainCamera = FindObjectOfType<Camera>();
		cameraOffset = mainCamera.transform.position - this.transform.position;
	}
	
	void OnCollisionEnter (Collision coll){
		ContactPoint colLoc = coll.contacts[0];
		Debug.Log (colLoc.point);
		Debug.Log (colLoc.normal);
		Debug.DrawRay(colLoc.point, -100 * colLoc.normal, Color.red, 120f, true);
	}
	
	void CameraFollow(){
		mainCamera.transform.position = this.transform.position + cameraOffset;
	}
	
	void Turn (){
		
		internalShipHeading = shipHeading + 360f;
		internalCourseToSteer = courseToSteer + 360f;
		
		if (internalShipHeading - internalCourseToSteer > 180f){
			internalCourseToSteer += 360;
		} else if (internalShipHeading - internalCourseToSteer < -180f){
			internalShipHeading += 360;
		}
		
		if (internalCourseToSteer < internalShipHeading){
			transform.Rotate (0f, -rudderRate * Time.deltaTime * shipSpeed, 0f);
		}
		if (internalCourseToSteer > internalShipHeading){
			transform.Rotate (0f, rudderRate * Time.deltaTime * shipSpeed, 0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.W)){
			if (shipSpeed <= 1f){
				shipSpeed += .2f;
			}
		}
		
		if(Input.GetKeyDown (KeyCode.S)){
			if (shipSpeed >= -0.6f){
				shipSpeed -= .2f;
			}
		}
		
		if(Input.GetKeyDown(KeyCode.A)){
			courseToSteer -= 5f;
		}
		
		if(Input.GetKeyDown(KeyCode.D)){
			courseToSteer += 5f;
		}
		
		transform.position += shipSpeed * .1544f * -transform.right * Time.deltaTime;
		
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			gunneryControl[0].Fire();
			Debug.Log (gunneryControl[0].name + " has fired!");
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)){
			gunneryControl[1].Fire();
			Debug.Log (gunneryControl[1].name + " has fired!");
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)){
			gunneryControl[2].Fire();
			Debug.Log (gunneryControl[2].name + " has fired!");
		}
		shipHeading = transform.rotation.eulerAngles.y;
				
		if (shipHeading != courseToSteer){
			Turn ();
		}
		if (cameraFollow){
			CameraFollow();
		}
	}
}
