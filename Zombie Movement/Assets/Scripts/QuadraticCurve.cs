using System.Collections.Generic;
using UnityEngine;

public class QuadraticCurve : MonoBehaviour
{
    public Transform player;
    public Transform target;
    
    public JoystickPlayerExample playerExample;
    public Transform control;
    public float t;
    public Vector3 evaluate;
    private int a =0;
  
    public Vector3 Evaluate(float t)
    {
        var position = control.position;
        Vector3 pc = Vector3.Lerp(player.position, position, t);
        Vector3 tc = Vector3.Lerp(position, target.transform.position, t);
        return Vector3.Lerp(pc, tc, t);
    }

    // private void Update()
    // {
    //    Vector3 evaluate = EnesUtils.Evaluate( t, player, control, target);
    // }

    private void OnDrawGizmos()
    {
        if (player == null || target == null || control == null)
        {
            return;
        }

        for (int i = 0; i < 40; i++)
        {
            Gizmos.DrawWireSphere(Evaluate(i/40f),0.1f);
        }
    }

    public void Init(Transform transform1, Transform enemyTargetTransform)
    {
        player = transform1;
        target = enemyTargetTransform;
    }
}