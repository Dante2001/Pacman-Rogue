using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour {

	public GameObject arrow;
	float timerPerShot = .5f;
	float currentTime = 0f;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate() {
		currentTime += Time.fixedDeltaTime;
		shoot ();
		//Debug.Log (transform.rotation.x+" "+transform.rotation.y+" "+transform.rotation.z);
	}

	void shoot() {
		if (currentTime > timerPerShot) {
			currentTime = 0;
			//transform.rotation
			Instantiate(arrow,transform.position,transform.rotation);
			Debug.Log (transform.rotation.eulerAngles.x + " " + transform.rotation.eulerAngles.y + " " + transform.rotation.eulerAngles.z);
		}

	}

	// Update is called once per frame
	void Update () {
	
	}



}
