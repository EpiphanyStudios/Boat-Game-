using UnityEngine;
using System.Collections;

public class Hull : MonoBehaviour {

	public float armour = 10;

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter (Collision coll){

		Debug.Log (coll.transform.name);

		float xComp = Mathf.Abs(coll.relativeVelocity.x);
		float yComp = Mathf.Abs(coll.relativeVelocity.y);
		float zComp = Mathf.Abs(coll.relativeVelocity.z);
		
		float f = Mathf.Pow (xComp, 2) + Mathf.Pow (yComp, 2) + Mathf.Pow (zComp, 2);
		
		float force = Mathf.Pow (f, (1f / 3f));

		Debug.Log (coll.relativeVelocity);

//		coll.rigidbody.AddForce (-coll.relativeVelocity);

		if (force < armour) {
			coll.gameObject.GetComponent<Shell_380mm> ().Explode ();	
		} else if (force >= armour) {
			Destroy(coll.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
