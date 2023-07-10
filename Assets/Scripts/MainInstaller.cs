using System;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{

    [SerializeField] Character _character;
    [SerializeField] UniversalItemUI _universalItemUIPrefab;
    [SerializeField] InventorySlotUI _inventorySlotUIPrefab;
    [SerializeField] WandSpellSlotUI _wandSpellSlotUIPrefab;
    [SerializeField] WandUI _wandUIPrefab;
    [SerializeField] SpellItemObject _spellItemObjectPrefab;

    public override void InstallBindings()
    {

        Container.Bind<CharacterManager>().AsSingle().WithArguments(_character);
        
        var inventoryData = DebugInventoryLoader.CreateDebugInventoryData();
        Container.Bind<CharacterInventoryManager>().AsSingle().WithArguments(inventoryData);

        Container.Bind<SpellsManager>().AsSingle();
        Container.Bind<GameStateManager>().AsSingle();
        Container.Bind<Storage>().AsSingle();

        Container.BindFactory<UniversalItemUI, UniversalItemUI.Factory>().FromComponentInNewPrefab(_universalItemUIPrefab);
        Container.BindFactory<InventorySlotUI, InventorySlotUI.Factory>().FromComponentInNewPrefab(_inventorySlotUIPrefab);
        Container.BindFactory<WandSpellSlotUI, WandSpellSlotUI.Factory>().FromComponentInNewPrefab(_wandSpellSlotUIPrefab);
        Container.BindFactory<WandUI, WandUI.Factory>().FromComponentInNewPrefab(_wandUIPrefab);
        Container.BindFactory<SpellItemObject, SpellItemObject.Factory>().FromComponentInNewPrefab(_spellItemObjectPrefab);

    }



   

}