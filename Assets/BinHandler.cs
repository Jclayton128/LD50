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

    //settings


    //state
    [SerializeField] List<TrashHandler> _trashInBin = new List<TrashHandler>();
    int _trashCount = 0;
    Vector2 _shapePurityVector = Vector2.zero;
    Vector2 _colorPurityVector = Vector2.zero;
    float _currentShapePurity = 1;
    float _currentColorPurity = 1;
    BeltHandler _beltHandler;
    float _burnTimeRemaining = 0;


    void Start()
    {
        UpdatePanel();
        _door.SetActive(false);
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
        _door.SetActive(false);
        _beltHandler.StopBurn();
        Instantiate(_burnProductPrefab, _burnProductOutput.position, Quaternion.identity);
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

    public void CommandBurn(BeltHandler bh)
    {
        _beltHandler = bh;
        _door.SetActive(true);
        _burnTimeRemaining = 2+ _trashCount / _currentColorPurity;
    }


    private void UpdatePanel()
    {
        if (_burnTimeRemaining > 0)
        {
            if (_burnTimeRemaining > 10)
            {
                _shapeSR.sprite = _shapeSprites[0];
            }
            else if (_burnTimeRemaining > 8)
            {
                _shapeSR.sprite = _shapeSprites[1];
            }
            else if (_burnTimeRemaining > 6)
            {
                _shapeSR.sprite = _shapeSprites[2];
            }
            else if (_burnTimeRemaining > 4)
            {
                _shapeSR.sprite = _shapeSprites[3];
            }
            else if (_burnTimeRemaining > 2)
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
            if (_trashCount > 10)
            {
                _shapeSR.sprite = _shapeSprites[0];
            }
            else if (_trashCount > 8)
            {
                _shapeSR.sprite = _shapeSprites[1];
            }
            else if (_trashCount > 6)
            {
                _shapeSR.sprite = _shapeSprites[2];
            }
            else if (_trashCount > 4)
            {
                _shapeSR.sprite = _shapeSprites[3];
            }
            else if (_trashCount > 2)
            {
                _shapeSR.sprite = _shapeSprites[4];
            }
            else
            {
                _shapeSR.sprite = _shapeSprites[5];
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
