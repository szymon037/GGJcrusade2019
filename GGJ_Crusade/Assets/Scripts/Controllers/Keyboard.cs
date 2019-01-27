using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (!PlayerStats.GetInstance().flags["atHome"]) {
			float x = 0f, z = 0f;
			x = Input.GetAxis("Horizontal") * Time.deltaTime * PlayerStats.GetInstance().playerStatistics.speed;
			z = Input.GetAxis("Vertical") * Time.deltaTime * PlayerStats.GetInstance().playerStatistics.speed;
			MainCharacterBehaviour.lookDirection = new Vector3(x, 0f, z).normalized;
			transform.Translate(new Vector3(x, 0, z));
		}
	}
}
