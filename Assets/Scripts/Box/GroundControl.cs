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

    private void OnEnable()
    {
        _colliderList.Clear();
    }

    public bool OnGround;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        OnGround = IsGrounded;
        if (_colliderList.Count != 0)
            foreach (Collider otherCollider in _colliderList)
            {
                if (otherCollider.bounds.min.x == _collider.bounds.max.x || otherCollider.bounds.max.x == _collider.bounds.min.x || otherCollider.transform.gameObject.activeSelf == false)
                {
                    RemoveFromList(otherCollider);
                    break;
                }
            }
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (!_colliderList.Contains(contact.otherCollider) && Mathf.Abs(contact.point.y - _collider.bounds.min.y) < 0.01f && Mathf.Abs(contact.point.y - contact.otherCollider.bounds.max.y) < 0.01f)
            {
                _colliderList.Add(contact.otherCollider);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        RemoveFromList(collision.collider);
    }

    private void RemoveFromList(Collider collider)
    {
        if (_colliderList.Contains(collider))
            _colliderList.Remove(collider);
    }
}
