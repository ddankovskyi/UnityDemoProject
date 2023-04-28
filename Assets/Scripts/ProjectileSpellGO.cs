using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ProjectileSpellGO : AItemObject
{
    ProjectileSpell _spellData;
    [SerializeField] ParticleSystem _explosionPrefab;

    Rigidbody2D _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, _spellData.Lifetime);
        _rigidbody.AddRelativeForce(Vector2.up * _spellData.Speed, ForceMode2D.Impulse);

    }

    void Explode()
    {

        ParticleSystem particles = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        particles.GetComponent<ParticleSystemRenderer>().material.SetTexture("_MainTex", _spellData.Icon.texture);

        Destroy(particles.gameObject, 1); // TODO destroy properly
        Destroy(gameObject, 0.01f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
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

    public override void Init(InventoryItem item) {
    }
}
