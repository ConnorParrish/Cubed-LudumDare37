using UnityEngine;
using System.Collections;

public class ColorScript : MonoBehaviour {
	public bool isOrange = true;
	public Material bullet1;
	public Material bullet2;

	void Start(){
		if (isOrange) {
			GetComponent<Renderer>().material = bullet1;
		} else {
			GetComponent<Renderer>().material = bullet2;
		}
	}	
	// Update is called once per frame
	void Update () {
	
	}
}
