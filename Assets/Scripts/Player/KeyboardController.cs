using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    [SerializeField] private Transform raySource;
    [SerializeField] private Transform raySource2;

    private GroundControl _groundChecker;
    private Rigidbody _rb;
    private Vector3 _boxOffset;

    private void Awake()
    {
        _boxOffset = new Vector3(1, 0);
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
        if (Physics.Raycast(raySource2.position, Vector3.right, out RaycastHit rayHit))
        {
            if (rayHit.distance > 0.05f)
                _rb.MovePosition(transform.position + transform.right * Time.deltaTime * speed);
            else if (rayHit.transform.gameObject.tag == "Box")
            {
                if (Physics.Raycast(raySource2.position + _boxOffset, Vector3.right, out RaycastHit rayHitFromBox))
                {
                    if (rayHitFromBox.distance > 0.05f)
                    {
                        _rb.MovePosition(transform.position + transform.right * Time.deltaTime * speed / 3);
                        rayHit.transform.GetComponent<Rigidbody>().MovePosition(rayHit.transform.position + transform.right * Time.deltaTime * speed / 3);
                    }
                }
            }
        }
    }

    private void MoveLeft()
    {
        if (Physics.Raycast(raySource.position, Vector3.left, out RaycastHit rayHit))
        {
            if (rayHit.distance > 0.05f)
                _rb.MovePosition(transform.position - transform.right * Time.deltaTime * speed);
            else if (rayHit.transform.gameObject.tag == "Box")
            {
                if (Physics.Raycast(raySource.position - _boxOffset, Vector3.left, out RaycastHit rayHitFromBox))
                {
                    if (rayHitFromBox.distance > 0.05f)
                    {
                        _rb.MovePosition(transform.position - transform.right * Time.deltaTime * speed / 3);
                        rayHit.transform.GetComponent<Rigidbody>().MovePosition(rayHit.transform.position - transform.right * Time.deltaTime * speed / 3);
                    }
                }
            }
        }
    }
}
