using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public float speed = .004f;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate() {
		Vector3 move = new Vector3(Mathf.Cos(transform.eulerAngles.z*Mathf.Deg2Rad)*speed ,Mathf.Sin(transform.eulerAngles.z*Mathf.Deg2Rad)*speed , 0);
		//Debug.Log (move.x+" "+move.y + " " + move.z);
		//Debug.Log (Mathf.Cos (transform.eulerAngles.z * Mathf.Deg2Rad));
		//Debug.Log (Mathf.Sin(transform.eulerAngles.z*Mathf.Deg2Rad));
		transform.position = new Vector3(transform.position.x + Mathf.Cos(transform.eulerAngles.z*Mathf.Deg2Rad)*speed ,transform.position.y + Mathf.Sin(transform.eulerAngles.z*Mathf.Deg2Rad)*speed , 0);

	}

	// Update is called once per frame
	void Update () {
	
	}
}
