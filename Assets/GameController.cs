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
    [SerializeField] GameObject[] _hideawayObjects = null;

    //state
    Color fadeout = Color.white;
    float _timeInCurrentGame = 0;
    bool _isTutorialMode = true;
    public bool IsInCoreGame { get; private set; } = true;
    public bool IsPaused { get; private set; } = false;

    public bool IsMuted { get; private set; } = false;
    List<SpriteRenderer> _hideSR  = new List<SpriteRenderer>();
    List<TextMeshPro> _hideTMPs = new List<TextMeshPro>();

    private void Start()
    {
        _bhs = FindObjectsOfType<BinHandler>();
        _tc = GetComponent<TrashController>();
        _oh = FindObjectOfType<OverflowHandler>();
        HandleResetGame();

        foreach (var obj in _hideawayObjects)
        {
            SpriteRenderer sr;

            if (obj.TryGetComponent<SpriteRenderer>(out sr))
            {
                _hideSR.Add(sr);
            }
            TextMeshPro tmp = obj.GetComponentInChildren<TextMeshPro>();
            if (tmp != null)
            {
                _hideTMPs.Add(tmp);
            }
        }

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

    private void Update()
    {
        _timeInCurrentGame += Time.deltaTime;
        if (_timeInCurrentGame > 10 && _isTutorialMode)
        {
            fadeout.a -= Time.deltaTime;
            foreach (var sr in _hideSR)
            {
                sr.color = fadeout;
            }
            foreach (var tmp in _hideTMPs)
            {
                tmp.color = fadeout;
            }

            if (fadeout.a <= 0)
            {
                _isTutorialMode = false;
            }
        }
    }

    public void HandleResetGame()
    {
        fadeout = Color.white;
        _timeInCurrentGame = 0;
        IsInCoreGame = true;
        _isTutorialMode = false;
        ShowHideTutorial();

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

    public void ShowHideTutorial()
    {
        _isTutorialMode = !_isTutorialMode;

        if (_isTutorialMode)
        {
            _timeInCurrentGame = 0;
            fadeout = Color.white;
        }
        else
        {
            fadeout.a = 0;
        }

        foreach (var sr in _hideSR)
        {
            sr.color = fadeout;
        }
        foreach (var tmp in _hideTMPs)
        {
            tmp.color = fadeout;
        }
        //foreach (var obj in _hideawayObjects)
        //{
        //    obj.SetActive(_isTutorialMode);
        //}
    }

}
