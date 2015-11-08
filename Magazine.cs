using UnityEngine;
using System.Collections;

public class Magazine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter (Collision coll){
		Debug.Log (name + " was hit by " + coll.transform.name);
		Debug.Log (coll.relativeVelocity);
		Debug.Log (coll.rigidbody.mass);

		float xComp = Mathf.Abs(coll.relativeVelocity.x);
        float yComp = Mathf.Abs(coll.relativeVelocity.y);
		float zComp = Mathf.Abs(coll.relativeVelocity.z);

		float f = Mathf.Pow (xComp, 2) + Mathf.Pow (yComp, 2) + Mathf.Pow (zComp, 2);

		float force = Mathf.Pow (f, (1f / 3f));

		Debug.Log (f);
		Debug.Log (force);

		Destroy (coll.gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
