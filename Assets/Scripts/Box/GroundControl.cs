using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControl : MonoBehaviour
{
    [SerializeField] private Transform raySource;
    [SerializeField] private Transform raySource2;
    private RaycastHit realRayHit;
    private RaycastHit realRayHit2;

    public bool IsGrounded
    {
        get
        {
            return realRayHit.distance < 0.2f || realRayHit2.distance < 0.2f;
        }
    }
    
    private void FixedUpdate()
    {
        if (Physics.Raycast(raySource.position, Vector3.down, out RaycastHit rayHit))
        {
            realRayHit = rayHit;
        }
        if (Physics.Raycast(raySource2.position, Vector3.down, out RaycastHit rayHit2))
        {
            realRayHit2 = rayHit2;
        }
    }  
}
