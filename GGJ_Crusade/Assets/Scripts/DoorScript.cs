using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isOpened = false;
    private Animator anim = null;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isOpened", isOpened);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isOpened = true;
    }

    
}