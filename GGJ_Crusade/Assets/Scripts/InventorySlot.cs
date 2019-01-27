using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
		EventTrigger temp = this.slotButton.gameObject.AddComponent<EventTrigger>() as EventTrigger;
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerEnter;
		entry.callback.AddListener((eventData)=>{Display();});
		temp.triggers.Add(entry);
		EventTrigger temp1 = this.slotButton.gameObject.AddComponent<EventTrigger>() as EventTrigger;
		EventTrigger.Entry exit = new EventTrigger.Entry();
		exit.eventID = EventTriggerType.PointerExit;
		exit.callback.AddListener((eventData)=>{ClearDisplay();});
		temp1.triggers.Add(exit);
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
		this.itemDurability = item.durability;
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

	public void Display() {
		try {
			Inventory.instance.itemTextDisplay.gameObject.GetComponent<Text>().text = string.Format("{0}\nAmount: {1} \n{2}", itemRef.itemName, stackSize, itemRef.description);
			} catch (System.Exception) {

			}
	}

	public void ClearDisplay() {
		Inventory.instance.itemTextDisplay.gameObject.GetComponent<Text>().text = "";
	}
}
