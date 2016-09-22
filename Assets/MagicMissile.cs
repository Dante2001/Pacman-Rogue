using UnityEngine;
using System.Collections;

public class MagicMissile : MonoBehaviour {

	public GameObject target;
	public Vector3 currentTarget;

	private Vector3 acceleration;
	private Vector3 speed;
	private float smoothTime = .5f;
	float currentTime = 0;
	// Use this for initialization
	void Start () {
		float xPosition;
		float yPosition;
		if (transform.position.x > target.transform.position.x) {
			xPosition = transform.position.x + .3f;
		} else {
			xPosition = transform.position.x - .3f;
		}
		if (transform.position.y > target.transform.position.y) {
			yPosition = transform.position.y + .3f;
		} else {
			yPosition = transform.position.y - .3f;
		}
		currentTarget = new Vector3 (xPosition,yPosition,0);
	}



	// Update is called once per frame
	void FixedUpdate () {

		currentTime += Time.fixedDeltaTime;

		float xPosition;
		float yPosition;

		if (currentTime < 1f) {
			xPosition = Mathf.SmoothDamp (transform.position.x, currentTarget.x, ref speed.x, smoothTime);
			yPosition = Mathf.SmoothDamp (transform.position.y, currentTarget.y, ref speed.y, smoothTime);
		} else {
			xPosition = Mathf.SmoothDamp (transform.position.x, target.transform.position.x, ref speed.x, smoothTime);
			yPosition = Mathf.SmoothDamp (transform.position.y, target.transform.position.y, ref speed.y, smoothTime);
		}

		transform.position = new Vector3(xPosition, yPosition, transform.position.z);


	}
}
