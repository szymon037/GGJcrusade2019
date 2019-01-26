using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
	public Item itemRef = null;
	public Image itemImage = null;
	public Button slotButton = null;
	public uint stackSize = 0;

	void Awake() {
		this.slotButton = gameObject.GetComponentInChildren<Button>();
		this.itemImage = transform.GetChild(1).gameObject.GetComponent<Image>();
		this.slotButton.onClick.AddListener(() => Inventory.instance.PickFromSlot());
		this.itemImage.enabled = false;
	}

	public void Add(Item item, uint amount) {
		this.itemRef = item;
		try {
			this.itemImage.sprite = item.itemSprite;
		} catch (System.Exception) {

		}
		this.stackSize += amount;
		this.itemImage.enabled = true;
	}

	public void Clear() {
		itemImage.enabled = false;
		itemRef = null;
	}

	public bool IsEmpty() {
		return this.itemRef == null;
	}
}
