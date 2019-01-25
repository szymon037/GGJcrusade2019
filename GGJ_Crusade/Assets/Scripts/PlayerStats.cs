using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats {
	public struct Stats {
		public float health;
		public float hunger;
		public float thirst;
		public float stamina;

		public Stats(float _health, float _hunger, float _thirst, float _stamina) {
			this.health = _health;
			this.hunger = _hunger;
			this.thirst = _thirst;
			this.stamina = _stamina;
		}
	}

	//singleton handle
	private static PlayerStats handle = null;

	//actual statistics
	public Stats playerStatistics;
	public Dictionary<string, bool> flags;

	private PlayerStats() {
		playerStatistics = new Stats(100f, 100f, 100f, 100f);
		flags = new Dictionary<string, bool>();
		flags["isHit"] = false;
		flags["hungerBelowZero"] = false;
		flags["thirstBelowZero"] = false;
	}

	public static PlayerStats GetInstance() {
		if (handle == null) handle = new PlayerStats();
		return handle;
	}

	public void ReduceHealth(float value) {
		this.playerStatistics.health -= value;
		if (this.playerStatistics.health < 0f) this.playerStatistics.health = 0f; 
	}

	public void ReduceStamina(float value) {
		this.playerStatistics.stamina -= value;
		if (this.playerStatistics.stamina < 0f) this.playerStatistics.stamina = 0f;
	}

	public void ModifyHungerMeter(float value) {
		this.playerStatistics.hunger += value;
	}

	public void ModifyThirstMeter(float value) {
		this.playerStatistics.thirst += value;
	}
}
