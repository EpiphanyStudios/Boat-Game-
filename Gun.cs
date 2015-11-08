using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public GameObject shell;
	public bool loaded = true;
	Vector3 firingOffset = new Vector3(0.2f, 0.2f, 0.2f);
	// Use this for initialization
	void Start () {
	
	}
	
	public void Fire(float direction, float elevation){
		if(loaded){
			firingOffset = new Vector3(Mathf.Sin(Mathf.Deg2Rad * direction), 0f,	Mathf.Cos(Mathf.Deg2Rad * direction));
			GameObject newShell = Instantiate(shell, this.transform.position + 0.2f * firingOffset, Quaternion.Euler(90f - elevation, direction, 0f)) as GameObject;
//			loaded = false;
//			Invoke ("Reload", 90f);
		}
	}
	
	void Reload(){
		loaded = true;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
