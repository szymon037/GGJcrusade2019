using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public string enemyName = "";
	public float health = 100f;
	public float speed = 5f;
	public float attackRange = 2f;
	public Sprite enemySprite = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy() {
		byte dropChance = (byte)(100 * Random.value);
		if (dropChance <= 10) {

		}
	}

	public void Die() {
		Destroy(this.gameObject);
	}

	public void ReceiveDamage(float value) {
		this.health -=value;
	}


}
