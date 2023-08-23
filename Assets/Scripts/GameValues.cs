using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValues
{
    public static float height;
    public static float heightMeterRatio;
    public const float MaxHeight = 1200;

    public static float enemyHeight;

    public const float maxObjHeightOffset = 40;

    public const float worldXOffset = 25;

    public static float getHeight(float actualHeight)
    {
        return actualHeight * heightMeterRatio * -1;
    }
}
