using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceInventorySlot : MonoBehaviour {
	public InventorySlot slot;
	public Image itemImage;
	public void Assign(InventorySlot _slot) {
		this.slot = _slot;
	}
}
