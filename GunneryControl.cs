using UnityEngine;
using System.Collections;

public class GunneryControl : MonoBehaviour {

	public GameObject target;

	private Turret[] turret;

	//temp variables for testing!
	public float elevation;

	public float range;
	public float bearing;


	// Use this for initialization
	void Start () {
		turret = GetComponentsInChildren<Turret>();
	}
	
	void FindRangeAndBearingToTarget(){
		float xDiff = target.transform.position.x - this.transform.position.x;
		float zDiff = target.transform.position.z - this.transform.position.z;
		
		//This part works out the range from the x and z components of the distance between this vessel and the target.
		float xDist = Mathf.Pow(Mathf.Abs (xDiff), 2);
		float zDist = Mathf.Pow(Mathf.Abs (zDiff), 2);
		range = Mathf.Sqrt(xDist + zDist);
		
		//This part will work out the bearing from the same elements. 
		bearing = Mathf.Atan2(xDiff, zDiff) * Mathf.Rad2Deg;

		//based on range to target we can give an estimate for elevation, but this part will have to be changed!
		//TODO make this bit more sensible later...

	}
	
	public void Fire(){
		for (int i = 0; i < turret.Length; i++){
			if(target){
				turret[i].SetFiringDirection(bearing + 90);
			}
			turret[i].Fire();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target){
			FindRangeAndBearingToTarget();
		}
	}
}
