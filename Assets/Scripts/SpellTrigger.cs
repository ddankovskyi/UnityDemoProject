using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTrigger : MonoBehaviour
{
    Vector3 _lastVelosity;
    Rigidbody _rigidbody;

    float TRIGGER_SPAWN_OFFSET = 0.5f;

    public WandLoad Load { private get; set; }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();    
    }
    private void FixedUpdate()
    {
        _lastVelosity = _rigidbody.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var normal = collision.GetContact(0).normal;
        Vector3 contactPoint = collision.GetContact(0).point;
        var reflection = Vector3.Reflect(_lastVelosity, normal).normalized;


        //Debug.DrawRay(contactPoint, normal, Color.blue, 3f);
        //Debug.DrawRay(contactPoint, reflection, Color.yellow, 3f);
        //Debug.DrawRay(contactPoint, -_lastVelosity.normalized, Color.green, 3f);

        var spawnPoint = contactPoint + reflection * TRIGGER_SPAWN_OFFSET;

        //Debug.DrawRay(spawnPoint, reflection, Color.cyan, 3f);

        Load?.Release(spawnPoint, Quaternion.LookRotation(reflection, transform.up));
    }
}
