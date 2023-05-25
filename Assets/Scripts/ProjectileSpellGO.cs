using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ProjectileSpellGO : MonoBehaviour
{
    ProjectileSpell _spellData;
    [SerializeField] ParticleSystem _explosionPrefab;

    Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, _spellData.Lifetime);
        _rigidbody.AddRelativeForce(Vector3.forward * _spellData.Speed, ForceMode.Impulse);

    }

    void Explode()
    {

        ParticleSystem particles = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        //particles.GetComponent<ParticleSystemRenderer>().material.SetTexture("_MainTex", _spellData.Icon.texture);

        Destroy(particles.gameObject, 1); // TODO destroy properly
        Destroy(gameObject, 0.01f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        Debug.Log(collision.gameObject.name);
        collision.gameObject.GetComponent<IDamageble>()?.ReceiveDamage(new Damage(_spellData.Damage));
    }

    public void InitSpell(ProjectileSpell spell)
    {
        _spellData = spell;
        if(spell is IWithTrigger spellWithTrigger)
        {
            var triggerComponent = gameObject.AddComponent<SpellTrigger>();
            triggerComponent.Load = spellWithTrigger.Load;
        }

    }
}
