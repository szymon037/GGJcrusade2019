using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	public List<InventorySlot> activeItemBarSlots = new List<InventorySlot>();

	public List<InventorySlot> slots = new List<InventorySlot>();

	public static Inventory instance = null;

	public Button activeButton;
	public InventorySlot activeSlot;

	public Transform player = null;

	public RectTransform inventoryWindow;
	public RectTransform craftingWindow;

	public Item tempItem = null;

	public bool pickingUp = false;

	void Awake() {
		inventoryWindow.gameObject.SetActive(false);
		craftingWindow.gameObject.SetActive(false);
	}

	void Update() {
		instance = this;
		if (Input.GetKeyDown(KeyCode.I)) {
			inventoryWindow.gameObject.SetActive(!inventoryWindow.gameObject.activeSelf);
		}
	}

	public bool IsFull() {
		foreach (var slot in slots) {
			if (slot.itemRef == null) return false;
		}
		return true;
	}

	public bool AddToInventory(Item item) {
		if (IsFull()) return false;
		uint stackSize = item.stackSize;
		foreach (var slot in slots) {
			if (item.ID == slot.itemRef.ID) {
				if (stackSize + slot.itemRef.stackSize <= slot.itemRef.maxStackSize) {
					slot.itemRef.stackSize += stackSize;
					break;
				} else continue;
			} else {
				slot.Add(item);
				break;
			}
		}
		return false;
	}

	public void UseActiveItem() {
		if (activeSlot == null || activeSlot.itemRef == null) return;
		switch (activeSlot.itemRef.type) {
			case ItemType.Food:
				PlayerStats.GetInstance().ModifyHungerMeter(activeSlot.itemRef.itemEffectValue);
				activeSlot.Clear();
				break;
			case ItemType.Drink:
				PlayerStats.GetInstance().ModifyThirstMeter(activeSlot.itemRef.itemEffectValue);
				activeSlot.Clear();
				break;
			case ItemType.Material: 
				break;
			case ItemType.Weapon:
				//miejsce na atak
				break;
			case ItemType.Armor: 
				break;
		}
	}


}
