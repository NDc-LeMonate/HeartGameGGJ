using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static Vector3 NearestVector(Vector3 vect)
    {
        return new Vector3(Mathf.RoundToInt(vect.x), Mathf.RoundToInt(vect.y), Mathf.RoundToInt(vect.z));
    }
}
