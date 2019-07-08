using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeControl : MonoBehaviour
{
    [SerializeField] private Transform leftBot;
    [SerializeField] private Transform rightBot;
    [SerializeField] private float cap = 1;


    private List<Collider> _leftList = new List<Collider>();
    private List<Collider> _rightList = new List<Collider>();
    private List<Collider> _leftCarryList = new List<Collider>();
    private List<Collider> _rightCarryList = new List<Collider>();
    private Collider _collider;

    public bool CanMoveLeft;
    public bool CanMoveRight;
    public bool CanCarryLeft1;
    public bool CanCarryRight2;

    public bool CanLeft
    {
        get
        {
            return _leftList.Count == 0;
        }
    }
    public bool CanRight
    {
        get
        {
            return _rightList.Count == 0;
        }
    }

    public bool CanCarryLeft
    {
        get
        {
            return _leftCarryList.Count == 0;
        }
    }
    public bool CanCarryRight
    {
        get
        {
            return _rightCarryList.Count == 0;
        }
    }
    private void OnEnable()
    {
        ClearList(_leftList);
        ClearList(_rightList);
        ClearList(_leftCarryList);
        ClearList(_rightCarryList);
    }
    
    private void Update()
    {
        CanMoveLeft = CanLeft;
        CanMoveRight = CanRight;
        CanCarryLeft1 = CanCarryLeft;
        CanCarryRight2 = CanCarryRight;
    }

    private void FixedUpdate()
    {
        if (_leftList.Count != 0)
            foreach (Collider otherCollider in _leftList)
            {
                if (((otherCollider.bounds.min.x == _collider.bounds.max.x || otherCollider.bounds.max.x == _collider.bounds.min.x) && Mathf.Abs(otherCollider.bounds.max.y - _collider.bounds.min.y) < 0.1f) || otherCollider.transform.gameObject.activeSelf == false)
                {
                    RemoveFromList(_leftList, otherCollider);
                    break;
                }
            }
        if (_rightList.Count != 0)
            foreach (Collider otherCollider in _rightList)
            {
                if (((otherCollider.bounds.min.x == _collider.bounds.max.x || otherCollider.bounds.max.x == _collider.bounds.min.x) && Mathf.Abs(otherCollider.bounds.max.y -_collider.bounds.min.y) < 0.1f) || otherCollider.transform.gameObject.activeSelf == false)
                {
                    RemoveFromList(_rightList, otherCollider);
                    break;
                }
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
            // if (Mathf.Abs(contact.point.y - _collider.bounds.min.y) > 0.1f)
            if (Mathf.Abs(contact.point.y - contact.otherCollider.bounds.max.y) > 0.1f)
            {
                if (contact.point.x < _collider.bounds.center.x)
                {
                    AddToList(_leftList, contact.otherCollider);
                    if (contact.point.y > _collider.bounds.center.y + 0.5f * _collider.bounds.extents.y)
                        AddToList(_leftCarryList, contact.otherCollider);
                }
                else
                {
                    AddToList(_rightList, contact.otherCollider);
                    if (contact.point.y > _collider.bounds.center.y + 0.5f * _collider.bounds.extents.y)
                        AddToList(_rightCarryList, contact.otherCollider);
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        RemoveFromList(_leftList, collision.collider);
        RemoveFromList(_rightList, collision.collider);
        RemoveFromList(_leftCarryList, collision.collider);
        RemoveFromList(_rightCarryList, collision.collider);
    }

    private void AddToList(List<Collider> list, Collider col)
    {
        if(!list.Contains(col))
            list.Add(col);
    }

    private void RemoveFromList(List<Collider> list, Collider col)
    {
        if (list.Contains(col))
            list.Remove(col);
    }
    
    private void ClearList(List<Collider> list)
    {
        list.Clear();
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
