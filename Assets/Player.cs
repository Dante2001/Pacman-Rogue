using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = .08f;
	public Vector3 currentDirection;

	/*void OnCollisionEnter2D(Collision2D col) {
		Debug.Log (col.collider.tag);
		if (col.collider.tag.Equals("Wall")) {
			currentDirection = Vector3.zero;
		}
		while (!col.collider.IsTouching) {

		}
	}
*/

	private bool canIGo(Vector2 direction) {
		/*if (direction.Equals (Vector2.up)) {
			Debug.Log ("Up");
		*/	
		Vector2 vectorA = new Vector2 (transform.position.x + direction.x,transform.position.y + direction.y);
		Vector2 vectorB = Vector2.zero;
		Vector2 vectorC = Vector2.zero;
		if (Mathf.Abs (direction.x) > 0) {
			vectorB = new Vector2 (transform.position.x + direction.x,transform.position.y + direction.y  - (GetComponent<BoxCollider2D>().bounds.extents.y));
			vectorC = new Vector2 (transform.position.x + direction.x,transform.position.y + direction.y  + (GetComponent<BoxCollider2D>().bounds.extents.y));
		}
		if (Mathf.Abs (direction.y) > 0) {
			vectorB = new Vector2 (transform.position.x + direction.x  - (GetComponent<BoxCollider2D>().bounds.extents.x),transform.position.y + direction.y);
			vectorC = new Vector2 (transform.position.x + direction.x  + (GetComponent<BoxCollider2D>().bounds.extents.x),transform.position.y + direction.y);
		}
		Debug.DrawLine(this.transform.position, new Vector3(transform.position.x+ direction.x,transform.position.y+direction.y,0),Color.red,.1f,false);
		Debug.DrawLine(this.transform.position, vectorB,Color.red,.1f,false);
		Debug.DrawLine(this.transform.position, vectorC,Color.red,.1f,false);

		if (Physics2D.OverlapPoint (vectorA, 1 << 8) || Physics2D.OverlapPoint (vectorB, 1 << 8) || Physics2D.OverlapPoint (vectorC, 1 << 8)) {
			//i can't go, there's a wall there

			return false;
		}
		//}

		return true;
	}

	// Use this for initialization
	void Start () {
		currentDirection = new Vector3 (speed, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.DownArrow) && canIGo(Vector2.down)) {
			currentDirection = new Vector3 (0, -speed, 0);
		} else if (Input.GetKey (KeyCode.UpArrow) && canIGo(Vector2.up)) {
			currentDirection = new Vector3 (0, speed, 0);
		} else if (Input.GetKey (KeyCode.LeftArrow) && canIGo(Vector2.left)) {
			currentDirection = new Vector3 (-speed, 0, 0);
		} else if (Input.GetKey (KeyCode.RightArrow) && canIGo(Vector2.right)) {
			currentDirection = new Vector3 (speed, 0, 0);
		}

		move (currentDirection.x,currentDirection.y,currentDirection.z);

	}

	void changeDirection(Vector3 newDirection) {

	}

	void move(float x, float y, float z) {
		if (canIGo (new Vector2(x*10, y*10))) {
			this.transform.position += new Vector3 (x, y, z);
		}
	}
}
