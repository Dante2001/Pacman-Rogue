using UnityEngine;
using System.Collections;
using ItemHelper;

public class ItemOnGround : MonoBehaviour {
	
	public ItemType type;



	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag.Equals ("Player") && type != ItemType.Coin) {
			GameManager.inventory.Add (type);
		} else if (col.gameObject.tag.Equals ("Player") && type == ItemType.Coin) {
			GameManager.coins++;
		}
		GameManager.mGuiManager.updateStatsAndText ();
		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
