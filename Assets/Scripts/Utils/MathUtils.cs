using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public static Quaternion GetRandom90DegRotation()
    {
        int random = Random.Range(0, 4);
        
        return Quaternion.Euler(0f, random * 90f, 0f);
    }
    
    public static Quaternion GetRandomFullCircleRotation()
    {
        int random = Random.Range(0, 359);
        
        return Quaternion.Euler(0f, random, 0f);
    }
}
