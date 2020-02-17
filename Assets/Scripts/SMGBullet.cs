using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGBullet : MonoBehaviour
{
    public float bulletSpeed = 15.0f;

  void OnTriggerEnter2D(Collider2D collider)
      {
            if (collider.gameObject.tag == "Obstacle") {
            Destroy(gameObject);
        }
      }
}
