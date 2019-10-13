using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGBullet : MonoBehaviour
{
    public float bulletSpeed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3.0f);
    }
}
