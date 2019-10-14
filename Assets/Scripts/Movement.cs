using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public bool mUp;
    public bool mDown;
    public bool mLeft;
    public bool mRight;
    public Rigidbody2D rb2d;
    public float speed = 50.0f;
    public float slowRate = 0.9f;
    private Vector2 mouseDirection;

	void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        checkKeys();
        faceMouse();
        controlSpeed();
    }

    void controlSpeed() {
        rb2d.velocity = new Vector2(rb2d.velocity.x * slowRate, rb2d.velocity.y * slowRate);
        if (rb2d.velocity.x > 10.0f) {
            rb2d.velocity = new Vector2(10.0f, rb2d.velocity.y);
        } else if (rb2d.velocity.x < -10.0f) {
            rb2d.velocity = new Vector2(-10.0f, rb2d.velocity.y);
        }
        if (rb2d.velocity.y > 10.0f) {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 10.0f);
        } else if (rb2d.velocity.y < -10.0f) {
            rb2d.velocity = new Vector2(rb2d.velocity.x, -10.0f);
        }
    }

    void faceMouse() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDirection = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        transform.up = -mouseDirection;
    }

    void checkKeys() {
        if (Input.GetKeyDown(KeyCode.W)) {
            mUp = true;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            mDown = true;
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            mLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            mRight = true;
        }
        if (Input.GetKeyUp(KeyCode.W)) {
            mUp = false;
        }
        if (Input.GetKeyUp(KeyCode.S)) {
            mDown = false;
        }
        if (Input.GetKeyUp(KeyCode.A)) {
            mLeft = false;
        }
        if (Input.GetKeyUp(KeyCode.D)) {
            mRight = false;
        }

        if (mUp) {
            rb2d.AddForce(new Vector2(0, speed));
        }
        if (mDown) {
            rb2d.AddForce(new Vector2(0, -speed));
        }
        if (mLeft) {
            rb2d.AddForce(new Vector2(-speed, 0));
        }
        if (mRight) {
            rb2d.AddForce(new Vector2(speed, 0));
        }
    }
}
