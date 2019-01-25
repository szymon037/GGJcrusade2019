using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterBehaviour : MonoBehaviour {

	public Keyboard keyboard;
	public XboxController xboxpad;

	private WaitForSeconds waitTime = new WaitForSeconds(0.25f);

	// Use this for initialization
	void Start () {
		StartCoroutine("InputCheck");
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
