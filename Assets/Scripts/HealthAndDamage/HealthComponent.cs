using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour, IDamageble
{


    public event Action<Damage> OnDamageResived;
    public UnityEvent<int> OnHPValueChanged;
    public UnityEvent<int> OnMaxHPValueChanged;
    
    public event Action OnDeath;

    public bool IsAlive => CurrentHP > 0;
    private int _maxHP = 1;
    public int MaxHP {
        get => _maxHP;
        set { _maxHP = value;
            OnMaxHPValueChanged.Invoke(_maxHP);
        }
    }

    public int CurrentHP { get; set; } = -1;


    public void ReceiveDamage(Damage damage)
    {
        CurrentHP -= damage.DamageValue;
        CurrentHP = Math.Max(CurrentHP, 0);
        OnDamageResived?.Invoke(damage);
        OnHPValueChanged?.Invoke(CurrentHP);
        if (CurrentHP <= 0) OnDeath?.Invoke();
        //Debug.Log($"{gameObject.name} lost {damage.DamageValue} hp. {CurrentHP}/{MaxHP}");
    }

    public void ReceiveHeal(int healValue, bool overhealAllowed = false)
    {
        CurrentHP += healValue;
        if(!overhealAllowed) 
            CurrentHP = Math.Min(CurrentHP, MaxHP);
        OnHPValueChanged?.Invoke(CurrentHP);

    }


    // Start is called before the first frame update
    void Start()
    {
        if (CurrentHP == -1) CurrentHP = MaxHP;
       
    }

    [ContextMenu("DebugHurt")]
    void DebugHurt()
    {
        ReceiveDamage(new Damage(25));
    }

}
