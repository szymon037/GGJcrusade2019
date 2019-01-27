#define DEBUG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

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

	public Transform activeBar;
	public Transform playerInventory;

	public Transform targetPlayerInventory;
	public Transform targetActiveBar;

	public Transform gameplayActiveBar;

	public Transform houseWindow;

	public Item tempItem = null;

	public bool pickingUp = false;

	public Image pickUpImage = null;

	public InventorySlot currentlyPickedFrom = null;

	public Transform itemTextDisplay;

	public Image[] activeSlotPointers = null;
	public Image activePointer = null;

	void Awake() {
		inventoryPanel.gameObject.SetActive(false);
		craftingWindow.gameObject.SetActive(false);
		houseWindow.gameObject.SetActive(false);
		gameplayActiveBar.gameObject.SetActive(true);
		pickUpImage.enabled = false;
		playerInventory = inventoryPanel.transform.GetChild(0);
		activeBar = inventoryPanel.transform.GetChild(1);
		foreach (Transform child in inventoryPanel.transform.GetChild(0)) {
			slots.Add(child.gameObject.GetComponent<InventorySlot>());
		}
		foreach (Transform child in inventoryPanel.transform.GetChild(1)) {
			activeItemBarSlots.Add(child.gameObject.GetComponent<InventorySlot>());
		}
		activeSlot = activeItemBarSlots[0];
		foreach (var im in activeSlotPointers) {
			im.enabled = false;
		}
		activePointer = activeSlotPointers[0];
		activePointer.enabled = true;
	}

	void Update() {
		instance = this;
		//bool flag = false;
		if (PlayerStats.GetInstance().flags["storage"] && !inventoryPanel.gameObject.activeSelf) {
			houseWindow.gameObject.SetActive(!false);
			List<Transform> first = new List<Transform>(), second = new List<Transform>();
			foreach (Transform child in playerInventory) {
				first.Add(child);
			}	
			first = first.OrderBy(c => c.name).ToList();
			foreach (Transform child in activeBar) {
				second.Add(child);
			}
			second = second.OrderBy(c => c.name).ToList();
			foreach (var c in first) {
				c.SetParent(targetPlayerInventory);
			}
			foreach (var c in second) {
				c.SetParent(targetActiveBar);
			}
		} else {
			houseWindow.gameObject.SetActive(false);
			List<Transform> first = new List<Transform>(), second = new List<Transform>();
			foreach (Transform child in targetPlayerInventory) {
				first.Add(child);
			}	
			first = first.OrderBy(c => c.name).ToList();
			foreach (Transform child in targetActiveBar) {
				second.Add(child);
			}
			second = second.OrderBy(c => c.name).ToList();
			foreach (var c in first) {
				c.SetParent(playerInventory);
			}
			foreach (var c in second) {
				c.SetParent(activeBar);
			}
		}
		if (Input.GetKeyDown(KeyCode.I) && !PlayerStats.GetInstance().flags["storage"]) {
			inventoryPanel.gameObject.SetActive(!inventoryPanel.gameObject.activeSelf);
			activePointer.enabled = !inventoryPanel.gameObject.activeSelf;
		}

		#if DEBUG 
			if (Input.GetKeyDown(KeyCode.H) && !inventoryPanel.gameObject.activeSelf) PlayerStats.GetInstance().flags["storage"] = !PlayerStats.GetInstance().flags["storage"];
		#endif
		int result = 0;
		System.Int32.TryParse(Input.inputString, out result);
		switch (result) {
			case 1: case 2: case 3: case 4: case 5:
				activePointer.enabled = false;
				activePointer = activeSlotPointers[result - 1];
				activePointer.enabled = true;
				activeSlot = activeItemBarSlots[result-1];
				break;
			default:
				break;
		}

		if (inventoryPanel.gameObject.activeSelf) {
			activePointer.enabled = !inventoryPanel.gameObject.activeSelf;
			List<Transform> l = new List<Transform>();
			foreach (Transform child in gameplayActiveBar) {
				l.Add(child);
			}
			l = l.OrderBy(c=>c.name).ToList();
			foreach (var x in l) {
				x.SetParent(activeBar);
			}
			gameplayActiveBar.gameObject.SetActive(false);
			return;
		} else {
			activePointer.enabled = !inventoryPanel.gameObject.activeSelf;
			List<Transform> l = new List<Transform>();
			foreach (Transform child in activeBar) {
				l.Add(child);
			}
			l = l.OrderBy(c=>c.name).ToList();
			foreach (var x in l) {
				x.SetParent(gameplayActiveBar);
			}
			gameplayActiveBar.gameObject.SetActive(true);
		}
		if (houseWindow.gameObject.activeSelf) {
			activePointer.enabled = !houseWindow.gameObject.activeSelf;
			List<Transform> l = new List<Transform>();
			foreach (Transform child in gameplayActiveBar) {
				l.Add(child);
			}
			l = l.OrderBy(c=>c.name).ToList();
			foreach (var x in l) {
				x.SetParent(targetActiveBar);
			}
			
				gameplayActiveBar.gameObject.SetActive(false);
		} else {
			activePointer.enabled = !houseWindow.gameObject.activeSelf;
			List<Transform> l = new List<Transform>();
			foreach (Transform child in targetActiveBar) {
				l.Add(child);
			}
			l = l.OrderBy(c=>c.name).ToList();
			foreach (var x in l) {
				x.SetParent(gameplayActiveBar);
			}
			
				gameplayActiveBar.gameObject.SetActive(true);
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
				} else continue;
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
