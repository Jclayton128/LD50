using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverflowHandler : MonoBehaviour
{
    [SerializeField] SpriteRenderer _dialSR;
    [SerializeField] Sprite[] _needlePos;

    //state
    int count = 0;

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        count++;
        _dialSR.sprite = _needlePos[count];
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        count--;
        _dialSR.sprite = _needlePos[count];
    }
}
