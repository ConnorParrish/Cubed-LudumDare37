using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerMovementScript : MonoBehaviour {

    /// <summary>
    /// Bullet prefab loaded from Inspector.
    /// </summary>
	public GameObject bullet;
	private bool isFinished;
	private bool isDead;
	private bool onMovingPlatform;

	public float speed;
	Rigidbody playerRigidbody;
	Vector3 movement;
	GameObject GameOverCanvas;
	GameObject NextLevelCanvas;
	private List<GameObject> Bullets = new List<GameObject>(2);
	bool evenBullet;

	// Use this for initialization
	void Start () {
		playerRigidbody = GetComponent<Rigidbody>();

		GameOverCanvas = GameObject.Find("GameOverUI");
		NextLevelCanvas = GameObject.Find("NextLevelUI");
		// PauseCanvas = GameObject.Find("PauseUI");

		GameOverCanvas.SetActive(false);
		NextLevelCanvas.SetActive(false);
		// PauseCanvas.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		movement = Vector3.right * speed * Time.deltaTime;

		if (isDead){
			Debug.Log("Ah, shit. He's dead...");
		}

		if (transform.position.y < -30)
        {
			isDead = true;
			GameOverCanvas.SetActive(true);
		}
		if (isFinished)
        {
			Debug.Log("You completed the level!");
			movement = Vector3.zero;
			NextLevelCanvas.SetActive(true);			
		}
		if (Input.GetKeyDown(KeyCode.Escape))
        {
			Application.Quit();
		}
        HandleKeyboardInputs();
				
	}

    /// <summary>
    /// Handle user input
    /// </summary>
    private void HandleKeyboardInputs()
    {
        // if (Input.GetKeyDown(KeyCode.Escape) && !PauseCanvas.activeSelf){
        // PauseCanvas.SetActive(true);
        // } else if (Input.GetKeyDown(KeyCode.Escape) && PauseCanvas.activeSelf){
        // PauseCanvas.SetActive(false);


        // if ((h != 0 || v != 0) && transform.parent != null){
        // 	playerRigidbody.isKinematic = false;
        // 	transform.parent = null;
        // }

        // Move(h,v);
        // Debug.Log("h: " + h + "| v: " + v);
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(movement);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(movement);
            transform.eulerAngles = new Vector3(0, -90, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(movement);
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(movement);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (transform.parent != null)
            {
                Debug.Log("Parent position" + transform.parent.position);
                ShootOrb(transform.parent.position + new Vector3(0, 1.5f, 0));
            }
            else if (transform.parent == null) // this doesn't make any sense
            {   //          quat rotation       spawn location      bulletangle
                ShootOrb(transform.position);
            }
        }
    }

    /// <summary>
    /// Shoots an orb from the player.
    /// </summary>
    /// <param name="spawnOffset">The spawn location of the bullet/orb</param>
    void ShootOrb(Vector3 spawnOffset){
        Quaternion rotation = transform.rotation;
        float bulletAngle = transform.eulerAngles.y;

		GameObject orb = (GameObject)Instantiate (bullet, spawnOffset, rotation);
		if (!evenBullet)
        {
			orb.GetComponent<ColorScript>().isOrange = true;
			evenBullet = !evenBullet;
		} else
        {
			orb.GetComponent<ColorScript>().isOrange = false;
			evenBullet = !evenBullet;
		}
		orb.GetComponent<BulletMovement>().SetDirection(bulletAngle);

		Bullets.Add(orb);

		if (Bullets.Count > 2){
			Destroy(Bullets[0]);
			Bullets[0] = Bullets[1];
			Bullets.RemoveAt(1);

		}

	}

    /// <summary>
    /// Moves the player based off user input
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
	void Move (float h, float v) {
		movement.Set(v, 0f, -h);
		movement = movement.normalized * speed * Time.deltaTime;
		transform.Translate(movement);
//		playerRigidbody.MovePosition (transform.position + movement);

	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Door"){

			isFinished = true;
		}

		if (col.gameObject.tag == "Bullet"){
			if (Bullets[0] == col.gameObject && (Bullets[0].GetComponent<BulletMovement>().isPlaced && Bullets[1].GetComponent<BulletMovement>().isPlaced)){
				Debug.Log("You touched the first bullet");
				transform.position = Bullets[1].transform.position;
			}
			else if (Bullets[1] == col.gameObject && (Bullets[0].GetComponent<BulletMovement>().isPlaced && Bullets[1].GetComponent<BulletMovement>().isPlaced)){
				Debug.Log("You touched the second bullet");
				transform.position = Bullets[0].transform.position;
			}

			Destroy(Bullets[1]);
			Bullets.Remove(Bullets[1]);
			Destroy(Bullets[0]);
			Bullets.Remove(Bullets[0]);
		}

		// if (col.gameObject.tag == "MovingPlatform"){
		// 	playerRigidbody.isKinematic = true;
		// 	transform.parent = col.transform;
		// }
	}

	// 	if (col.gameObject.GetComponent<CharacterHold>()){
	// 		playerRigidbody.useGravity = false;
	// 	}
	// }
	// void OnTriggerExit(Collider col){
	// 	if (col.gameObject.GetComponent<CharacterHold>()){
	// 		playerRigidbody.useGravity = true;
	// 	}
	// }
}
