using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorReference
{
    public enum ColorType { Black, White, Red, Green, Blue };

    public static Dictionary<ColorType, Color> colorTypeToColor = new Dictionary<ColorType, Color>
    {
        { ColorType.Black, Color.black },
        { ColorType.White, Color.white},
        { ColorType.Red, Color.red },
        { ColorType.Green, Color.green },
        { ColorType.Blue, Color.blue },
    };

    public static Dictionary<ColorType, string> colorTypeToName = new Dictionary<ColorType, string>()
    {
        { ColorType.Black, "Black" },
        { ColorType.White, "White" },
        { ColorType.Red, "Red" },
        { ColorType.Green, "Green" },
        { ColorType.Blue, "Blue" },
    };
}
