using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
	public Item itemRef = null;
	public Image itemImage = null;
	public Button slotButton = null;
	public uint stackSize = 0;
	public float itemDurability= 0f;

	void Awake() {
		this.slotButton = gameObject.GetComponentInChildren<Button>();
		this.itemImage = transform.GetChild(1).gameObject.GetComponent<Image>();
		this.slotButton.onClick.AddListener(() => Inventory.instance.PickFromSlot());
		this.itemImage.enabled = false;
		try {
			this.itemDurability = itemRef.durability;
		} catch(System.Exception) {

		}
	}

	public void Add(Item item, uint amount) {
		this.itemRef = item;
		try {
			this.itemImage.sprite = item.itemSprite;
		} catch (System.Exception) {
			Debug.Log("sum ting wong");
		}
		this.stackSize += amount;
		this.itemImage.enabled = true;
	}

	public void Clear() {
		itemImage.enabled = false;
		itemRef = null;
		stackSize = 0;
		itemDurability = 0f;
	}

	public bool IsEmpty() {
		return this.itemRef == null;
	}
}
