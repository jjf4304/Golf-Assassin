using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Values
{
    public static bool Paused { get; set; } = false;
    public static int HoleScore { get; set; }
    public static int CourseScore { get; set; } = 0;
    public static int Strokes { get; set; }
    public static int DestroyedTargets { get; set; }
    public static int TotalTargets { get; set; }
    public static int HoleIndex { get; set; } = 0;
    public static List<string> HoleNames = new List<string>() { "SampleScene", "Level2", "Level3", "level4", "level5" };
    public static List<int> HolePars = new List<int>() { 8, 15, 30, 5, 3};
}
