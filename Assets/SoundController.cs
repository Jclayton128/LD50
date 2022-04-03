using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    GameController _gc;
    AudioSource _auso;

    [Tooltip ("0: ")]
    [SerializeField] AudioClip[] _clips;

    void Awake()
    {
        _gc = GetComponent<GameController>();
        _auso = GetComponent<AudioSource>();
    }
    
    public void PlaySound(int index)
    {
        _auso.PlayOneShot(_clips[0]);
    }
}
