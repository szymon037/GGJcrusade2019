using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBehaviour : MonoBehaviour {

	public Item item;
	public Sprite objSprite = null;
	public static Transform player = null;
	public float minDistance = 0f;

	void Start () {
		objSprite = item.itemSprite;
		if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update () {
		if (Vector3.Distance(player.position, this.transform.position) <= minDistance) {
			PickUp();
		} 
	}

	void PickUp() {
		if (Inventory.instance.AddToInventory(this.item))
			Destroy(this.gameObject);
	}
}
