using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ValueHelper
{
    static Vector2 Up = new Vector2(0, 1);
    static Vector2 UpRight = new Vector2(0.86f, .5f);
    static Vector2 DownRight = new Vector2(0.86f, -0.5f);
    static Vector2 Down = new Vector2(0, -1);
    static Vector2 DownLeft = new Vector2(-.86f, -.5f);
    static Vector2 UpLeft = new Vector2(-0.86f, 0.5f);

    public static Vector2 GetShapeValue(TrashHandler.TShape shape)
    {
        Vector2 value = Vector2.zero;

        switch (shape)
        {
            case TrashHandler.TShape.Square:
                value += DownLeft;
                break;
            case TrashHandler.TShape.Clover:
                value += UpLeft;
                break;
            case TrashHandler.TShape.X:
                value += Up;
                break;
            case TrashHandler.TShape.Star:
                value += UpRight;
                break;
            case TrashHandler.TShape.Hex:
                value += DownRight;
                break;
            case TrashHandler.TShape.Circle:
                value += Down;
                break;
        }

        return value;
    }

    public static Vector2 GetColorValue(TrashHandler.TColor color)
    {
        Vector2 value = Vector2.zero;

       
        switch (color)
        {
            case TrashHandler.TColor.Blue:
                value += DownLeft;
                break;
            case TrashHandler.TColor.Cyan:
                value += UpLeft;
                break;
            case TrashHandler.TColor.Green:
                value += Up;
                break;
            case TrashHandler.TColor.Yellow:
                value += UpRight;
                break;
            case TrashHandler.TColor.Red:
                value += DownRight;
                break;
            case TrashHandler.TColor.Purple:
                value += Down;
                break;
        }

        return value;
    }

}
