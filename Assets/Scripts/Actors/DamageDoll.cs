using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDoll : MonoBehaviour, IDamageble {

    readonly int HIT_ANIM = Animator.StringToHash("Hit");

    Animator _animator;

    public void ReceiveDamage(Damage damage)
    {
        _animator.SetTrigger(HIT_ANIM);
        Debug.Log($"damage received {damage.DamageValue}");
    }


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

}
