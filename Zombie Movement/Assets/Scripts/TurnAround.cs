using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
    private GameObject _target;
    
    // Update is called once per frame

    private void Start()
    {
        _target = GameObject.Find("Aim");
    }

    void Update()
    {
       // if (Input.GetMouseButtonDown(1))
        //todo : Active - Deactive aim for inspect the mech cuz it's lookin cool
        //todo : decrease rotation speed and lock for y rotation only guns can look up and down 

        Vector3 targetPosition =
            new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z);
        transform.LookAt(targetPosition);
    }
}
