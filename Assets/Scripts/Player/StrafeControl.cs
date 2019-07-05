using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeControl : MonoBehaviour
{
    [SerializeField] private Transform leftBot;
    [SerializeField] private Transform leftTop;
    [SerializeField] private Transform rightTop;
    [SerializeField] private Transform rightBot;
    [SerializeField] private float cap = 1;

    private RaycastHit _leftBotHit;
    private RaycastHit _rightBotHit;

    private RaycastHit _leftTopHit;
    private RaycastHit _rightTopHit;

    public bool CanLeft
    {
        get
        {
            return !(_leftBotHit.distance < cap || _leftTopHit.distance < cap);
        }
    }
    public bool CanRight
    {
        get
        {
            return !(_rightBotHit.distance < cap || _rightTopHit.distance < cap);
        }
    }

    public bool CanCarryLeft
    {
        get
        {
            return (_leftBotHit.distance < cap && _leftTopHit.distance > cap);
        }
    }
    public bool CanCarryRight
    {
        get
        {
            return (_rightBotHit.distance < cap && _rightTopHit.distance > cap);
        }
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(leftBot.position, Vector3.left, out RaycastHit rayHit))
        {
            _leftBotHit = rayHit;
        }
        if (Physics.Raycast(leftTop.position, Vector3.left, out RaycastHit rayHit2))
        {
            _leftTopHit = rayHit2;
        }
        if (Physics.Raycast(rightTop.position, Vector3.right, out RaycastHit rayHit3))
        {
            _rightTopHit = rayHit3;
        }
        if (Physics.Raycast(rightBot.position, Vector3.right, out RaycastHit rayHit4))
        {
            _rightBotHit = rayHit4;
        }
    }

    public Transform GetGOLeft()
    {
        if (Physics.Raycast(leftBot.position, Vector3.left, out RaycastHit rayHit))
        {
            return rayHit.transform;
        }
        return null;
    }

    public Transform GetGORight()
    {
        if (Physics.Raycast(rightBot.position, Vector3.right, out RaycastHit rayHit4))
        {
            return rayHit4.transform;
        }
        return null;
    }
}
