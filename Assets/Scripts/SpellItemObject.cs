using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using static UnityEditor.Progress;

public class SpellItemObject : MonoBehaviour
{

    [SerializeField] Renderer _renderer;
    [SerializeField] Spell _spell;
    Texture2D _itemIcon;
    [Inject] SpellsManager _spellsManager;


    public SpellItem SpellItem { get; private set; }
    Animator _animator;

    int ANIM_NEAR_PLAYER_HASH = Animator.StringToHash("NearPlayer");

    public void Init(SpellItem item)
    {
        SpellItem = item;
        _itemIcon = _spellsManager.GetSpellById(item.Id).Icon.texture;
    }

    void Start()
    {
        if(SpellItem == null && _spell != null) {
            SpellItem = new SpellItem();
            SpellItem.Id = _spell.Id;
            _itemIcon = _spellsManager.GetSpellById(_spell.Id).Icon.texture;
        }
        var materialBlock = new MaterialPropertyBlock();
        materialBlock.SetTexture("_SpellIcon", _itemIcon);

        _renderer.SetPropertyBlock(materialBlock);
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _animator.SetBool(ANIM_NEAR_PLAYER_HASH, true);
        other.GetComponent<SpellItemsObjectsCollector>().SetObjectAvailable(this);
    }

    private void OnTriggerExit(Collider other)
    {
        _animator.SetBool(ANIM_NEAR_PLAYER_HASH, false);
        other.GetComponent<SpellItemsObjectsCollector>().SetObjectNoMoreAvailable(this);

    }

    public void Collect()
    {
        _animator.SetTrigger("Collect");
    }

    public void OnCollected()
    {
        Destroy(gameObject);
    }

    public class Factory : PlaceholderFactory<SpellItemObject> { };
}
