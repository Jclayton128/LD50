using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OverflowHandler : MonoBehaviour
{
    [SerializeField] SpriteRenderer _dialSR;
    [SerializeField] Sprite[] _needlePos;
    [SerializeField] TextMeshPro _scoreTMP;

    //state
    int count = 0;
    int score = 0;

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        count = 0;
        score = 0;
        _scoreTMP.text = "Trash processed: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        count++;
        score++;
        _scoreTMP.text = "Trash processed: " + score.ToString();
        _dialSR.sprite = _needlePos[count];
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        count--;
        _dialSR.sprite = _needlePos[count];
    }
}
