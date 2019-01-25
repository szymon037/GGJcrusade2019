using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	public List<InventorySlot> slots = new List<InventorySlot>();
	public static Inventory instance = null;

	void Update() {
		instance = this;
	}
}
