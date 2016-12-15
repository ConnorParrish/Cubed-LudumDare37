using UnityEngine;
using System.Collections;

public class PressurePadScript : MonoBehaviour {
	public GameObject target;
	public Vector3 startMarker;
	public Vector3 endMarker;
	public Vector3 changeInPosition;
	public bool activated;

	public bool MovingWall;
	public bool HorizontalPlatform;
	public bool VerticalPlatform;
	
	private Vector3 activatedElevation;
	private float localTime;

	// Use this for initialization
	void Start () {
		startMarker = target.transform.position;
		
		if (changeInPosition != Vector3.zero){
			endMarker = new Vector3(target.transform.position.x + changeInPosition.x, target.transform.position.y + changeInPosition.y, target.transform.position.z + changeInPosition.z);		
		}

		activatedElevation = new Vector3(transform.position.x, transform.position.y -0.1f, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (activated){
			if (HorizontalPlatform){
				// Keeps track of the time the pressure pad is active onwards
				localTime += Time.deltaTime; 
				// Make the platform move...overtime...from here.....to here...slow down at each side......and go back and forth
				target.transform.position = Vector3.Lerp(startMarker, endMarker, Mathf.SmoothStep(0f,1f,Mathf.PingPong(localTime/4f, 1f)));
			} else if (MovingWall){
				// Make the platform move...overtime...from its original spot.....to its end...at this speed
				target.transform.position = Vector3.Lerp(target.transform.position, endMarker, 0.05f);
			}
//			else 
		}
	}

	void OnCollisionStay(Collision col){
		if (col.gameObject.tag == "Player"){
			StartCoroutine("PressureMovement", 1f);
		}
	}

	IEnumerator PressureMovement(float waitTime){
		while (true){
			yield return new WaitForSeconds(waitTime);
			transform.position = activatedElevation;
			activated = true;
			return false;
		}

	}
}
