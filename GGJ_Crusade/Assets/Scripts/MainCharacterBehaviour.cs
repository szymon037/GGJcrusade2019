using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainCharacterBehaviour : MonoBehaviour {

	public Keyboard keyboard;
	public XboxController xboxpad;
	public Camera playerCamera;
	public static Vector3 lookDirection = new Vector3(0f,0f,0f);
	public static Vector3 playerPosition = new Vector3(0f, 0,0f);
	private float constCameraY = 0f;
	private WaitForSeconds waitTime = new WaitForSeconds(0.25f);
	public Image keyPrompt = null;

	// Use this for initialization
	void Start () {
		keyboard = GetComponent<Keyboard>();
		xboxpad = GetComponent<XboxController>();
		StartCoroutine("InteractionCheck");
		constCameraY = playerCamera.transform.position.y;
		keyPrompt.enabled = false;
	}

	void Update() {
		playerCamera.transform.position = new Vector3(this.transform.position.x, constCameraY, this.transform.position.z);
		playerPosition = this.transform.position;

		if (PlayerStats.GetInstance().hitTimer > 0f) {
			PlayerStats.GetInstance().hitTimer -= Time.deltaTime;
		} else {
			PlayerStats.GetInstance().flags["isHit"] = false;
		}
	}

	void GamepadCheck() {
		string[] gamepads = Input.GetJoystickNames();
		if (gamepads == null || gamepads.Length <= 0) {
			xboxpad.enabled = false;
			keyboard.enabled = true;
		} else {
			xboxpad.enabled = true;
			keyboard.enabled = false;
		}
	}

	IEnumerator InputCheck() {
		while (true) {
			GamepadCheck();
			yield return waitTime;
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

	public static int Attack(Vector3 direction, float damageValue) {
		int caughtEnemies = 0;

		RaycastHit[] hitResults = Physics.RaycastAll(playerPosition, direction, 5f);
		foreach (var hit in hitResults) {
			if (hit.transform != null) {
				if (hit.transform.gameObject.CompareTag("Enemy")) {
					caughtEnemies++;
					hit.transform.gameObject.GetComponent<Enemy>().ReceiveDamage(damageValue);
				}
			}
		}

		return caughtEnemies;
	}
}
