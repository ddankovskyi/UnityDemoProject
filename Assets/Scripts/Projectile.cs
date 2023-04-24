using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float Speed = 3;
    [SerializeField] float LifeTimeSeconds = 3;
    void Start()
    {
        Invoke(nameof(SelfDestroy), LifeTimeSeconds);
    }

    void Update()
    {

    }


    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * Time.deltaTime * Speed);

    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
