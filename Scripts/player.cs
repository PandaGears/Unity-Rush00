using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class player : MonoBehaviour
{
   	public float speed;

	public int health = 1; 
	private Rigidbody2D rBody;
	private Vector2 moveVelocity;
	public LayerMask ObstacleMask;
	public Text weapondeets;
	public Text Ammodeets;
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

		weapondeets.text = "WEAPON : TACOS";
		Ammodeets.text = "AMMO : PANDAS"; 

	}
	}
