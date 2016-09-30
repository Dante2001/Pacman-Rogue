using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public Text textStatsAndItems;
	public Text textGold;

	// Use this for initialization
	void Start () {
		GameManager.mGuiManager = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateStatsAndText() {
		string sword = "No sword";
		if (GameManager.inventory.Contains (ItemHelper.ItemType.Sword)) {
			sword = "Ready";
		}
		string bow = "No arrows";
		if (GameManager.inventory.Contains (ItemHelper.ItemType.BowAndArrow)) {
			bow = "Ready";
		}
		string wand = "No wand";
		if (GameManager.inventory.Contains (ItemHelper.ItemType.Wand)) {
			wand = "Ready";
		}

		textStatsAndItems.text = "Q - Sword: " + sword + "\nW - Bow: " + bow+ "\nE - Wand: " + wand;
		textGold.text = "Gold: " + GameManager.coins;

	}

}
