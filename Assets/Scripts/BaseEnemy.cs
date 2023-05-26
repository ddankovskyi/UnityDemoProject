using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{

    NavMeshAgent _agent;
    public Transform target;//temp

    [SerializeField] Transform face;
    // Start is called before the first frame update

    HealthComponent _health;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _health = GetComponent<HealthComponent>();
        _health.MaxHP = 15;

        _health.OnDeath += Die;
    }

    private void OnDestroy()
    {
        _health.OnDeath -= Die;
    }
    void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(target.position);
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            Vector3 newFaceScale = face.localScale;
            newFaceScale.z += Time.deltaTime;
            face.localScale = newFaceScale;
        } else
        {
            face.localScale = Vector3.one;
        }
    }
}
