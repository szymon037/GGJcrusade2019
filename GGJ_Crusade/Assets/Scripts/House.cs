using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {

	private static Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(player.position, this.transform.position) <= 1.5f) {
			EnterOrLeaveHouse();
		}
	}

	void EnterOrLeaveHouse() {
		PlayerStats.GetInstance().flags["atHome"] = !PlayerStats.GetInstance().flags["atHome"];
	}
}
