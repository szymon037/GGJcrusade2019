using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour {

    public Rigidbody body = null;
    private float tempSpeed = 0f;
    public Vector3 moveDirection = new Vector3(0f, 0f, 0f);


    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update () {
		if (!PlayerStats.GetInstance().flags["atHome"]) {
			float x = 0f, z = 0f;
			x = Input.GetAxisRaw("Horizontal");
			z = Input.GetAxisRaw("Vertical") ;
			MainCharacterBehaviour.lookDirection = new Vector3(x, 0f, z).normalized;

            tempSpeed = PlayerStats.GetInstance().playerStatistics.speed;

            if (x != 0 && z != 0) tempSpeed /= 1.41f;
            moveDirection.x = x * tempSpeed * 0.707f;
            moveDirection.z = z * tempSpeed;
            body.velocity = moveDirection;


            //transform.Translate(new Vector3(x, 0, z));
		}
	}
}
