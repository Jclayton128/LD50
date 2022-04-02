using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //state
    public bool IsInCoreGame { get; private set; } = true;
    public bool IsPaused { get; private set; } = false;

}
