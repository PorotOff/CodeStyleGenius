using UnityEngine;

public static class Vector3Extentions
{
    public static float GetSqrDistance(this Vector3 start, Vector3 end)
        => (end - start).sqrMagnitude;

    public static bool IsEnoughClose(this Vector3 start, Vector3 end, float distance)
        => start.GetSqrDistance(end) <= distance * distance;
}