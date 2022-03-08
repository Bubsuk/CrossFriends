using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezierCurve
{
    public static Vector3 Bezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float elapsedTime, float durationTime)
    {

        Vector3 _pointA = Vector3.Lerp(a, b, elapsedTime / durationTime);
        Vector3 _pointB = Vector3.Lerp(b, c, elapsedTime / durationTime);
        Vector3 _pointC = Vector3.Lerp(c, d, elapsedTime / durationTime);

        Vector3 _pointD = Vector3.Lerp(_pointA, _pointB, elapsedTime / durationTime);
        Vector3 _pointE = Vector3.Lerp(_pointB, _pointC, elapsedTime / durationTime);

        Vector3 _pointF = Vector3.Lerp(_pointD, _pointE, elapsedTime / durationTime);


        return _pointF;
    }
}
