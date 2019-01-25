using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XboxController : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		float x = 0f, z = 0f;
		x = Input.GetAxis("GamepadHorizontal");
		z = Input.GetAxis("GamepadVertical");
		transform.Translate(new Vector3(x, 0, z));
	}
}
