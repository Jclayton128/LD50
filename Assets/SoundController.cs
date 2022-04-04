using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    GameController _gc;
    AudioSource _auso;

    [Tooltip ("0: ")]
    [SerializeField] AudioClip[] _clips;

    [SerializeField] AudioClip[] _landingClips = null;

    void Awake()
    {
        _gc = GetComponent<GameController>();
        _auso = GetComponent<AudioSource>();
    }
    
    public void PlaySound(int index)
    {
        if (_gc.IsMuted) return;
        _auso.PlayOneShot(_clips[index]);
    }

    public void PlayLandingSound()
    {
        if (_gc.IsMuted) return;
        int odds = UnityEngine.Random.Range(0, 6);
        if (odds >= 2) { return; }

        int rand = UnityEngine.Random.Range(0,_landingClips.Length);
        _auso.PlayOneShot(_landingClips[rand]);
    }
}
