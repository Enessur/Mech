using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public static class EnesUtils
{
    public static void LerpMe_Rotation(this Transform _transform,Quaternion _target, float lerpRate = 35f)
    {
        _transform.rotation = Quaternion.Lerp(_transform.rotation, _target, lerpRate * Time.deltaTime);
    }
    public static void LerpMe(this Vector3 _orginal, Vector3 _target, float lerpRate = 25f)
    {
        _orginal = Vector3.Lerp(_orginal, _target, lerpRate * Time.deltaTime);
    }


    public static Vector3 Evaluate(float t, Transform player, Transform control, Transform target)
    {
        var position = control.position;
        Vector3 pc = Vector3.Lerp(player.position, position, t);
        Vector3 tc = Vector3.Lerp(position, target.position, t);
        return Vector3.Lerp(pc, tc, t);
    }
    
}
