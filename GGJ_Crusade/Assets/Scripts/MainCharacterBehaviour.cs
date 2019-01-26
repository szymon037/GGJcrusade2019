using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterBehaviour : MonoBehaviour {

	public Keyboard keyboard;
	public XboxController xboxpad;
	public Camera playerCamera;
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
}
