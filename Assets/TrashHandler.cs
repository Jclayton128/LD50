using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHandler : MonoBehaviour
{
    SpriteRenderer _sr;
    public enum TColor { Blue, Green, Yellow, Red, Brown};
    public enum TShape { X, Star, Hex, Circle, Square, Clover};

    //settings
    Color blue = Color.blue;
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
                return;
            case TColor.Brown:
                _sr.color = brown;
                return;
            case TColor.Green:
                _sr.color = green;
                return;
            case TColor.Red:
                _sr.color = red;
                return;
            case TColor.Yellow:
                _sr.color = yellow;
                return;

        }
    }

    public TColor GetTColor()
    {
        return tColor;
    }
}
