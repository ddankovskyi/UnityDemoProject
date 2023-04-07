using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : AItemObject
{

    [SerializeField] float speed = 3;
    [SerializeField] float lifeTimeSeconds = 3;
    [SerializeField] Damage damage = new Damage(5);

    Rigidbody2D _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Invoke(nameof(SelfDestroy), lifeTimeSeconds);
    }

    private void FixedUpdate()
    {
        _rigidbody.AddRelativeForce(Vector2.up * speed, ForceMode2D.Impulse);
    }

    void SelfDestroy() => Destroy(gameObject);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SelfDestroy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    public override void Init(InventoryItem item) { }
}
