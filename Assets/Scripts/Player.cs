using UnityEngine;
using System.Collections;
using ItemHelper;

public class Player : MonoBehaviour {

	enum Orientation{UP,DOWN,LEFT,RIGHT,NONE};

	public float speed = .05f;
	public float rotatingSpeed = .5f;
	public Vector3 currentDirection;
	Orientation currentOrientation = Orientation.NONE;
	public GameObject goHand;
	public GameObject goSword;
	public GameObject goBow;
	public GameObject goMagicMissile;
	public ItemType usingItem = ItemType.Nothing;


	/*void OnCollisionEnter2D(Collision2D col) {
		Debug.Log (col.collider.tag);
		if (col.collider.tag.Equals("Wall")) {
			currentDirection = Vector3.zero;
		}
		while (!col.collider.IsTouching) {

		}
	}
*/

	public void arcaneExplosion() {
		GameObject[] goMonsterList = GameObject.FindGameObjectsWithTag ("Monster");
		for (int i = 0; i < goMonsterList.Length; i++) {
			GameObject goMissile = (GameObject)Instantiate (goMagicMissile, transform.position, transform.rotation);
			goMissile.GetComponent<MagicMissile> ().target = goMonsterList [i];

		}

	}

	private bool canIGo(Vector2 direction) {
		/*if (direction.Equals (Vector2.up)) {
			Debug.Log ("Up");
		*/	
		direction = direction * 1.2f;//1.1f;
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
		} else {
			//}

			return true;
		}
	}

	// Use this for initialization
	void Start () {
		//currentDirection = new Vector3 (speed, 0, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//CONTROLS

		//MOVEMENT
		if (Input.GetKey (KeyCode.DownArrow) && canIGo(new Vector2(0,-GetComponent<BoxCollider2D>().bounds.extents.y))) {
			currentDirection = new Vector3 (0, -speed, 0);
			currentOrientation = Orientation.DOWN;
		} else if (Input.GetKey (KeyCode.UpArrow) && canIGo(new Vector2(0,GetComponent<BoxCollider2D>().bounds.extents.y))) {
			currentDirection = new Vector3 (0, speed, 0);
			currentOrientation = Orientation.UP;
		} else if (Input.GetKey (KeyCode.LeftArrow) && canIGo(new Vector2(-GetComponent<BoxCollider2D>().bounds.extents.x, 0))) {
			currentDirection = new Vector3 (-speed, 0, 0);
			currentOrientation = Orientation.LEFT;
		} else if (Input.GetKey (KeyCode.RightArrow) && canIGo(new Vector2(GetComponent<BoxCollider2D>().bounds.extents.x, 0))) {
			currentDirection = new Vector3 (speed, 0, 0);
			currentOrientation = Orientation.RIGHT;
		}

		//USE ITEMS

		//USE SWORD
		if (Input.GetKeyDown (KeyCode.Q)) {
			//IF NOT USING SWORD AND I HAVE A SWORD, EQUIP IT
			if (!usingItem.Equals(ItemType.Sword) && GameManager.inventory.Contains (ItemHelper.ItemType.Sword)) {
				usingItem = ItemType.Sword;
				goSword.SetActive (true);
			}
			//IF AM USING A SWORD, UNEQUIP IT
			else if (usingItem.Equals(ItemType.Sword)) {
				usingItem = ItemType.Nothing;
				goSword.SetActive (false);
			}

		}

		//USE BOW
		if (Input.GetKeyDown (KeyCode.W)) {
			//IF NOT USING BOW AND I HAVE A BOW, EQUIP IT
			if (!usingItem.Equals(ItemType.BowAndArrow) && GameManager.inventory.Contains (ItemHelper.ItemType.BowAndArrow)) {
				usingItem = ItemType.BowAndArrow;
				goBow.SetActive (true);
			}
			//IF AM USING A BOW, UNEQUIP IT
			else if (usingItem.Equals(ItemType.BowAndArrow)) {
				usingItem = ItemType.Nothing;
				goBow.SetActive (false);
			}

		}

		//USE WAND
		if (Input.GetKeyDown (KeyCode.E)) {
			//IF NOT USING BOW AND I HAVE A BOW, EQUIP IT
			if (!usingItem.Equals(ItemType.Wand) && GameManager.inventory.Contains (ItemHelper.ItemType.Wand)) {
				//usingItem = ItemType.Wand;
				GameManager.inventory.Remove (ItemType.Wand);
				arcaneExplosion ();
			}

		}

		move (currentDirection.x,currentDirection.y,currentDirection.z);


		float targetAngle = 0f;
		if (currentOrientation.Equals(Orientation.UP)) {
			targetAngle = 90f;
		}
		if (currentOrientation.Equals(Orientation.LEFT)) {
			targetAngle = 180f;
		}
		if (currentOrientation.Equals(Orientation.RIGHT)) {
			targetAngle = 0f;
		}
		if (currentOrientation.Equals(Orientation.DOWN)) {
			targetAngle = 270f;
		}
		float zAngle = targetAngle;//Mathf.LerpAngle (transform.rotation.eulerAngles.z, targetAngle, rotatingSpeed);
		transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,zAngle);

		if (usingItem.Equals (ItemType.Sword)) {
			Debug.Log ("Swoosh!");
		}

	}

	void move(float x, float y, float z) {
		bool move = false;
		if (x > 0) {
			move = canIGo (new Vector2(GetComponent<BoxCollider2D>().bounds.extents.x, 0));
		}
		if (x < 0) {
			move = canIGo (new Vector2(-GetComponent<BoxCollider2D>().bounds.extents.x, 0));
		}
		if (y > 0) {
			move = canIGo (new Vector2(0,GetComponent<BoxCollider2D>().bounds.extents.y));
		}
		if (y < 0) {
			move = canIGo (new Vector2(0,-GetComponent<BoxCollider2D>().bounds.extents.y));
		}
		if (move) {
			this.transform.position += new Vector3 (x, y, z);
		}
		this.transform.position = new Vector3 (Mathf.Round(transform.position.x * 100f) / 100f,Mathf.Round(transform.position.y * 100f) / 100f,Mathf.Round(transform.position.z * 100f) / 100f);

	}
}
