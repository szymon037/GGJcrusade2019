using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		float x = 0f, z = 0f;
		x = Input.GetAxis("Horizontal");
		z = Input.GetAxis("Vertical");
		transform.Translate(new Vector3(x, 0, z));
	}
}
