using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

public class CraftingManager : MonoBehaviour {

	public List<Recipe> recipes = new List<Recipe>();
	public List<Item> allItems = new List<Item>();

	public static CraftingManager instance = null;

	void Awake() {
		DontDestroyOnLoad(this.gameObject);
		InitRecipes();
		SaveRecipesToJson();
	}

	void Update() {
		instance = this;
	}

	public void LoadRecipesFromJson() {
		using (StreamReader reader = new StreamReader("JsonFiles/recipes.json")) {
			recipes = JsonConvert.DeserializeObject<List<Recipe>>(reader.ReadToEnd());
		}
	}
	public void SaveRecipesToJson() {
		FileStream fs = new FileStream("JsonFiles/recipes.json", FileMode.OpenOrCreate);
		using (StreamWriter writer = new StreamWriter(fs)){
			writer.WriteLine(JsonConvert.SerializeObject(recipes, Formatting.Indented));
		} 
		fs.Close();
	}

	public bool ValidateRecipe(Recipe recipe) {
		Dictionary<string ,bool> validatedRecipes = new Dictionary<string, bool>();
		foreach (var s in recipe.resourcesNeeded.Keys) validatedRecipes[s] = false;
		foreach (var entry in recipe.resourcesNeeded) {
			foreach (InventorySlot slot in Inventory.instance.slots) {
				if (slot.itemRef.itemName == entry.Key) {
					if (slot.itemRef.stackSize >= entry.Value) {
						validatedRecipes[entry.Key] = true;
					} else {
						return false;
					}
				}
			}
		}
		return true;
	}

	public void InitRecipes() {
		List<string> lines = new List<string>();
		using (StreamReader reader = new StreamReader("JsonFiles/recipes.txt")) {
			while (!reader.EndOfStream) lines.Add(reader.ReadLine());
		}
		foreach (string line in lines) {
			string[] temp = line.Split();
			string[] newArray = new string[temp.Length - 1];
			for (int i = 1; i < temp.Length; ++i) {
				newArray[i - 1] = temp[i];
			}
			List<Pair<string, uint>> pairList = new List<Pair<string, uint>>();
			for (int j = 0; j < newArray.Length; j += 2) {
				Pair<string, uint> tempPair = new Pair<string, uint>(newArray[j], System.UInt32.Parse(newArray[j + 1]));
				pairList.Add(tempPair);
			}
			Recipe newRecipe = new Recipe(temp[0], pairList.ToArray());
			recipes.Add(newRecipe);
		}
	}

	public GameObject GenerateItem(Item item) {
		GameObject handle = new GameObject();
		(handle.AddComponent<ItemBehaviour>() as ItemBehaviour).Init(item);
		return handle;
	}
}
