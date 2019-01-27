using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {

	private static Transform player;
	public Transform houseExit;
	public Transform houseEntrance;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(player.position, houseExit.position) <= 1.5f) {
			player.position = houseEntrance.transform.position;
		}
		else if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(player.position, houseEntrance.position) <= 1.5f) {
			player.position = houseExit.transform.position;
		}
	}

}
