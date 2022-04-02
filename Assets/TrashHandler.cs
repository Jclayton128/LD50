using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHandler : MonoBehaviour
{
    SpriteRenderer _sr;
    public enum TColor { Blue, Cyan, Green, Yellow, Red, Purple};
    public enum TShape { X, Star, Hex, Circle, Square, Clover};

    //settings
    Color blue = Color.blue;
    Color cyan = Color.cyan;
    Color green = Color.green;
    Color yellow = Color.yellow;
    Color red = Color.red;
    Color brown = new Color(0.387f, 0.250f, 0.163f);

    //state
    [SerializeField] TShape tShape;
    TColor tColor;
    
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();   
    }

    public void SetTColor(TColor newTColor)
    {
        tColor = newTColor;
        switch (newTColor)
        {
            case TColor.Blue:
                _sr.color = blue;
                break;
            case TColor.Purple:
                _sr.color = brown;
                break;
            case TColor.Green:
                _sr.color = green;
                break;
            case TColor.Red:
                _sr.color = red;
                break;
            case TColor.Yellow:
                _sr.color = yellow;
                break;
            case TColor.Cyan:
                _sr.color = cyan;
                break;

        }
    }

    public TColor GetTColor()
    {
        return tColor;
    }

    public TShape GetTShape()
    {
        return tShape;
    }
}
