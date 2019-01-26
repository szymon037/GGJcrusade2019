using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int dayCount = 1;
	public float dayTime = 300f;//5 minut, potem najwyżej zmienimy
	public float dayTimeCounter = 0f;

	public static GameManager instance = null;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
		dayTimeCounter = dayTime;
	}
	
	// Update is called once per frame
	void Update () {
		instance = this;
		if (dayTimeCounter > 0 && !PlayerStats.GetInstance().flags["atHome"]) dayTimeCounter -= Time.deltaTime;
		else InitHorde();
	}

	void InitHorde() {
		
	}

	public void EndTheDay() {
		++dayCount;
		PlayerStats.GetInstance().ModifyThirstMeter(-20f);
		PlayerStats.GetInstance().ModifyHungerMeter(-20f);
	}
}
