using UnityEngine;
using System.Collections;

public class cameraTracker : MonoBehaviour {

	Camera MainCamera;
	
	// Use this for initialization
	void Start () {
		MainCamera = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
//		float posX = MainCamera.transform.position.x;
//		float posZ = MainCamera.transform.position.z;
//		transform.position = new Vector3(posX, 0f, posZ);
	}
}
