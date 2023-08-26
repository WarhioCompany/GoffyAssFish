using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValues
{
    public static float height;
    public static float heightMeterRatio;
    public const float MaxHeight = 1200;

    public static float enemyHeight;

    public const float maxObjHeightOffset = 100;
    public const float objGravity = 0.5f;

    public const float worldXOffset = 25;

    public static float getHeight(float actualHeight)
    {
        return actualHeight * heightMeterRatio * -1;
    }
}
