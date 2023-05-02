using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTrigger : MonoBehaviour
{
    Vector3 _lastVelosity;
    Rigidbody2D _rigidbody;

    float TRIGGER_SPAWN_OFFSET = 0.5f;

    public WandLoad Load { private get; set; }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();    
    }
    private void FixedUpdate()
    {
        _lastVelosity = _rigidbody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var normal = collision.GetContact(0).normal;
        Vector3 contactPoint = collision.GetContact(0).point;
        Vector3 InDirection = contactPoint + _lastVelosity.normalized;

        var reflection = Vector3.Reflect(InDirection, normal).normalized;


        //Debug.DrawRay(contactPoint, reflection, Color.yellow, 3f);
        //Debug.DrawRay(contactPoint, _lastVelosity.normalized, Color.blue, 3f);
        //Debug.DrawRay(contactPoint, -_lastVelosity.normalized, Color.green, 3f);

        var spawnPoint = contactPoint + reflection * TRIGGER_SPAWN_OFFSET;

        Debug.DrawRay(spawnPoint, reflection, Color.cyan, 3f);

        Load?.Release(spawnPoint, Quaternion.LookRotation(transform.forward, reflection));
    }
}
