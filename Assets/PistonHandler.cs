using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonHandler : MonoBehaviour
{
    //settings
    [SerializeField] Vector3 _horizOffset = new Vector2(4, 0);
    [SerializeField] float _compressSpeed = 2f;

    //state
    Vector3 _compressingPosition;
    Vector3 _retractedPosition;
    bool _isCompressing = false;
    bool _isRetracting = false;


    private void Awake()
    {
        _retractedPosition = transform.position;
        _compressingPosition = transform.position + _horizOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCompressing)
        {
            transform.position = Vector3.MoveTowards(transform.position, _compressingPosition, _compressSpeed * Time.deltaTime);
            if ((transform.position - _compressingPosition).magnitude < Mathf.Epsilon)
            {
                _isCompressing = false;
                _isRetracting = true;
            }
        }
        if (_isRetracting)
        {
            transform.position = Vector3.MoveTowards(transform.position, _retractedPosition, 2* _compressSpeed * Time.deltaTime);
            if ((transform.position - _retractedPosition).magnitude < Mathf.Epsilon)
            {
                _isRetracting = false;
            }
        }
    }

    public void CommandCompress()
    {
        if (!_isRetracting)
        {
            _isCompressing = true;
        }
    }
}
