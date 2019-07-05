using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private GroundControl _groundChecker;
    private StrafeControl _strafeChecker;
    private Rigidbody _rb;
    private Vector3 _boxOffset;
    private Transform _movedBox;
    private Transform _prevParent;

    private void Awake()
    {
        _boxOffset = new Vector3(1, 0);
        _groundChecker = GetComponent<GroundControl>();
        _strafeChecker = GetComponent<StrafeControl>();
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
        if (_strafeChecker.CanRight)
            transform.position += transform.right * Time.deltaTime * speed;
        else if (_strafeChecker.CanCarryRight)
        {
            _movedBox = _strafeChecker.GetGORight();
            if (_movedBox.tag == "Box")
            {
                Vector3 offset = transform.right * Time.deltaTime * speed / 3;
                transform.position += offset;
                _movedBox.position += offset;
                /*
                    if (Physics.Raycast(raySource2.position + _boxOffset, Vector3.right, out RaycastHit rayHitFromBox))
                    {
                        if (rayHitFromBox.distance > 0.05f)
                        {
                            _rb.MovePosition(transform.position + transform.right * Time.deltaTime * speed / 3);
                            rayHit.transform.GetComponent<Rigidbody>().MovePosition(rayHit.transform.position + transform.right * Time.deltaTime * speed / 3);
                        }
                    }
                    */
            }
        }
    }

    private void MoveLeft()
    {
        if (_strafeChecker.CanLeft)
            transform.position -= transform.right * Time.deltaTime * speed;
        else if(_strafeChecker.CanCarryLeft)
        {
            _movedBox = _strafeChecker.GetGOLeft    ();
            if (_movedBox.tag == "Box")
            {
                Vector3 offset = -transform.right * Time.deltaTime * speed / 3;
                transform.position += offset;
                _movedBox.position += offset;
                /*
                    if (Physics.Raycast(raySource.position - _boxOffset, Vector3.left, out RaycastHit rayHitFromBox))
                    {
                        if (rayHitFromBox.distance > 0.05f)
                        {
                            _rb.MovePosition(transform.position - transform.right * Time.deltaTime * speed / 3);
                            rayHit.transform.GetComponent<Rigidbody>().MovePosition(rayHit.transform.position - transform.right * Time.deltaTime * speed / 3);
                        }
                    }
                    */
            }
        }
    }
}
