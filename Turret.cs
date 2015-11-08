using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	
	public float facing;
	public float arcBase;
	public float arcOfFire;
	
	public float arcMin;
	public float arcMax;
	public float modifiedArcFiringDirection;
	float relativeFacing;
	
	public float maxElevation = 30f;
	public float minElevation = -5f;
	
	public float internalElevation;
	public float elevation;
	
	public Gun[] gun;
	public int numberToFire = 2;
	
	//Yet to be used - these figures will determine just how quickly the turret can swivel to face the required direction
	public float trackingSpeed;	
	float targetAimDirection;
	
	public float internalArcBase;
	
	//These variables are supposed to help with bringing local gun direction in line with world gun rotation
	public float internalFacing;
	public float shipFacing;
	
	public GameObject ship;
	// Use this for initialization
	void Start () {
		gun = GetComponentsInChildren<Gun>();
	}
	
	public void Fire(){
		shipFacing = ship.GetComponent<Transform>().rotation.eulerAngles.y;
		
		//Firing arc limitation - to ensure that guns don't fire outside of their arc... forward batteries firing into the bridge etc.

		arcMin = - arcOfFire/2;
		arcMax = + arcOfFire/2;
				
		if(facing - arcBase >= 180) {
			if (facing - arcBase >= arcMin + 360){
				modifiedArcFiringDirection = facing;
			} else if (facing - arcBase < arcMin + 360){
				modifiedArcFiringDirection = arcBase + arcMin;
			}
		} else if (facing - arcBase < 180 && facing - arcBase >= 0) {
			if (facing - arcBase <= arcMax) {
				modifiedArcFiringDirection = facing;
			} else if (facing - arcBase > arcMax) {
				modifiedArcFiringDirection = arcBase + arcMax;
			}
		} else if (facing - arcBase < 0) {
			if (facing - arcBase + 360 >= arcMin + 360){
				modifiedArcFiringDirection = facing;
			} else if (facing - arcBase + 360 < arcMin + 360){
				modifiedArcFiringDirection = arcBase + arcMin;
			}
		}
		
		internalFacing = (modifiedArcFiringDirection + shipFacing + 270f);
				
		//Elevation limiting - to prevent Main batteries firing straight downwards etc.
		internalElevation = elevation;
		
		if(elevation >= maxElevation){
			internalElevation = maxElevation;
		} else if (elevation <= minElevation){
			internalElevation = minElevation;
		}
			
		for (int i = 0; i < numberToFire; i++){
			if (modifiedArcFiringDirection == facing){
				gun[i].Fire(internalFacing, internalElevation);
			}
		}
	}
	
	public void SetFiringDirection(float bearing){
//		facing = bearing;
		if (bearing - ship.transform.rotation.y >= 0){
			facing = bearing - ship.transform.rotation.eulerAngles.y;
		} else if (bearing - ship.transform.rotation.y < 0){
			facing = bearing + 360 - ship.transform.rotation.eulerAngles.y;
		}
	}
	
	//Not used yet - as of Oct 25th - but will be soon
	public void Rotate(float newDegrees){
		float maxArc = arcBase + (arcOfFire/2);
		float minArc = arcBase - (arcOfFire/2);
		if (newDegrees >= minArc && newDegrees <= maxArc){
			if (newDegrees <= minArc){
				targetAimDirection = minArc;
			} else if (newDegrees >= maxArc){
				targetAimDirection = maxArc;
			}
		}
	}
	
	void OnCollisionEnter (Collision coll){
		Debug.Log (coll.transform.name);
		Debug.Log (coll.relativeVelocity);
	}
		
	
	// Update is called once per frame
	void Update () {
		transform.rotation.eulerAngles.Set(transform.rotation.x, facing, transform.rotation.z);
	}
}
