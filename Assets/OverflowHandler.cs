using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class OverflowHandler : MonoBehaviour
{
    [SerializeField] SpriteRenderer _dialSR;
    [SerializeField] Sprite[] _needlePos;
    [SerializeField] TextMeshPro _TMP;
    GameController _gc;

    //Settings
    float _graceTime = 3f;
    float _timeBetweenFlash = 0.6f;

    //state
    public int count = 0;
    bool _isInJeapordy = false;
    float _timeForFail;
    float _timeToSwapFlash;
    bool _flashIsOn = false;

    private void Start()
    {
        _gc = FindObjectOfType<GameController>();
        Reset();
    }

    public void Reset()
    {
        count = 0;
        _TMP.color = Color.white;
        _dialSR.sprite = _needlePos[count];
        _isInJeapordy = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (_isInJeapordy)
        {
            if (Time.time >= _timeToSwapFlash)
            {
                if (_flashIsOn)
                {
                    _TMP.color = Color.white;
                }
                else
                {
                    _TMP.color = Color.red;
                }
                _flashIsOn = !_flashIsOn;
                _timeToSwapFlash = Time.time + _timeBetweenFlash;
            }

            if (Time.time >= _timeForFail)
            {
                _gc.HandleGameOver();
                
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        count++;
        _dialSR.sprite = _needlePos[count];
        if (count >= _needlePos.Length -1)
        {
            Debug.Log("in Jeapordry");
            _isInJeapordy = true;
            _timeForFail = Time.time + _graceTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        count--;
        _dialSR.sprite = _needlePos[count];
        if (count < _needlePos.Length - 2)
        {
            _isInJeapordy = false;

        }
    }
}
