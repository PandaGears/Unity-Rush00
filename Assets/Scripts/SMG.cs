using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Weapon
{
    public GameObject Owner;
    public AudioClip fireA;
    public AudioClip throwA;
    public AudioClip pickupA;
    public AudioSource aSource;
    [SerializeField]
    public Transform GunTip;

    [SerializeField]
    public GameObject Bullet;
    private Rigidbody2D rb2d;
    public float slowRate = 0.92f;
    public float throwSpeed = 45f;
    public float offsetX = -0.15f;
    public float offsetY = -0.35f;
    public float offsetRot = -90.0f;
    public int dps = 150;
    public SMG() : base(60, 60, 0.06f, 0.0f) {}

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Owner == null) {
            rb2d.velocity = new Vector2(rb2d.velocity.x * slowRate, rb2d.velocity.y * slowRate);
            transform.Rotate(0, 0, dps * Time.deltaTime);
            if (dps > 150) {
                dps -= 5;
            }
            if (Mathf.Abs(rb2d.velocity.y) + Mathf.Abs(rb2d.velocity.x) <= 1) {
                GetComponent<CapsuleCollider2D>().isTrigger = true;
            }
        }
    }

    public override void fire() {
        if (Time.time > nextShot && ammo != 0) {
            nextShot = Time.time + fireRate;
            GameObject firedBullet = Instantiate(Bullet, GunTip.position, GunTip.rotation);
            aSource.PlayOneShot(fireA);
            aSource.Play();
            firedBullet.GetComponent<Rigidbody2D>().velocity = GunTip.right * firedBullet.GetComponent<SMGBullet>().bulletSpeed;
            if (ammo > 0) {
                ammo -= 1;
            }
        }
    }

    public override void toss() {
        rb2d.simulated = true;
        GetComponent<CapsuleCollider2D>().isTrigger = false;
        transform.SetParent(null, true);
        Owner.GetComponent<PlayerBehaviour>().weapon = null;
        Owner = null;
        aSource.PlayOneShot(throwA);
        aSource.Play();
        rb2d.velocity = transform.right * throwSpeed;
        dps = 750;
    }

    public override void equip(GameObject owner) {
        rb2d.simulated = false;
        this.Owner = owner;
        transform.position = owner.transform.position;
        transform.parent = owner.transform;
        aSource.PlayOneShot(pickupA);
        aSource.Play();
        transform.localPosition = new Vector3(offsetX, offsetY, 0);
        CopyRotation(owner, new Vector3(0, 0, offsetRot));
    }
}