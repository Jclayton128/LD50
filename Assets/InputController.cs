using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [Tooltip ("0: A, 1: S, 2: D, 3: F")]
    [SerializeField] BeltHandler[] _flaps = null;
    [SerializeField] BinHandler[] _bins = null;
    [SerializeField] PistonHandler _piston = null;

    // Update is called once per frame
    void Update()
    {
        CheckForAKey();
        CheckForSKey();
        CheckForDKey();
        CheckForFKey();
        CheckForSpace();
    }

    private void CheckForAKey()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    _flaps[0].StartBurn();
            //    _bins[0].CommandBurn(_flaps[0]);
            //}
            //else
            //{
                _flaps[0].ToggleRaiseCommand();
            //}

        }
    }

    private void CheckForSKey()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    _flaps[1].StartBurn();
            //    _bins[1].CommandBurn(_flaps[1]);
            //}
            //else
            //{
                _flaps[1].ToggleRaiseCommand();
            //}

        }
    }

    private void CheckForDKey()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    _flaps[2].StartBurn();
            //    _bins[2].CommandBurn(_flaps[2]);
            //}
            //else
            //{
                _flaps[2].ToggleRaiseCommand();
            //}

        }
    }

    private void CheckForFKey()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    _flaps[3].StartBurn();
            //    _bins[3].CommandBurn(_flaps[3]);
            //}
            //else
            //{
                _flaps[3].ToggleRaiseCommand();
            //}

        }
    }

    private void CheckForSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _piston.CommandCompress();
        }
    }
}
