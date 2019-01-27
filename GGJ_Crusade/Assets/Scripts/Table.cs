using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

	public static Transform player = null;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(player.position, this.transform.position) <= 1.5f) {
			Inventory.instance.craftingWindow.gameObject.SetActive(!Inventory.instance.craftingWindow.gameObject.activeSelf);
		}
	}
}
