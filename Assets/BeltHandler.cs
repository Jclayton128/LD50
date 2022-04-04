using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltHandler : MonoBehaviour
{
    SoundController _sc;
    [SerializeField] BoxCollider2D _conveyorArea = null;

    //settings
    [SerializeField] Vector2 _moveForce = new Vector2(1, 0);
    [SerializeField] Vector3 _raisedOffset = new Vector2(0, 1);
    [SerializeField] float _raiseSpeed = 2f;
    float _angleRaised = 60f;

    //state
    List<Collider2D> _trashOnBelt = new List<Collider2D>();
    ContactFilter2D _cf2d = new ContactFilter2D();
    bool _shouldBeRaised = false;
    bool _isRaised = false;
    bool _isStopped = true;
    bool _isBurning = false;
    Vector3 _raisedPosition;
    Vector3 _loweredPosition;
    float _zRot = 0;

    private void Awake()
    {
        _sc = FindObjectOfType<SoundController>();
    }

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
                //_zRot = Mathf.Lerp(transform.rotation.eulerAngles.z, _angleRaised, Time.deltaTime * _raiseSpeed);
                //transform.rotation = Quaternion.Euler(0, 0, _zRot);
                //if (Mathf.Abs(transform.rotation.eulerAngles.z - _angleRaised) < Mathf.Epsilon)
                //{
                //    _isStopped = true;
                //}
                transform.position = Vector3.MoveTowards(transform.position, _raisedPosition, _raiseSpeed * Time.deltaTime);
                if ((transform.position - _raisedPosition).magnitude < Mathf.Epsilon)
                {
                    _isStopped = true;
                }
            }
            else
            {
                //_zRot = Mathf.Lerp(transform.rotation.eulerAngles.z, 0f, Time.deltaTime * _raiseSpeed);
                //transform.rotation = Quaternion.Euler(0, 0, _zRot);
                //if (Mathf.Abs(transform.rotation.eulerAngles.z - 0) < Mathf.Epsilon)
                //{
                //    _isStopped = true;
                //}
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
        if (_isBurning) return;
        _shouldBeRaised = !_shouldBeRaised;
        _isStopped = false;
        if (_shouldBeRaised)
        {
            _sc.PlaySound(7);
        }
        if (!_shouldBeRaised)
        {
            _sc.PlaySound(8);
        }
        //Debug.Log($"belt should now be raised {_shouldBeRaised} ");
    }

    public void ForceDown()
    {
        _isBurning = true;
        _shouldBeRaised = false;
        _isStopped = false;
        _sc.PlaySound(8);
    }

    public void StartBurn()
    {
        _isBurning = true;
        _isStopped = false;
        _shouldBeRaised = false;
    }

    public void StopBurn()
    {
        _isBurning = false;
    }
}
