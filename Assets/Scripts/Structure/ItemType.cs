using UnityEngine;
using System.Collections;
using ItemHelper;

public class ItemOnInventory {
	public ItemType mItemType;
	public float timeDuration;
	public float timeStartUse;

}

namespace ItemHelper {
	public enum ItemType {Sword,BowAndArrow,Wand};

}

