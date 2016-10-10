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
		if (type.Equals (ItemType.Random)) {
			switch ((int)Random.Range (0, 3)) {
			case 0:
				type = ItemType.Sword;
				break;
			case 1:
				type = ItemType.BowAndArrow;
				break;
			case 2:
				type = ItemType.Wand;
				break;
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
