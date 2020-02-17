using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnumBullet : MonoBehaviour
{
    public float bulletSpeed = 20.0f;
      void OnTriggerEnter2D(Collider2D collider)
      {
            if (collider.gameObject.tag == "Obstacle") {
            Destroy(gameObject);
        }
      }
}
