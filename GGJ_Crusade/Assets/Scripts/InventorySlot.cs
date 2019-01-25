using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
	public Item itemRef = null;
	public Image itemImage = null;
	public Button slotButton = null;

	void Awake() {
		this.itemImage = gameObject.GetComponentInChildren<Image>();
		this.slotButton = gameObject.GetComponentInChildren<Button>();
		this.slotButton.onClick.AddListener(() => Inventory.instance.PickFromSlot());
		this.itemImage.enabled = false;
	}

	public void Add(Item item) {
		this.itemRef = item;
		try {
			this.itemImage.sprite = item.itemSprite;
		} catch (System.Exception) {

		}
		this.itemImage.enabled = true;
	}

	public void Clear() {
		itemImage.enabled = false;
		itemRef = null;
	}
}
