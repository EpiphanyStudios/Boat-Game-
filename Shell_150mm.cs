using UnityEngine;
using System.Collections;

public class Shell_150mm : MonoBehaviour {

	public float shellSpeed = 1f;
	Rigidbody rigidBody;

	public GameObject explosion;
	
	float lastRotationX;
	
	float muzzleVelocity = 785f;
	
	public Vector3 velocity;
	public float drag = 1f;
	public float rotationXVis;
	
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.drag = drag;
		Fire ();
	}
	
	void Fire () {
		rigidBody.AddForce(transform.up * muzzleVelocity * shellSpeed);
		lastRotationX = 90 - (Mathf.Tan(rigidBody.velocity.y / rigidBody.velocity.z)) * Mathf.Rad2Deg;
	}

	public void Explode () {
		Instantiate (explosion, this.transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

	
	// Update is called once per frame
	void Update () {
		velocity = rigidBody.velocity;
		drag = rigidBody.drag;
		float rotationX = (Mathf.Tan(rigidBody.velocity.y / rigidBody.velocity.z)) * Mathf.Rad2Deg;
		rotationXVis = 90 - rotationX;
		float presRotate = rotationXVis - lastRotationX;
		transform.Rotate(new Vector3 (presRotate, 0f, 0f));
		lastRotationX = rotationXVis;
		
		Debug.DrawRay(transform.position, -rigidBody.velocity * Time.deltaTime / 2f, Color.yellow, 0.5f, true);
		
		if (transform.position.y <= 0 && transform.position.y >= - 0.1){
			Debug.Log (transform.position);
		}
		
		if (transform.position.y <= -1){
			Destroy(gameObject);
		}
	}
}
