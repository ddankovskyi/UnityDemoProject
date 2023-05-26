using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageble
{

    public event Action<Damage> OnDamageResived;
    public event Action OnDeath;

    public int MaxHP { get; set; } = 1;
    public int CurrentHP { get; set; } = -1;


    public void ReceiveDamage(Damage damage)
    {
        CurrentHP -= damage.DamageValue;
        OnDamageResived?.Invoke(damage);
        if (CurrentHP <= 0) OnDeath?.Invoke();
        Debug.Log($"{gameObject.name} lost {damage.DamageValue} hp. {CurrentHP}/{MaxHP}");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (CurrentHP == -1) CurrentHP = MaxHP;
    }

}
