using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSounds: MonoBehaviour {

	public GameObject player;       
	public List<AudioClip> Musacslist; 
	private AudioSource Music;

	private Vector3 offset;
	void Start ()
	{
		Music = GetComponent<AudioSource> ();
	}

	void Update () {
		if (!Music.isPlaying) {
			Music.clip = Musacslist
	 [Random.Range (0, Musacslist
	.Count)];
			Music.Play ();
		}
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		if (player != null)
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
	}
}