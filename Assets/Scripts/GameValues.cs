using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValues
{
    public static float height;
    public static float heightMeterRatio;
    public const float MaxHeight = 1200;

    public static float enemyHeight;

    public static float getHeight(float actualHeight)
    {
        return actualHeight * heightMeterRatio * -1;
    }
}
