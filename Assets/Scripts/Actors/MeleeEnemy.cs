using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Temp quick AI  
public class MeleeEnemy : MonoBehaviour
{
    NavMeshAgent _agent;
    public Transform target;
    [SerializeField] Animator _animator;
    [SerializeField] float _atackRange;
    [SerializeField] float _alertRange;

    int _InAtackRangeAnimHash = Animator.StringToHash("InAtackRange");

    bool _isAlerted;
    HealthComponent _health;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _health = GetComponent<HealthComponent>();
        _health.MaxHP = 15;

        _agent.stoppingDistance = _atackRange;

        _health.OnDeath += Die;
    }

    private void OnDestroy()
    {
        _health.OnDeath -= Die;
    }
    void Die()
    {
        _animator.SetTrigger("Dead");
        _agent.enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    void Update()
    {
        if (!_isAlerted)
        {
            if (Vector3.Distance(transform.position, target.position) < _alertRange)
            {
                _isAlerted = true;
                _animator.SetBool("Alerted", _isAlerted);
            }
            else return;
        }

        if (!_health.IsAlive) return;

        _agent.SetDestination(target.position);
        bool inAtackRange = _agent.remainingDistance <= _atackRange;

        _animator.SetBool(_InAtackRangeAnimHash, inAtackRange);
        if (inAtackRange)
        {
            RotateTowardTarget();
        }
    }

    private void RotateTowardTarget()
    {

        var dir = (target.position - transform.position);
        dir.y = 0;
        transform.forward = dir.normalized;
    }

    public void Hit()
    {
        Debug.Log("Hit");
    }
}
