using UnityEngine;
using System.Collections;
using System;

public class BulletMovement : MonoBehaviour {
    /// <summary>
    /// Determines whether or not the bullet has hit an interactive wall.
    /// </summary>
	public bool isPlaced;

    /// <summary>
    /// The speed at which the bullet hits a wall.
    /// </summary>
	public float speed;

    /// <summary>
    /// The direction the bullet should fire in.
    /// </summary>
    private float direction;
	Rigidbody bulletRigidbody;

	// Use this for initialization
	void Start ()
    {
		bulletRigidbody = GetComponent<Rigidbody>();
        PlaceBasedOnDirection();
	}

    /// <summary>
    /// When the bullet is spawned, this will spawn it in the correct locationr relative to the player.
    /// </summary>
    private void PlaceBasedOnDirection()
    {
        if (direction == 0)
            transform.position = new Vector3(transform.position.x + .6f, transform.position.y, transform.position.z);
        else if (direction == 90)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - .6f);
        else if (direction == 180)
            transform.position = new Vector3(transform.position.x - .6f, transform.position.y, transform.position.z);
        else if (direction == 270)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + .6f);
    }

    void OnCollisionEnter(Collision col)
    {
		if (col.gameObject.name == "Bullet(Clone)" && isPlaced)
        {
			Debug.Log("It's another me!");
			Destroy(col.gameObject);
		}
        else if (col.gameObject.tag == "Interactable Wall")
        {
			Debug.Log("ITS BIGGGGG");
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y*3, transform.localScale.z*2);
			isPlaced = true;
		}
        else if (col.gameObject.tag == "Player")
			col.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        else
			Destroy(this.gameObject);
	}

	// Update is called once per frame
	void Update ()
    {
		if (!isPlaced)
        {
			if (direction == 0)
				transform.Translate(new Vector3(1,0,0) * speed * Time.deltaTime);
			else if (direction == 90)
				transform.Translate(new Vector3(1,0,0) * speed * Time.deltaTime);
			else if (direction == 180)
				transform.Translate(new Vector3(1,0,0) * speed * Time.deltaTime);
			else if (direction == 270)
				transform.Translate(new Vector3(1,0,0) * speed * Time.deltaTime);
			
		}
	}

    /// <summary>
    /// Sets the direction of the bullet relative to player.
    /// </summary>
    /// <param name="dir"></param>
    internal void SetDirection(float dir)
    {
        direction = dir;
    }
}
