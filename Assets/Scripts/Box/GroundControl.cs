using System.Collections.Generic;
using UnityEngine;

public class GroundControl : MonoBehaviour
{
    private List<Collider> _colliderList = new List<Collider>();
    private Collider _collider;
    public bool IsGrounded
    {
        get
        {
            return _colliderList.Count > 0;
        }
    }
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (!_colliderList.Contains(contact.otherCollider) && Mathf.Abs(contact.point.y - _collider.bounds.min.y) < 0.01f)
                _colliderList.Add(contact.otherCollider);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (_colliderList.Contains(collision.collider))
            _colliderList.Remove(collision.collider);
    }
}
