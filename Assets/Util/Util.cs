using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    private static Vector3[] halfVectors =
    {
        Vector3.forward * 0.5f,
        Vector3.right * 0.5f,
        Vector3.back * 0.5f,
        Vector3.left * 0.5f
    };

    
    public static Vector3 GetHalfVector(this Direction direction)
    {
        return halfVectors[(int) direction];
    }
}
