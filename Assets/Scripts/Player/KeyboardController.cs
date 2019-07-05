using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private GroundControl _groundChecker;
    private Rigidbody _rb;

    private void Awake()
    {
        _groundChecker = GetComponent<GroundControl>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            MoveLeft();
        if (Input.GetKey(KeyCode.RightArrow))
            MoveRight();
        if (Input.GetKeyDown(KeyCode.UpArrow) && _groundChecker.IsGrounded)
            Jump();
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * jumpForce);
    }

    private void MoveRight()
    {
        transform.position = new Vector2(transform.position.x + Time.deltaTime * speed, transform.position.y);
    }

    private void MoveLeft()
    {
        transform.position = new Vector2(transform.position.x - Time.deltaTime * speed, transform.position.y);
    }
}
