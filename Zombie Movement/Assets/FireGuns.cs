using System;
using System.Collections;
using System.Collections.Generic;
using Andtech.ProTracer;
using UnityEngine;

public class FireGuns : MonoBehaviour
{
    public TracerDemo upperBulletRight;
    public TracerDemo downBulletRight;
    public AudioSource gunshotSound;

    private string _currentAnimation;
    private Animator _animator;
    private float animSpeed = 1f;
    const string SHOOT = "Shoot";
    const string IDLE = "Idle";
    private float timer;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            ChangeAnimationState(SHOOT);
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                if (animSpeed < 3)
                {
                    animSpeed = animSpeed +0.2f;
                }

                timer = 0;
            }
        }
        else
        {
            animSpeed = 1;
            ChangeAnimationState(IDLE);
        }
    }

    public void upBarrelShoot()
    {
        upperBulletRight.Fire();
        gunshotSound.pitch  = Mathf.Lerp(1f, 2f, Mathf.InverseLerp(1f, 3f, animSpeed));
        gunshotSound.Play();
    }

    public void downBarrelShoot()
    {
        downBulletRight.Fire();
        gunshotSound.pitch  = Mathf.Lerp(1f, 2f, Mathf.InverseLerp(1f, 3f, animSpeed));
        gunshotSound.Play();
    }

    void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (_currentAnimation == newState)
        {
            return;
        }

        _animator.SetFloat("Speed", animSpeed);
        _animator.Play(newState);
    }
}