using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed;
    public int Enemyhp;
    public float stoppingDistance;
    public float noticeDistance;
    private Rigidbody2D rb2d;
    private Transform playerPos;
    private Transform doorPos;
    private PlayerBehaviour player;
    public GameObject effect;
    public GameObject effect2;

    public AudioClip hitsound;
    public AudioClip death;
    public AudioSource aSource;
    public Weapon weapon;
    public float RotationSpeed;

    private Quaternion _lookRotation;
    private Vector3 _direction;

    void lookAtPlayer()
    {

        _direction = (playerPos.position - transform.position).normalized;

        Vector2 dir = _direction;
        transform.right = Quaternion.Euler(new Vector3(0, 0, 90)) * dir;
    }
    void Start()
    {
        doorPos = GameObject.FindGameObjectWithTag("Door").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    
    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if(Vector2.Distance(transform.position, playerPos.position) <= noticeDistance){
            lookAtPlayer();
            if(Vector2.Distance(transform.position, playerPos.position) > stoppingDistance){
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
            }
            else if(Vector2.Distance(transform.position, playerPos.position) > Vector2.Distance(transform.position, doorPos.position)){
                transform.position = Vector2.MoveTowards(transform.position, doorPos.position, speed * Time.deltaTime);
            }
            if (weapon != null) {
                weapon.fire();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (other.CompareTag("Player")) {
            Instantiate(effect, transform.position, Quaternion.identity);
            player.health--;
            Enemyhp--;
            aSource.PlayOneShot(death);
            aSource.Play();
            Debug.Log(player.health);
            if (Enemyhp == 0){
                Destroy(gameObject);
                Instantiate(effect2, transform.position, Quaternion.identity);
            }
        } else if (other.CompareTag("Bullets")) {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Enemyhp--;
            aSource.PlayOneShot(hitsound);
            aSource.Play();
            if (Enemyhp == 0){
                Destroy(gameObject);
                aSource.PlayOneShot(death);
                aSource.Play();
                Instantiate(effect2, transform.position, Quaternion.identity);
            }
        }
        if(other.CompareTag("Weapon")){
           // rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (weapon == null) {
            weapon = collider.GetComponent<Weapon>();
            weapon.equip(gameObject);
        }
    }
}