using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainCharacterBehaviour : MonoBehaviour {

	public Keyboard keyboard;
	public XboxController xboxpad;
	public Camera playerCamera;
	public static Vector3 lookDirection = new Vector3(0f,0f,0f);
	public static Vector3 playerPosition = new Vector3(0f, 0,0f);
	private float constCameraY = 0f;
	private WaitForSeconds waitTime = new WaitForSeconds(0.25f);
	public Image keyPrompt = null;

    private Vector3 oldPosition = Vector3.zero;
    public Vector3 lastMove = Vector3.zero;
    public bool isMoving = false;
    private float axisX = 0f;
    private float axisY = 0f;
    private Animator anim = null;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        oldPosition = this.transform.position;

        keyboard = GetComponent<Keyboard>();
		xboxpad = GetComponent<XboxController>();
		StartCoroutine("InteractionCheck");
        //constCameraY = playerCamera.transform.position.y;
        constCameraY = this.transform.position.z - playerCamera.transform.position.z;
		keyPrompt.enabled = false;
	}

	void FixedUpdate() {
        constCameraY = this.transform.position.z - playerCamera.transform.position.z;
        playerCamera.transform.position = new Vector3(this.transform.position.x, 60f, this.transform.position.z - 35f);
		playerPosition = this.transform.position;

		if (PlayerStats.GetInstance().hitTimer > 0f) {
			PlayerStats.GetInstance().hitTimer -= Time.deltaTime;
		} else {
			PlayerStats.GetInstance().flags["isHit"] = false;
		}

        //ANIMACJA

        if ((Mathf.Abs(playerPosition.x - oldPosition.x) > 0.01) || (Mathf.Abs(playerPosition.z - oldPosition.z) > 0.01))
        {
            isMoving = true;
            oldPosition = playerPosition;
        }
        else
        {
            isMoving = false;
        }

        if (((Input.GetAxisRaw("Horizontal") == 0f) && (Input.GetAxisRaw("Vertical") == 0f)))
            isMoving = false;

        axisX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxisRaw("Vertical");

        /*if (playerBehaviour.isAttacking)
        {
            if (AttackX > 0f && AttackY >= 0f)               //up
                lastMove = new Vector3(0f, 0f, 1f);

            else if (AttackX < 0f && AttackY <= 0f)          //down
                lastMove = new Vector3(0f, 0f, -1f);

            else if (AttackX >= 0f && AttackY < 0f)          //right   
                lastMove = new Vector3(1f, 0f, 0f);

            else if (AttackX <= 0f && AttackY > 0f)          //left
                lastMove = new Vector3(-1f, 0f, 0f);
        }

        else
        {*/
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.3f)
                lastMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.3f)
                lastMove = new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));
        //}

        anim.SetFloat("MoveX", axisX);
        anim.SetFloat("MoveY", axisY);
        
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.z);

        anim.SetBool("isMoving", isMoving);



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

	bool CheckForInteractions() {
		Collider[] caught = Physics.OverlapSphere(this.transform.position, 1.2f);
		foreach (var c in caught) {
			//Debug.Log(c.name);
			if (c.gameObject.CompareTag("Interactable")) {
				return true;
			}
		}
		return false;
	}

	IEnumerator InteractionCheck() {
		while (true) {
			keyPrompt.enabled = CheckForInteractions();
			yield return new WaitForSeconds(0.2f);
		}
	}

	public static int Attack(Vector3 direction, float damageValue) {
		int caughtEnemies = 0;

		RaycastHit[] hitResults = Physics.RaycastAll(playerPosition, direction, 5f);
		foreach (var hit in hitResults) {
			if (hit.transform != null) {
				if (hit.transform.gameObject.CompareTag("Enemy")) {
					caughtEnemies++;
					hit.transform.gameObject.GetComponent<Enemy>().ReceiveDamage(damageValue);
				}
			}
		}

		return caughtEnemies;
	}
}
