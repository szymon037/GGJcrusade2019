using UnityEngine.UI;
using UnityEngine;

public enum ItemType {
	Food,
	Drink,
	Material,
	Weapon,
	Armor
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject {
	public uint stackSize = 0;
	public uint maxStackSize = 0;
	public uint ID = 0;

	public Sprite itemSprite = null;

	public string itemName = null;
	public string description = null;

	public ItemType type;

	public float itemEffectValue;
}
