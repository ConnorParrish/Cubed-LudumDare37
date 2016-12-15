using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {
	public float direction;
	public float speed;
	public bool isBig;
	Rigidbody bulletRigidbody;

	// Use this for initialization
	void Start () {
		bulletRigidbody = GetComponent<Rigidbody>();
		if (direction == 0){
			transform.position = new Vector3(transform.position.x + .6f, transform.position.y, transform.position.z);
		}
		else if (direction == 90){
			transform.position = new Vector3(transform.position.x, transform.position.y,transform.position.z - .6f);
		}
		else if (direction == 180){
			transform.position = new Vector3(transform.position.x -.6f, transform.position.y, transform.position.z);
		}
		else if (direction == 270){
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + .6f);
		}
	}
	
	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "Bullet(Clone)" && isBig){
			Debug.Log("It's another me!");
			Destroy(col.gameObject);
		} else if (col.gameObject.tag == "Interactable Wall"){
			Debug.Log("ITS BIGGGGG");
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y*3, transform.localScale.z*2);
			isBig = true;
		} else if (col.gameObject.tag == "Player"){
			col.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		} else {
			Destroy(this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (!isBig){
			if (direction == 0){
				transform.Translate(new Vector3(1,0,0) * speed * Time.deltaTime);
			}
			else if (direction == 90){
				transform.Translate(new Vector3(1,0,0) * speed * Time.deltaTime);
			}
			else if (direction == 180){
				transform.Translate(new Vector3(1,0,0) * speed * Time.deltaTime);
			}
			else if (direction == 270){
				transform.Translate(new Vector3(1,0,0) * speed * Time.deltaTime);
			}
			
		}
	}
}
