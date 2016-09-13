using UnityEngine;
using System.Collections;
using ItemHelper;

public class ItemOnGround : MonoBehaviour {
	
	public ItemType type;



	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag.Equals ("Player")) {
			GameManager.inventory.Add (type);
			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
