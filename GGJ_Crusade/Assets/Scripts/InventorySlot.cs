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
	}

	public void Add(Item item) {
		this.itemRef = item;
		this.itemImage.sprite = item.itemSprite;
		this.itemImage.enabled = true;
	}

	public void Clear() {
		itemImage.enabled = false;
		itemRef = null;
	}
}
