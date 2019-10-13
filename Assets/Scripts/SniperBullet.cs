using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    public float bulletSpeed = 50.0f;
  void OnTriggerEnter2D(Collider2D collider)
      {
            if (collider.gameObject.tag == "Obstacle") {
            Destroy(gameObject);
        }
      }
}
