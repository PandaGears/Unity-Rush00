using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
public float speed = 5F;
    void Start()
    {
        
    }
	void changePosition(Vector3 direction) {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, transform.position + direction, step);
	
	}
    void Update()
    {
		Vector3 Move = Vector3.zero;
		if (Input.GetKey (KeyCode.W))
			Move += Vector3.up;			
		if (Input.GetKey (KeyCode.S))
			Move += Vector3.down;			
		if (Input.GetKey (KeyCode.A))
			Move += Vector3.left;			
		if (Input.GetKey (KeyCode.D))
			Move += Vector3.right;
        		changePosition (Move);
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);  
    }
}
