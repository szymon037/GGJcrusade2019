using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterBehaviour : MonoBehaviour {

	public Keyboard keyboard;
	public XboxController xboxpad;
	public Camera playerCamera;
	public static Vector3 lookDirection = new Vector3(0f,0f,0f);
	public static Vector3 playerPosition = new Vector3(0f, 0,0f);
	private float constCameraY = 0f;
	private WaitForSeconds waitTime = new WaitForSeconds(0.25f);

	// Use this for initialization
	void Start () {
		keyboard = GetComponent<Keyboard>();
		xboxpad = GetComponent<XboxController>();
		StartCoroutine("InputCheck");
		constCameraY = playerCamera.transform.position.y;
	}

	void Update() {
		playerCamera.transform.position = new Vector3(this.transform.position.x, constCameraY, this.transform.position.z);
		playerPosition = this.transform.position;
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
