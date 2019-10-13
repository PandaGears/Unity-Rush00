using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    public Weapon weapon;

    [SerializeField]
    private Transform GunTip;

    [SerializeField]
    public GameObject bullet;

    public Vector2 bulletPos;
    public Collider2D droppedWeapon;
    public float fireRate = 0.5f;
    public float nextShot = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && weapon != null) {
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
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Weapon") {
            droppedWeapon = null;
        }
    }
}
