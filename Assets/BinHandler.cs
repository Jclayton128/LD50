using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinHandler : MonoBehaviour
{
    [Tooltip("0: 0%, 1: 20%, 2: 40%, 3: 60%, 4: 80%, 5: 100%")]
    [SerializeField] Sprite[] _shapeSprites = null;
    [SerializeField] SpriteRenderer _shapeSR = null;

    [Tooltip("0: 0%, 1: 20%, 2: 40%, 3: 60%, 4: 80%, 5: 100%")]
    [SerializeField] Sprite[] _colorSprites = null;
    [SerializeField] SpriteRenderer _colorSR = null;
    [SerializeField] GameObject _door = null;

    [SerializeField] GameObject _burnProductPrefab = null;
    [SerializeField] Transform _burnProductOutput = null;
    ParticleSystem _particle;
    ParticleSystem.EmissionModule _psem;

    //settings
    public bool _burnsTrash = false;

    //state
    [SerializeField] List<TrashHandler> _trashInBin = new List<TrashHandler>();
    int _trashCount = 0;
    Vector2 _shapePurityVector = Vector2.zero;
    Vector2 _colorPurityVector = Vector2.zero;
    float _currentShapePurity = 1;
    float _currentColorPurity = 1;
    [SerializeField] BeltHandler _beltHandler  = null;
    float _burnTimeRemaining = 0;
    float _currentBurnMax;


    void Start()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
        _psem = _particle.emission;
        _psem.rateOverTime = 0;
        //_door.SetActive(false);
    }

    public void Reset()
    {
        _trashCount = 0;
        _trashInBin.Clear();
        _burnTimeRemaining = 0;
        _shapePurityVector = Vector2.zero;
        _colorPurityVector = Vector2.zero;
        _currentShapePurity = 1;
        _currentColorPurity = 1;
        _psem.rateOverTime = 0;
        UpdatePanel();
    }

    private void Update()
    {
        if (_burnTimeRemaining >0)
        {
            _burnTimeRemaining -= Time.deltaTime;
            UpdatePanel();
            if (_burnTimeRemaining < 0)
            {
                EndBurn();
            }
        }
        
    }

    private void EndBurn()
    {
        //_door.SetActive(false);
        _beltHandler.StopBurn();
        _psem.rateOverTime = 0;
        if (!_burnsTrash)
        {
            Instantiate(_burnProductPrefab, _burnProductOutput.position, Quaternion.identity);
        }

        _trashCount = 0;
        _colorPurityVector = Vector2.zero;
        _currentColorPurity = 1;
        foreach (var trash in _trashInBin)
        {
            trash.Despawn();
        }
        _trashInBin.Clear();
        UpdatePanel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TrashHandler newTrash;
        if (collision.TryGetComponent<TrashHandler>(out newTrash))
        {
            AddTrash(newTrash);
            if (_burnsTrash && _trashCount >= 5)
            {
                CommandBurn();
            }
            if (!_burnsTrash && _trashCount >= 10)
            {
                CommandBurn();
            }
        }
    }

    private void AddTrash(TrashHandler newTrash)
    {
        _trashInBin.Add(newTrash);
        _trashCount++;


        _colorPurityVector += ValueHelper.GetColorValue(newTrash.GetTColor());
        _currentColorPurity = _colorPurityVector.magnitude / _trashCount;

        UpdatePanel();
    }

    private void CommandBurn()
    {
        _beltHandler.ForceDown();
        _beltHandler.StartBurn();
        //_door.SetActive(true);
        _psem.rateOverTime = 100;
        if (_burnsTrash)
        {
            _currentBurnMax = Mathf.Clamp((2 * _trashCount) / _currentColorPurity, 1, 10f);
            _burnTimeRemaining = _currentBurnMax;
        }
        else
        {
            _currentBurnMax = Mathf.Clamp(2 + _trashCount / _currentColorPurity, 1, 10f);
            _burnTimeRemaining = _currentBurnMax;
        }

    }


    private void UpdatePanel()
    {       
            if (_burnTimeRemaining > 0)
            {
                if (_burnTimeRemaining/_currentBurnMax > 0.8f)
                {
                    _shapeSR.sprite = _shapeSprites[0];
                }
                else if (_burnTimeRemaining / _currentBurnMax > 0.6f)
                {
                    _shapeSR.sprite = _shapeSprites[1];
                }
                else if (_burnTimeRemaining / _currentBurnMax > 0.4f)
                {
                    _shapeSR.sprite = _shapeSprites[2];
                }
                else if (_burnTimeRemaining / _currentBurnMax > 0.28f)
                {
                    _shapeSR.sprite = _shapeSprites[3];
                }
                else if (_burnTimeRemaining > 0.15f)
                {
                    _shapeSR.sprite = _shapeSprites[4];
                }
                else
                {
                    _shapeSR.sprite = _shapeSprites[5];
                }
            }

            else
            {
                if (_burnsTrash)
                {
                    if (_trashCount >= 5)
                    {
                        _shapeSR.sprite = _shapeSprites[0];
                    }
                    else if (_trashCount >= 4)
                    {
                        _shapeSR.sprite = _shapeSprites[1];
                    }
                    else if (_trashCount >= 3)
                    {
                        _shapeSR.sprite = _shapeSprites[2];
                    }
                    else if (_trashCount >= 2)
                    {
                        _shapeSR.sprite = _shapeSprites[3];
                    }
                    else if (_trashCount >= 1)
                    {
                        _shapeSR.sprite = _shapeSprites[4];
                    }
                    else
                    {
                        _shapeSR.sprite = _shapeSprites[5];
                    }
                }
                else
            {
                if (_trashCount >= 10)
                {
                    _shapeSR.sprite = _shapeSprites[0];
                }
                else if (_trashCount >= 8)
                {
                    _shapeSR.sprite = _shapeSprites[1];
                }
                else if (_trashCount >= 6)
                {
                    _shapeSR.sprite = _shapeSprites[2];
                }
                else if (_trashCount >= 4)
                {
                    _shapeSR.sprite = _shapeSprites[3];
                }
                else if (_trashCount >= 2)
                {
                    _shapeSR.sprite = _shapeSprites[4];
                }
                else
                {
                    _shapeSR.sprite = _shapeSprites[5];
                }
            }
            }
        

        


        if (_currentColorPurity < 0.15f)
        {
            _colorSR.sprite = _colorSprites[0];
        }
        else if (_currentColorPurity < 0.3f)
        {
            _colorSR.sprite = _colorSprites[1];
        }
        else if (_currentColorPurity < 0.5f)
        {
            _colorSR.sprite = _colorSprites[2];
        }
        else if (_currentColorPurity < 0.7f)
        {
            _colorSR.sprite = _colorSprites[3];
        }
        else if (_currentColorPurity < 0.9f)
        {
            _colorSR.sprite = _colorSprites[4];
        }
        else
        {
            _colorSR.sprite = _colorSprites[5];
        }
    }
}
