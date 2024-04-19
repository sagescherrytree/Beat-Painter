using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class StrokeReader
{
    public static List<List<Vector2>> ReadStrokes(string filename)
    {
        List<List<Vector2>> res = new();
        using var reader = new StreamReader(filename);
        while (reader.ReadLine() is { } line)
        {
            List<Vector2> stroke = new();
            var floats = line.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(float.Parse)
                .ToArray();
            for (var i = 0; i < floats.Length; i += 2)
            {
                stroke.Add(new Vector2(floats[i], floats[i + 1]));
            }
            res.Add(stroke);
        }

        return res;
    }
}
