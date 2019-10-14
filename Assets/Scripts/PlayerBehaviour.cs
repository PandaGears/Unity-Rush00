using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    public Weapon weapon;
    public Collider2D droppedWeapon;
    public int health = 1;

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
            if (droppedWeapon != null && weapon == null) {
                weapon = droppedWeapon.GetComponent<Weapon>();
                weapon.equip(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Weapon") {
            if (weapon != null) {
                if (weapon.GetType() == collider.gameObject.GetComponent<Weapon>().GetType()) {
                    if (weapon.ammo != weapon.maxAmmo) {
                        weapon.ammo = weapon.maxAmmo;
                        Destroy(collider.gameObject);
                    }
                }
            } else {
                droppedWeapon = collider;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Weapon") {
            droppedWeapon = null;
        }
    }
}
