using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneLimits
{
    public static float MaxX;
    public static float MinX;
    public static float MaxY;
    public static float MinY;

    public static void FindLimits()
    {
        Vector2 TopRightPoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        Vector2 BottomLeftPoint = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        MaxX = TopRightPoint.x;
        MinX = BottomLeftPoint.x;
        MaxY = TopRightPoint.y;
        MinY = BottomLeftPoint.y;
    }
}
