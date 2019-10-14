using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSounds: MonoBehaviour {
     
	public List<AudioClip> Musacslist; 
	private AudioSource Music;
	void Start ()
	{
		Music = GetComponent<AudioSource> ();
	}

	void Update () {
		if (!Music.isPlaying) {
			Music.clip = Musacslist[Random.Range (0, Musacslist.Count)];
			Music.Play ();
		}
	}


}