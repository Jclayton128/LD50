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


    //state
    [SerializeField] List<TrashHandler> _trashInBin = new List<TrashHandler>();
    int _trashCount = 0;
    Vector2 _shapePurityVector = Vector2.zero;
    Vector2 _colorPurityVector = Vector2.zero;
    float _currentShapePurity = 1;
    float _currentColorPurity = 1;

    void Start()
    {
        UpdatePurityPanel();
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

        _shapePurityVector += ValueHelper.GetShapeValue(newTrash.GetTShape());
        _colorPurityVector += ValueHelper.GetColorValue(newTrash.GetTColor());

        _currentShapePurity = _shapePurityVector.magnitude / _trashCount;
        _currentColorPurity = _colorPurityVector.magnitude / _trashCount;

        UpdatePurityPanel();
    }


    private void UpdatePurityPanel()
    {
        if (_currentShapePurity < 0.15f)
        {
            _shapeSR.sprite = _shapeSprites[0];
        }
        else if (_currentShapePurity < 0.3f)
        {
            _shapeSR.sprite = _shapeSprites[1];
        }
        else if (_currentShapePurity < 0.5f)
        {
            _shapeSR.sprite = _shapeSprites[2];
        }
        else if (_currentShapePurity < 0.7f)
        {
            _shapeSR.sprite = _shapeSprites[3];
        }
        else if (_currentShapePurity < 0.9f)
        {
            _shapeSR.sprite = _shapeSprites[4];
        }
        else
        {
            _shapeSR.sprite = _shapeSprites[5];
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
