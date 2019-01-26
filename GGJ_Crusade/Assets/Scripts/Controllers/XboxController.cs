using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XboxController : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		float x = 0f, z = 0f;
		x = Input.GetAxis("GamepadHorizontal") * Time.deltaTime * PlayerStats.GetInstance().playerStatistics.speed;
		z = Input.GetAxis("GamepadVertical") * Time.deltaTime * PlayerStats.GetInstance().playerStatistics.speed;
		MainCharacterBehaviour.lookDirection = new Vector3(x, 0f, z).normalized;
		transform.Translate(new Vector3(x, 0, z));
	}
}
