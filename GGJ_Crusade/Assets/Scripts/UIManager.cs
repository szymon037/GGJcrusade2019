using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance = null;

	public Image healthDisplay;
	public Sprite[] healthSprites;
	public Image hungerDisplay;
	public Sprite[] hungerSprites;

	// Use this for initialization
	void Start () {
		UpdateSprites();
	}
	
	// Update is called once per frame
	void Update () {
		instance = this;
	}

	public void UpdateSprites() {
		float health = PlayerStats.GetInstance().playerStatistics.health;
		float hunger = PlayerStats.GetInstance().playerStatistics.hunger;
		float x = health / PlayerStats.GetInstance().playerStatistics.maxHealth;
		float y = hunger / PlayerStats.GetInstance().playerStatistics.maxHunger;
		if (x >= 0.33f && x <= 0.66f) healthDisplay.sprite = healthSprites[1];
		else if (x < 0.33f) healthDisplay.sprite = healthSprites[2];
		else healthDisplay.sprite = healthSprites[0];
	//	if (y >= 0.33f && y <= 0.66f) hungerDisplay.sprite = hungerSprites[1];
	//	else if (y < 0.33f) hungerDisplay.sprite = hungerSprites[2];
	//	else hungerDisplay.sprite = hungerSprites[0];
	}
}
