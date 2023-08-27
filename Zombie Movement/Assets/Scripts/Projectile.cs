using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public QuadraticCurve curve;
    public float speed;
    private float sampleTime;
    void Start()
    {
        sampleTime = 0f;
    }

    
    void Update()
    {
        if (curve == null)
        {
            return;
        }
        sampleTime += Time.deltaTime * speed;
        transform.position = curve.Evaluate(sampleTime);
        transform.forward = curve.Evaluate(sampleTime + 0.001f) - transform.position;

        if (sampleTime >=1f)
        {
            ObjectPooling.Instance.DestroyItem(curve.gameObject);
            Destroy(gameObject);
        }
    }
}
