using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    TrashController _tc;
    BinHandler[] _bhs;
    OverflowHandler _oh;
    [SerializeField] TextMeshPro _blurbTMP = null;
    [SerializeField] Sprite[] _SFXbuttons = null;
    [SerializeField] Image _SFXSR = null;

    //state
    public bool IsInCoreGame { get; private set; } = true;
    public bool IsPaused { get; private set; } = false;

    public bool IsMuted { get; private set; } = false;

    private void Start()
    {
        _bhs = FindObjectsOfType<BinHandler>();
        _tc = GetComponent<TrashController>();
        _oh = FindObjectOfType<OverflowHandler>();
        HandleResetGame();
    }

    public void ToggleSFX()
    {
        IsMuted = !IsMuted;
        if (IsMuted)
        {
            _SFXSR.sprite = _SFXbuttons[0];
        }
        else
        {
            _SFXSR.sprite = _SFXbuttons[1];
        }
    }

    public void HandleGameOver()
    {
        _blurbTMP.text = "Game Over At: ";
        PauseGame();
        IsInCoreGame = false;

    }

    public void HandleResetGame()
    {
        _blurbTMP.text = "Trash Processed: ";
        foreach (var bh in _bhs)
        {
            bh.Reset();
        }
        _tc.Reset();
        _oh.Reset();
        UnpauseGame();
    }

    private void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0;
    }

    private void UnpauseGame()
    {
        IsPaused = false;
        Time.timeScale = 1;
    }

}
