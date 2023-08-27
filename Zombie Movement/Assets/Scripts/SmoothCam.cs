using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SmoothCam : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; 
    public Transform target;
    private Vector3 previousPosition;
    public JoystickPlayerExample PlayerExample;
    private float speed;
    private CinemachineFramingTransposer framingTransposer;
    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        target = virtualCamera.Follow;
        previousPosition = target.position;
    }

    private void Update()
    {
        
        float dampValue = Mathf.Lerp(5f, 0.5f, Mathf.InverseLerp(0f, PlayerExample.maxSpeed, PlayerExample.currentSpeed));
        float track = Mathf.Lerp(10, 3f, Mathf.InverseLerp(0f, PlayerExample.maxSpeed, PlayerExample.currentSpeed));
       
        framingTransposer.m_XDamping = dampValue;
        framingTransposer.m_YDamping = dampValue;
        framingTransposer.m_ZDamping = dampValue;

        framingTransposer.m_TrackedObjectOffset.z = track;


    }
}
