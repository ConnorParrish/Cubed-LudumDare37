using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float smoothing = 5f;
	public float alphaPercent;

	Vector3 screenPos;
	Ray ray;
	Transform target;
	Vector3 offset;
	GameObject oldObject;
	Color oldColor;
	Material interactiveOpaque;
	Material interactiveTransp;
	Material nonIntOpaque;
	Material nonIntTransp;

	void Start(){
		target = GameObject.Find("Player").transform;
		offset = transform.position - target.position;
		alphaPercent *= 0.01f;
		interactiveOpaque = new Material(Resources.Load<Material>("Materials/Wall-Interactive-Opaque"));
		interactiveTransp = new Material(Resources.Load<Material>("Materials/Wall-Interactive-Transparent"));
		nonIntOpaque = new Material(Resources.Load<Material>("Materials/Wall-Non-Interactive-Opaque"));
		nonIntTransp = new Material(Resources.Load<Material>("Materials/Wall-Non-Interactive-Transparent"));
	}

	void Update(){
		RaycastHit hit;
		
		screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		Debug.DrawRay(ray.origin, ray.direction * 50, Color.yellow);
		

		if (Physics.Raycast(ray, out hit)){
			if (hit.collider.gameObject.tag != "Player"){
				oldObject = hit.collider.gameObject;
				//oldColor = oldObject.GetComponent<Renderer>().material.color;
				if (oldObject.tag == "Interactable Wall"){
					oldObject.GetComponent<Renderer>().material = interactiveTransp;
				} else {
					oldObject.GetComponent<Renderer>().material = nonIntTransp;
				}
				
				//ToTransparent(oldObject.GetComponent<Renderer>().material);
			}
			if (hit.collider.gameObject.tag == "Player"){

				if (oldObject != null){
					if (oldObject.tag == "Interactable Wall"){
						oldObject.GetComponent<Renderer>().material = interactiveOpaque;
					} else {
						oldObject.GetComponent<Renderer>().material = nonIntOpaque;
					}
					

					//ToOpaque(oldObject.GetComponent<Renderer>().material);
				}
			}
		}
	
		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}

	// void ToTransparent(Material mat){
	// 	mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
	// 	mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
	// 	mat.SetInt("_ZWrite", 0);
	// 	mat.DisableKeyword("_ALPHATEST_ON");
	// 	mat.DisableKeyword("_ALPHABLEND_ON");
	// 	mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
	// 	mat.renderQueue = 3000;
	// }

	// void ToOpaque(Material mat){
	// 	mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
	// 	mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
	// 	mat.SetInt("_ZWrite", 1);
	// 	mat.DisableKeyword("_ALPHATEST_ON");
	// 	mat.DisableKeyword("_ALPHABLEND_ON");
	// 	mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
	// 	mat.renderQueue = -1;
	// }

}
