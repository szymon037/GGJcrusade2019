using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainCharacterBehaviour : MonoBehaviour {

	public Keyboard keyboard;
	public Camera playerCamera;
	public Vector3 lookDirection = new Vector3(0f,0f,0f);
	public Vector3 playerPosition = new Vector3(0f, 0,0f);
	private float constCameraY = 0f;
	private WaitForSeconds waitTime = new WaitForSeconds(0.25f);
	public Image keyPrompt = null;
	public static MainCharacterBehaviour instance = null;
	// Use this for initialization
	void Start () {
		keyboard = GetComponent<Keyboard>();
		StartCoroutine("InteractionCheck");
		constCameraY = playerCamera.transform.position.y;
		keyPrompt.enabled = false;
	}

	void Update() {
		instance = this;
		playerCamera.transform.position = new Vector3(this.transform.position.x, constCameraY, this.transform.position.z);
		playerPosition = this.transform.position;

		if (PlayerStats.GetInstance().hitTimer > 0f) {
			PlayerStats.GetInstance().hitTimer -= Time.deltaTime;
		} else {
			PlayerStats.GetInstance().flags["isHit"] = false;
		}

		if (Input.GetMouseButtonDown(0)) {
			if (Inventory.instance.activeSlot.itemRef.type != ItemType.Weapon) Inventory.instance.UseActiveItem();
			else Attack(Inventory.instance.activeSlot.itemRef.itemEffectValue) ;
		}

		if (!PlayerStats.GetInstance().flags["storage"]) {
			float x = 0f, z = 0f;
			x = Input.GetAxis("Horizontal") ;
			z = Input.GetAxis("Vertical") ;
			lookDirection = new Vector3(x, 0, z);
			playerPosition = this.transform.position;
			transform.Translate(new Vector3(x * Time.deltaTime * PlayerStats.GetInstance().playerStatistics.speed, 0f, z* Time.deltaTime * PlayerStats.GetInstance().playerStatistics.speed));
		}

		
	}

	bool CheckForInteractions() {
		Collider[] caught = Physics.OverlapSphere(this.transform.position, 1.2f);
		foreach (var c in caught) {
			//Debug.Log(c.name);
			if (c.gameObject.CompareTag("Interactable")) {
				return true;
			}
		}
		return false;
	}

	IEnumerator InteractionCheck() {
		while (true) {
			keyPrompt.enabled = CheckForInteractions();
			yield return new WaitForSeconds(0.2f);
		}
	}

	public int Attack(float damageValue) {
		int caughtEnemies = 0;
		Debug.Log("xD");
		Debug.DrawRay(playerPosition, lookDirection * 10f, Color.red ,100f);
		Debug.Log(playerPosition.ToString());
		Debug.Log("lookdirection:" + (lookDirection * 10).ToString());
		RaycastHit[] hitResults = Physics.RaycastAll(playerPosition, lookDirection, 10f);
		Debug.Log(hitResults.Length.ToString());
		foreach (var hit in hitResults) {
			if (hit.transform != null) {
				Debug.Log(hit.transform.name);
				if (hit.transform.gameObject.CompareTag("Enemy")) {
					caughtEnemies++;
					Debug.Log("xD");
					hit.transform.gameObject.GetComponent<Enemy>().ReceiveDamage(damageValue);
				}
			}
		}

		return caughtEnemies;
	}
}
