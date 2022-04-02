using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [Tooltip ("0: A, 1: S, 2: D, 3: F")]
    [SerializeField] BeltHandler[] _flaps = null;

    // Update is called once per frame
    void Update()
    {
        CheckForAKey();
        CheckForSKey();
        CheckForDKey();
        CheckForFKey();
    }

    private void CheckForAKey()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("heard A");
            _flaps[0].ToggleRaiseCommand();
        }
    }

    private void CheckForSKey()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _flaps[1].ToggleRaiseCommand();
        }
    }

    private void CheckForDKey()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _flaps[2].ToggleRaiseCommand();
        }
    }

    private void CheckForFKey()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _flaps[3].ToggleRaiseCommand();
        }
    }
}
