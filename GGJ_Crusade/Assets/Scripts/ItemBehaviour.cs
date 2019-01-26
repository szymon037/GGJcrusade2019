using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBehaviour : MonoBehaviour {

	public Item item;
	public uint itemAmount;
	public Sprite objSprite = null;
	public static Transform player = null;
	public float minDistance;

	void Start() {
		Init(item);
	}
	
	void Update () {
		float dist = Vector3.Distance(player.position, this.transform.position);
		if (dist <= minDistance && Input.GetKeyDown(KeyCode.E)) {
			PickUp();
		} 
	}

	void PickUp() {
		Debug.Log("pickingUp");
		if (Inventory.instance.AddToInventory(this.item, this.itemAmount))
			Destroy(this.gameObject);
	}

	public void Init(Item _item) {
		minDistance = 1.2f;
		objSprite = item.itemSprite;
		uint _amount = item.maxStackSize / 2;
		itemAmount = (uint)Random.Range(1, _amount <= 0 ? 1 : _amount);
		if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
	}
}
