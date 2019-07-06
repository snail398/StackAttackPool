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
    private int _startBoxMass = 1000000;

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
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            TakeOffBox();
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
            if (_movedBox == null)
                _movedBox = _strafeChecker.GetGORight();
            if (_movedBox.tag == "Box")
            {
                if (_movedBox.GetComponent<StrafeControl>().CanRight)
                {
                    Vector3 offset = transform.right * Time.deltaTime * speed / 3;
                    transform.position += offset;
                    _movedBox.position += offset;
                }
            }
        }
    }

    private void TakeOffBox()
    {
        if (_movedBox != null )
        {
            if ( _movedBox.tag == "Box")
                _movedBox.GetComponent<Rigidbody>().mass = _startBoxMass;
            _movedBox = null;
        }
    }

    private void MoveLeft()
    {
        if (_strafeChecker.CanLeft)
            transform.position -= transform.right * Time.deltaTime * speed;
        else if(_strafeChecker.CanCarryLeft)
        {
            if (_movedBox == null)
                _movedBox = _strafeChecker.GetGOLeft();
            if (_movedBox.tag == "Box")
            {
                if (_movedBox.GetComponent<StrafeControl>().CanLeft)
                {
                    _movedBox.GetComponent<Rigidbody>().mass = _startBoxMass / 100;
                    Vector3 offset = -transform.right * Time.deltaTime * speed / 3;
                    transform.position += offset;
                    _movedBox.position += offset;
                }
            }
        }
    }
}
