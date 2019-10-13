using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float noticeDistance;
    private Transform playerPos;
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    
    void Update()
    {
                if(Vector2.Distance(transform.position, playerPos.position) > noticeDistance){        {
                   if(Vector2.Distance(transform.position, playerPos.position) > stoppingDistance){
                        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
                    }
                }   
            }
}
}
