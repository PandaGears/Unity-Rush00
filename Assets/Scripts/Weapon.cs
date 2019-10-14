using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int maxAmmo;
    public int ammo;
    public float fireRate;
    public float nextShot;

    public Weapon(int maxAmmo, int ammo, float fireRate, float nextShot) {
        this.maxAmmo = maxAmmo;
        this.ammo = ammo;
        this.fireRate = fireRate;
        this.nextShot = nextShot;
    }
    public abstract void fire();
    public abstract void toss();
    public abstract void equip(GameObject owner);

    public void rotateWeapon() {

    }

    public void CopyRotation(GameObject Target, Vector3 Offset = default(Vector3)) {
        transform.rotation = Quaternion.Euler(Offset) * Target.transform.localRotation;
    }
}
