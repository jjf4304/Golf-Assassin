using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Values
{
    public static string Score { get; set; }
    public static int Strokes { get; set; }
    public static int DestroyedTargets { get; set; }
    public static int TotalTargets { get; set; }
    public static int HoleIndex { get; set; } = 0;
    public static List<int> Par { get; set; } = new List<int>() { 8 };
}
