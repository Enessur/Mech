using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class JoystickPlayerExample : MonoBehaviour
{
    public enum MovementMode
    {
        Velocity,
        Force
    }

    [SerializeField] private MovementMode movementMode;
    [SerializeField] private bool ısForceMovementOn;
    [SerializeField] public List<QuadraticCurve> curve = new();
    
    [BoxGroup("Variables")]
    [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] private float rotateSpeed = 100f;
    [BoxGroup("Variables")]
    [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    public float fireRate = 0.5f;
    [BoxGroup("Variables")]
    [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    public float speed;
    [BoxGroup("Variables")]
    [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    public float topRotationSpeed;
    [BoxGroup("Variables")]
    [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    public float currentSpeed;
    [BoxGroup("Variables")]
    [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    public float maxSpeed;
    [BoxGroup("Scripts")]
    [GUIColor(1f, 0f, 0f, 1f)]
    public Projectile projectile;
    [BoxGroup("Scripts")]
    [GUIColor(1f, 0f, 0f, 1f)]
    public VariableJoystick variableJoystick;
    [BoxGroup("Transforms")]
    [GUIColor(1f, 0.8f, 0.2f, 1f)]
    public Transform objectRotation;
    [BoxGroup("Transforms")]
    [GUIColor(1f, 0.8f, 0.2f, 1f)]
    public Transform topRotation;
    
    public Rigidbody rb;
    public EnemyBehaviour _enemyTarget;
    private Transform control;
    private Vector3 direction;
    private Quaternion objectQuaternion;
    private string _currentAnimation;
    private Animator _animator;
    private float animSpeed;
    public AudioSource stepSoundEffect;
    const string IDLE = "Idle";
    const string WALK= "Walk";
    private float nextFireTime = 0.0f;
    public List<Vector3> controlPositionsList;
    private void Start()
    {
        objectQuaternion = objectRotation.rotation;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if ( Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                Fire();
            }
        }
    }

    public void FixedUpdate()
    {
        FindEnemy();

         direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        if (ısForceMovementOn == true)
        {
            movementMode = MovementMode.Force;
        }
        else
        {
            movementMode = MovementMode.Velocity;
        }

        switch (movementMode)
        {
            case  MovementMode.Force :
                rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
              
                // Debug.Log(rb.velocity.magnitude);
                if (rb.velocity.magnitude > maxSpeed)
                {
                    rb.velocity = rb.velocity.normalized * maxSpeed;

                }
                break;
            case  MovementMode.Velocity :
                rb.velocity = direction * speed* 100f * Time.deltaTime;
                break;
        }

     
        
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            objectRotation.rotation = Quaternion.RotateTowards(objectRotation.rotation, targetRotation,
                rotateSpeed * Time.fixedDeltaTime);
            
            if ((_enemyTarget != null) && (Vector3.Distance(transform.position, _enemyTarget.transform.position) < 40f))
            {
                Vector3 targetDirection = _enemyTarget.transform.position - objectRotation.position;
                Quaternion enemyTargetRotation = Quaternion.LookRotation(targetDirection.normalized, Vector3.up);
                topRotation.rotation = Quaternion.RotateTowards( topRotation.rotation, enemyTargetRotation,
                    rotateSpeed * Time.fixedDeltaTime);
            }
            else
            {
              
                Quaternion topR = Quaternion.LookRotation(direction.normalized, Vector3.up);
                topRotation.rotation = Quaternion.RotateTowards(topRotation.rotation, topR,
                    rotateSpeed * Time.fixedDeltaTime);
            }

        }
        else
        {
            rb.velocity = (direction * speed) / Time.deltaTime;
        }
        currentSpeed = rb.velocity.magnitude;
        animSpeed = Mathf.Lerp(0.1f, 2f, Mathf.InverseLerp(0f, maxSpeed, currentSpeed));
        
        if (currentSpeed == 0)
        {
            ChangeAnimationState(IDLE);
        }
        else
        {
            ChangeAnimationState(WALK);
        }

       
        
        // if (Input.GetKey(KeyCode.Q))
        // {
        //     
        //     Projectile _projectile = Instantiate(projectile, transform.position, Quaternion.identity);
        //     _projectile.curve = curve;
        // }
    }
        int i;
    
    void Fire()
    {
        Vector3 newPosition = new Vector3(Random.Range(-25f, 25f), Random.Range(20f, 35f), 0f);

        var item = ObjectPooling.Instance.GetObject();
        var myCurve = item.GetComponent<QuadraticCurve>();
        myCurve.Init(transform, _enemyTarget.transform);
        myCurve.control.transform.position = newPosition+ transform.position;
        Projectile _projectile = Instantiate(projectile, transform.position, Quaternion.identity);
        _projectile.curve = myCurve;

        // Vector3 controlPosition = control.transform.position;
        // controlPositionsList = new List<Vector3>();
        // controlPositionsList.Add(controlPosition);

    }
    
    
    

    public void PlayStepSound()
    {
        stepSoundEffect.Play();
    }
    public void FindEnemy()
    {
        _enemyTarget = TargetManager.Instance.FindClosestTarget(gameObject.transform.position);
    }
    void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (_currentAnimation == newState)
        {
            return;
        }

        //play the animation
        _animator.SetFloat("Speed",animSpeed);
        _animator.Play(newState);
    }
}