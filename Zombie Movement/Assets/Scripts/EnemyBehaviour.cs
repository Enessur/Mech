using System.Collections.Generic;
using Script;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum TaskCycleEnemy
    {
        Chase,
        Patrol
    }
    
    [SerializeField] protected TaskCycleEnemy taskCycleEnemy;
    [SerializeField] private float xMin, yMin, xMax, yMax, zMin, zMax;
    [SerializeField]private float chaseSpeed;
    [SerializeField]private float patrolSpeed;
    [SerializeField] private  float chasingDistance;
    
    public GameObject patrolBorders;
    public Transform moveSpot;
   
    public Rigidbody Rb => _rb;
    protected Transform _target;
    private Rigidbody _rb;
    private float startWaitTime = 1f;
    private Vector3 _patrolPos;
    private float _patrolTimer;
    
    
    protected virtual void Start()
    {
        BoxCollider squareCollider = patrolBorders.GetComponent<BoxCollider>();

        xMin = patrolBorders.transform.position.x - squareCollider.size.x / 2;
        xMax = patrolBorders.transform.position.x + squareCollider.size.x / 2;
        yMin = patrolBorders.transform.position.y - squareCollider.size.y / 2;
        yMax = patrolBorders.transform.position.y + squareCollider.size.y / 2;
        zMin = patrolBorders.transform.position.z - squareCollider.size.z / 2;
        zMax = patrolBorders.transform.position.z + squareCollider.size.z / 2;
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _patrolPos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), Random.Range(zMin, zMax));
        _rb = GetComponent<Rigidbody>();
        moveSpot.SetParent(null);
        patrolBorders.transform.parent = null;
        TargetManager.Instance.AddEnemy(this);
    }
    
    protected virtual void Update()
    {
        if (Vector3.Distance(transform.position, _target.position) < chasingDistance)
        {
            taskCycleEnemy = TaskCycleEnemy.Chase;
        }
        else
        {
            taskCycleEnemy = TaskCycleEnemy.Patrol;
        }


        switch (taskCycleEnemy)
        {
            case TaskCycleEnemy.Chase:
                Vector3 chaseDirection = (_target.position - transform.position).normalized;
                _rb.velocity = chaseDirection * chaseSpeed;
                break;
            case TaskCycleEnemy.Patrol:
                PatrolPosition();
                Vector3 patrolDirection = (_patrolPos - transform.position).normalized;
                _rb.velocity = patrolDirection * patrolSpeed;
                break;
        }
    }

    private void PatrolPosition()
    {
        _patrolTimer += Time.deltaTime;

        if (!(_patrolTimer >= startWaitTime)) return;
        _patrolTimer = 0;
        
        if (Vector3.Distance(transform.position,_patrolPos) <1f)
        {
            _patrolPos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), Random.Range(zMin, zMax));
        }
    }
    
    
}