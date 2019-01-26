using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe {
	public string itemNameToCreate;
	public Dictionary<string, uint> resourcesNeeded = new Dictionary<string, uint>();
	public Recipe(string name, params Pair<string, uint>[] resources) {
		this.itemNameToCreate = name;
		foreach (Pair<string, uint> pair in resources) {
			this.resourcesNeeded[pair.item1] = pair.item2;
		}
	}
	// public void Display() {
	// 	Debug.Log(itemNameToCreate);
	// 	string result = "";
	// 	foreach (var entry in resourcesNeeded) {
	// 		result += entry.Key + " : " + entry.Value.ToString() + ", ";
	// 	}
	// 	Debug.Log(result);
	// 	Debug.Log("leaving...");
	// }
}
