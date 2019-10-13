using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerBehaviour : MonoBehaviour
{
    private bool haswon = false;
    private bool haslost = false;
    [SerializeField]
    public int health = 1; 

    public Weapon weapon;
    public Text Ammodeets;

    public Text weapondeets;

    [SerializeField]
    private Transform GunTip;

    [SerializeField]
    public GameObject bullet;

    public Vector2 bulletPos;
    public Collider2D droppedWeapon;
    // public float fireRate = 0.5f;
    // public float nextShot = 0.0f;
    public GameObject effect;
    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0) && weapon != null) {
            weapondeets.text = "WEAPON : " + weapon;
		    Ammodeets.text = "AMMO : ???" ; 
            weapon.fire();
        }

        if (Input.GetKey(KeyCode.Mouse1) && weapon != null) {
            weapon.toss();
        }

        if (Input.GetKey(KeyCode.E)) {
            if (droppedWeapon != null) {
                weapon = droppedWeapon.GetComponent<Weapon>();
                weapon.equip(gameObject);
    
    
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Weapon") {
            droppedWeapon = collider;
        }
       else if(collider.gameObject.tag == "Enemy"){
            Instantiate(effect, transform.position, Quaternion.identity);
             health--;
             Debug.Log(health);
            Destroy(gameObject);
        }
        else if (collider.gameObject.tag == "goal") {
            haswon = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Weapon") {
            droppedWeapon = null;
        }
    }
}
