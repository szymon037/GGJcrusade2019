using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeButton : MonoBehaviour {
	public string itemName;
	public void Craft() {
		CraftingManager.instance.CreateItem(itemName);
	}
}
