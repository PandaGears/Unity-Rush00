using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   	public float speed;
	private Rigidbody2D rBody;
	private Vector2 moveVelocity;
	public LayerMask ObstacleMask;
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
		Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		moveVelocity = moveInput.normalized * speed;
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);


		// Vector3 Move = Vector3.zero;
		// if (Input.GetKey (KeyCode.W))
		// 	Move += Vector3.up;			
		// if (Input.GetKey (KeyCode.S))
		// 	Move += Vector3.down;			
		// if (Input.GetKey (KeyCode.A))
		// 	Move += Vector3.left;			
		// if (Input.GetKey (KeyCode.D))
		// 	Move += Vector3.right;

		// if (Move == Vector3.zero)
		// 	legs.GetComponent<Animator> ().SetBool ("isMoving", false);
		// else
		// 	changePosition (Move);

			
	}
	// 	void changePosition(Vector3 direction) {
	// 	float step = speed * Time.deltaTime;
	// 	transform.position = Vector3.MoveTowards (transform.position, transform.position + direction, step);
	// 	legs.GetComponent<Animator>().SetBool ("isMoving", true);
	// }

	void FixedUpdate() {
		rBody.MovePosition(rBody.position + moveVelocity * Time.fixedDeltaTime);
	}
}
