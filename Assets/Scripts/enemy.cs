using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private EnemySpawn maxenemy;
    public float speed;
    public int Enemyhp;
    public float stoppingDistance;
    public float noticeDistance;
    private Rigidbody2D rigidbody;
    private Transform playerPos;
    private Transform doorPos;
    private PlayerBehaviour player;
    public GameObject effect;
    public GameObject effect2;
 
    public AudioClip hitsound;
    public AudioClip death;
    public AudioSource aSource;
    void Start()
    {
        maxenemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemySpawn>();
        doorPos = GameObject.FindGameObjectWithTag("Door").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
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
         Enemyhp--;
         aSource.PlayOneShot(death);
         aSource.Play();
         Debug.Log(player.health);
         if (Enemyhp == 0){
         Destroy(gameObject);
         maxenemy.maxEnemyCount--;
         Instantiate(effect2, transform.position, Quaternion.identity);
         }
     }

    else if(other.CompareTag("Bullets")){
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(other.gameObject);
        Enemyhp--;
        aSource.PlayOneShot(hitsound);
        aSource.Play();
        if (Enemyhp <= 0){
         Destroy(gameObject);
         maxenemy.maxEnemyCount--;
         aSource.PlayOneShot(death);
          aSource.Play();
         Instantiate(effect2, transform.position, Quaternion.identity);
            if(maxenemy.maxEnemyCount <= 0){
                FindObjectOfType<GameManager>().GenocideLevel();
            }
         }
     }   
    if(other.CompareTag("Weapon")){
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
     }   
    }
}
