using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //state
    public bool IsInCoreGame { get; private set; } = true;
    public bool IsPaused { get; private set; } = false;

    public bool IsMuted { get; private set; } = false;

    public void ToggleSFX()
    {
        Debug.Log("click");
        IsMuted = !IsMuted;
        //return IsMuted;
    }

}
