using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltHandler : MonoBehaviour
{
    [SerializeField] BoxCollider2D _conveyorArea = null;

    //settings
    [SerializeField] Vector2 _moveForce = new Vector2(1, 0);
    [SerializeField] Vector3 _raisedOffset = new Vector2(0, 1);
    [SerializeField] float _raiseSpeed = 2f;

    //state
    List<Collider2D> _trashOnBelt = new List<Collider2D>();
    ContactFilter2D _cf2d = new ContactFilter2D();
    bool _shouldBeRaised = false;
    bool _isRaised = false;
    bool _isStopped = true;
    Vector3 _raisedPosition;
    Vector3 _loweredPosition;

    void Start()
    {
        _loweredPosition = transform.position;
        _raisedPosition = transform.position + _raisedOffset;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForRaiseLower();
    }

    private void CheckForRaiseLower()
    {
        if (!_isStopped)
        {
            if (_shouldBeRaised)
            {
                transform.position = Vector3.MoveTowards(transform.position, _raisedPosition, _raiseSpeed * Time.deltaTime);
                if ((transform.position - _raisedPosition).magnitude < Mathf.Epsilon)
                {
                    _isStopped = true;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _loweredPosition, _raiseSpeed * Time.deltaTime);
                if ((transform.position - _loweredPosition).magnitude < Mathf.Epsilon)
                {
                    _isStopped = true;
                }
            }


        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(_moveForce, ForceMode2D.Force);
    }

    public void ToggleRaiseCommand()
    {
        _shouldBeRaised = !_shouldBeRaised;
        _isStopped = false;
        //Debug.Log($"belt should now be raised {_shouldBeRaised} ");
    }
}
