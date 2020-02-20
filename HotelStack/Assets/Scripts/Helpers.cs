using System;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    private const float _tolerance = 0.1f;

    public static float ComponentSum(this Vector3 vector)
    {
        return vector.x + vector.y + vector.z;
    }

    public static Vector3 Inverted(this Vector3 vector)
    {
        return new Vector3(
            Math.Abs(vector.x) > _tolerance ? 0 : 1,
            Math.Abs(vector.y) > _tolerance ? 0 : 1,
            Math.Abs(vector.z) > _tolerance ? 0 : 1
        );
    }

    public static Vector3 Abs(this Vector3 vector)
    {
        return new Vector3(Math.Abs(vector.x), Math.Abs(vector.y), Math.Abs(vector.z));
    }

    public static T Random<T>(this IList<T> collection)
    {
        var randomIndex = UnityEngine.Random.Range(0, collection.Count);
        return collection[randomIndex];
    }
}