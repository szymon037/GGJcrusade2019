using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour {

	public string enemyName = "";
	public float health = 100f;
	public float speed = 5f;
	public float attackRange = 2f;
	public float attackDamage = 0f;
	public float attackTimer = 0f;
	public Sprite enemySprite = null;
	public SpriteRenderer renderer = null;
	public static Transform player = null;


	// Use this for initialization
	void Start () {
		if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
		renderer = GetComponent<SpriteRenderer>();
		enemySprite = renderer.sprite;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Vector3.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
		if (attackTimer <= 0f && Vector3.Distance(player.position, this.transform.position) <= 1.5f) {
			Attack();
		}
		if (attackTimer > 0f) attackTimer -= Time.deltaTime;
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
		Debug.Log(this.health.ToString());
		if (this.health <= 0f) Die();
	}

	public void Attack() {
		Debug.Log("Attacking");
		Vector3 direction = player.position - this.transform.position;
		RaycastHit[] hits = Physics.RaycastAll(this.transform.position, direction, 5f);
		foreach (var hit in hits) {
			if (hit.transform != null) {
				if (hit.transform.gameObject.CompareTag("Player")) {
					PlayerStats.GetInstance().ReduceHealth(this.attackDamage);
					break;
				}
			}
		}
		attackTimer = 0.5f;
	}
}
