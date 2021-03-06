﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe {
	public string itemNameToCreate;
	public uint amountOfItemToCreate;
	public Dictionary<string, uint> resourcesNeeded = new Dictionary<string, uint>();
	public Recipe(string name, uint amount, params Pair<string, uint>[] resources) {
		this.itemNameToCreate = name;
		this.amountOfItemToCreate = amount;
		foreach (Pair<string, uint> pair in resources) {
			this.resourcesNeeded[pair.item1] = pair.item2;
		}
	}
}
