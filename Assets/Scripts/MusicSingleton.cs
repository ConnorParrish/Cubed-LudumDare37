using UnityEngine;
using System.Collections;

public class MusicSingleton : MonoBehaviour {

	public static MusicSingleton Instance { get { return instance;} }
	public AudioClip music;
	public bool useAudio;

	private static MusicSingleton instance = null;
	private GameObject go;

	void Awake(){
		if (instance != null && instance != this){
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	
		GetComponent<AudioSource>().enabled = useAudio;
	
	}

}
