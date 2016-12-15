using UnityEngine;
using System.Collections;

public class CharacterHold : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		Debug.Log("You're on a moving platform");
		col.transform.parent = transform;
		
	}

	void OnTriggerExit(Collider col){
		Debug.Log("You're no longer on a moving platform");
		col.transform.parent = null;
	}
}
