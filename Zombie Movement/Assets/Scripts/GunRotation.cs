using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    
    public JoystickPlayerExample playerExample;
    private Quaternion m_initialRotation;
    public Transform torsoRotation;
    private void Awake()
    {
        m_initialRotation = transform.rotation;
    }


    void Update()
    {
      
            if ((playerExample._enemyTarget != null) &&
                (Vector3.Distance(transform.position, playerExample._enemyTarget.transform.position) < 40f))
            {
                Vector3 direction = playerExample._enemyTarget.transform.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                // transform.rotation = targetRotation;
                transform.LerpMe_Rotation(targetRotation,75f);
                
            }
            else
            {
                transform.LerpMe_Rotation(m_initialRotation * torsoRotation.rotation);
                //transform.rotation = Quaternion.Lerp(transform.rotation,m_initialRotation * torsoRotation.rotation,25* Time.deltaTime);
            }
        
        
    }
}