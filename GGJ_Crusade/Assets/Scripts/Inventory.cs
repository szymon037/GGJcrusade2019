using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*TODO: UŻYĆ SPRITE RENDERERA ABY DOSTAĆ SIĘ DO SPRITE'A I PODMIENIĆ*/
public class Inventory : MonoBehaviour {
	public List<InventorySlot> activeItemBarSlots = new List<InventorySlot>();

	public List<InventorySlot> slots = new List<InventorySlot>();

	public static Inventory instance = null;

	public Button activeButton;
	public InventorySlot activeSlot;

	public Transform player = null;

	public RectTransform inventoryPanel;
	public RectTransform craftingWindow;

	public Item tempItem = null;

	public bool pickingUp = false;

	public Image pickUpImage = null;

	public InventorySlot currentlyPickedFrom = null;

	void Awake() {
		inventoryPanel.gameObject.SetActive(false);
		craftingWindow.gameObject.SetActive(false);
		pickUpImage.enabled = false;
		foreach (Transform child in inventoryPanel.transform.GetChild(0)) {
			slots.Add(child.gameObject.GetComponent<InventorySlot>());
		}
		foreach (Transform child in inventoryPanel.transform.GetChild(1)) {
			slots.Add(child.gameObject.GetComponent<InventorySlot>());
		}
	}

	void Update() {
		instance = this;
		if (Input.GetKeyDown(KeyCode.I)) {
			inventoryPanel.gameObject.SetActive(!inventoryPanel.gameObject.activeSelf);
		}
	}

	public bool IsFull() {
		foreach (var slot in slots) {
			if (slot.itemRef == null) return false;
		}
		return true;
	}

	public bool AddToInventory(Item item, uint amount) {
		if (IsFull()) return false;
		foreach (var slot in slots) {
			if (slot.IsEmpty()) {
				slot.Add(item, amount);
				break;
			} else {
				if (item.itemName == slot.itemRef.itemName) {
					if (amount + slot.stackSize > slot.itemRef.maxStackSize) continue;
					else {
						slot.stackSize += amount;
						break;
					}
				} else {
					slot.Add(item, amount);
					break;
				}
			}
		}
		return true;
	}

	public void UseActiveItem() {
		if (activeSlot == null || activeSlot.itemRef == null) return;
		switch (activeSlot.itemRef.type) {
			case ItemType.Food:
				PlayerStats.GetInstance().ModifyHungerMeter(activeSlot.itemRef.itemEffectValue);
//				activeSlot.Clear();
				activeSlot.stackSize--;
				if (activeSlot.stackSize == 0) activeSlot.Clear();
				break;
			case ItemType.Drink:
				PlayerStats.GetInstance().ModifyThirstMeter(activeSlot.itemRef.itemEffectValue);
//				activeSlot.Clear();
				activeSlot.stackSize--;
				if (activeSlot.stackSize == 0) activeSlot.Clear();
				break;
			case ItemType.Material: 
				break;
			case ItemType.Weapon:
				//miejsce na atak
				int numberOfHits = PlayerStats.GetInstance().attack(MainCharacterBehaviour.lookDirection, activeSlot.itemRef.itemEffectValue);
				activeSlot.itemDurability -= numberOfHits * activeSlot.itemRef.durabilityLostPerHit;
				if (activeSlot.itemDurability <= 0f) activeSlot.Clear();
				break;
			case ItemType.Armor: 
				break;
		}
	}

	public void PickFromSlot() {
		Transform temp = EventSystem.current.currentSelectedGameObject.transform;
		if (temp.parent != null) {
			if (!pickingUp) {
				if (temp.parent.GetComponent<InventorySlot>().itemRef == null) {
					Debug.Log("empty");
					Debug.Log(temp.parent.name);
					return;
				} else {
					pickingUp = true;
					Debug.Log("picking up");
					Debug.Log(temp.parent.name);
					tempItem = temp.parent.GetComponent<InventorySlot>().itemRef;
					pickUpImage.sprite = temp.parent.GetComponent<InventorySlot>().itemImage.sprite;
					pickUpImage.enabled = true;
					currentlyPickedFrom = temp.parent.GetComponent<InventorySlot>();
				}
			} else {
				if (temp.parent.GetComponent<InventorySlot>().itemRef == null) {
					Debug.Log("adding");
					Debug.Log(temp.parent.name);
					pickingUp = false;
					pickUpImage.enabled = false;
					temp.parent.GetComponent<InventorySlot>().Add(currentlyPickedFrom.itemRef, currentlyPickedFrom.stackSize);
					currentlyPickedFrom.Clear();
					currentlyPickedFrom = null;
					temp.parent.GetComponent<InventorySlot>().itemImage.sprite = temp.parent.GetComponent<InventorySlot>().itemRef.itemSprite;
					return;
				} else {
					Debug.Log("Swapping");
					Debug.Log(temp.parent.name);
					Item xD = currentlyPickedFrom.itemRef;
					currentlyPickedFrom.itemRef = temp.parent.GetComponent<InventorySlot>().itemRef;
					temp.parent.GetComponent<InventorySlot>().itemRef = xD;
					Image tempimg = currentlyPickedFrom.itemImage;
					currentlyPickedFrom.itemImage = temp.parent.GetComponent<InventorySlot>().itemImage;
					temp.parent.GetComponent<InventorySlot>().itemImage = tempimg;
					currentlyPickedFrom = temp.parent.GetComponent<InventorySlot>();
				}
			}
		}
	}
}
