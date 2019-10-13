using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float noticeDistance;
    //public Rigidbody2D rigidbody;
    private Transform playerPos;
    private Transform doorPos;
    private player player;
    public GameObject effect;
    void Start()
    {
        doorPos = GameObject.FindGameObjectWithTag("Door").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    
    void Update()
    {
       if(Vector2.Distance(transform.position, playerPos.position) <= noticeDistance){
           if(Vector2.Distance(transform.position, playerPos.position) > stoppingDistance){
                 transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
            }
           else if(Vector2.Distance(transform.position, playerPos.position) > Vector2.Distance(transform.position, doorPos.position)){
              transform.position = Vector2.MoveTowards(transform.position, doorPos.position, speed * Time.deltaTime);
           }
        }   
    }
    void OnTriggerEnter2D(Collider2D other)
    {
     if(other.CompareTag("Player")){
         Instantiate(effect, transform.position, Quaternion.identity);
         player.health--;
         Debug.Log(player.health);
         Destroy(gameObject);
     }   
    else if(other.CompareTag("Bullets")){
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(other.gameObject);
        Destroy(gameObject);
     }   
   // if(other.CompareTag("PlayerWeapon")){
       //rigidbody.angularVelocity = Vector3.zero;
     //}   
    }
}
