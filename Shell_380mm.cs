using UnityEngine;
using System.Collections;

public class Shell_380mm : MonoBehaviour {

	public float shellSpeed = 1f;
	private Vector3 cameraOffset;
	Rigidbody rigidBody;
	//public float initRotation;
	
	float lastRotationX;
	
	float muzzleVelocity = 749f;
	
	public Vector3 velocity;
	public float drag = .03f;
	public float rotationXVis;
	public float weight = 890;

	public GameObject explosion;
	
	Camera mainCamera;
	
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();	
		mainCamera = FindObjectOfType<Camera>();
		cameraOffset = mainCamera.transform.position - this.transform.position;
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

		Debug.DrawRay(transform.position, -rigidBody.velocity * Time.deltaTime * 0.9f , Color.red, 0.5f, true);
		
		if (transform.position.y <= 0 && transform.position.y >= - 0.01){
		}
		
		if (transform.position.y <= -1){
			Destroy(gameObject);
		}
	}
	
}
