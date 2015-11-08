using UnityEngine;
using System.Collections;

public class HMS_Hood : MonoBehaviour {

	public float shipSpeed = 0f;
	GunneryControl gunneryControl;
	// Use this for initialization
	void Start () {
		gunneryControl = GetComponentInChildren<GunneryControl>();
	}
	
	void OnCollisionEnter (Collision coll){
		ContactPoint colLoc = coll.contacts[0];
//		Debug.Log (colLoc.point);
//		Debug.Log (colLoc.normal);
		Debug.DrawRay(colLoc.point, -100 * colLoc.normal, Color.red, 120f, true);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += shipSpeed * .1544f * -transform.right * Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.KeypadEnter)){
			gunneryControl.Fire ();
			Debug.Log ("KeypadEnter pressed");
		}
	}
}
