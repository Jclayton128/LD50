using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonHandler : MonoBehaviour
{
    SoundController _soundCon;

    //settings
    [SerializeField] Vector3 _horizOffset = new Vector2(4, 0);
    [SerializeField] float _compressSpeed = 2f;
    float _slowingPerTrash = 0.025f;
    [SerializeField] BoxCollider2D _pressCheck = null;

    //state
    float _currentSpeed;
    Vector3 _compressingPosition;
    Vector3 _retractedPosition;
    public bool _isCompressing = false;
    public bool _isRetracting = false;
    ContactFilter2D cf2d;
    Collider2D[] thing = new Collider2D[0];

    public int _load;


    private void Awake()
    {
        _retractedPosition = transform.position;
        _compressingPosition = transform.position + _horizOffset;
        _soundCon = FindObjectOfType<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCompressing)
        {
           
            _currentSpeed = _compressSpeed - (_slowingPerTrash * _load);
            _currentSpeed = Mathf.Clamp(_currentSpeed, _compressSpeed / 30f, _compressSpeed);
            transform.position = Vector3.MoveTowards(transform.position, _compressingPosition, _currentSpeed * Time.deltaTime);
            
            if ((transform.position - _compressingPosition).magnitude < 1)
            {
                _isCompressing = false;
                _isRetracting = true;
                _soundCon.PlaySound(4);
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
            _soundCon.PlaySound(3);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _load++;
        _load = Mathf.Clamp(_load,0, 30);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _load--;
        _load = Mathf.Clamp(_load, 0, 30);
    }
}
