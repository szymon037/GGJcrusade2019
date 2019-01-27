using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats {
	public struct Stats {
		public float health;
		public float hunger;
		public float thirst;
		public float stamina;
		public float speed;
		public float maxHealth;
		public float maxHunger;

		public Stats(float _health, float _hunger, float _thirst, float _stamina) {
			this.health = this.maxHealth = _health;
			this.hunger = this.maxHunger = _hunger;
			this.thirst = _thirst;
			this.stamina = _stamina;
			this.speed = 1f;
		}
	}

	//singleton handle
	private static PlayerStats handle = null;

	//actual statistics
	public Stats playerStatistics;
	public Dictionary<string, bool> flags;
	public System.Func<Vector3, float, int> attack = null;

	public float hitTimer = 0f;
	public float attackTimer = 0f;

	private PlayerStats() {
		playerStatistics = new Stats(100f, 100f, 100f, 100f);
		flags = new Dictionary<string, bool>();
		flags["isHit"] = false;
		flags["hungerBelowZero"] = false;
		flags["thirstBelowZero"] = false;
		flags["atHome"] = false;
		attack = MainCharacterBehaviour.Attack;
	}

	public static PlayerStats GetInstance() {
		if (handle == null) handle = new PlayerStats();
		return handle;
	}

	public void ReduceHealth(float value) {
		this.playerStatistics.health -= value;
		if (this.playerStatistics.health < 0f) this.playerStatistics.health = 0f; 
		this.flags["isHit"] = true;
		hitTimer = 0.5f;
		UIManager.instance.UpdateSprites();
	}

	public void RestoreHealth(float value) {
		this.playerStatistics.health += value;
		if (this.playerStatistics.health > this.playerStatistics.maxHealth) this.playerStatistics.health = this.playerStatistics.maxHealth;
		UIManager.instance.UpdateSprites();
	}

	public void ReduceStamina(float value) {
		this.playerStatistics.stamina -= value;
		if (this.playerStatistics.stamina < 0f) this.playerStatistics.stamina = 0f;
	}

	public void ModifyHungerMeter(float value) {
		this.playerStatistics.hunger += value;
		if (this.playerStatistics.hunger < 0f) this.playerStatistics.hunger = 0f;
		UIManager.instance.UpdateSprites();
	}

	public void ModifyThirstMeter(float value) {
		this.playerStatistics.thirst += value;
		if (this.playerStatistics.thirst < 0f) this.playerStatistics.thirst = 0f;
		UIManager.instance.UpdateSprites();
	}
}
