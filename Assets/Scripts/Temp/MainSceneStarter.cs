
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class MainSceneStarter : MonoBehaviour
{
    private void Awake()
    {
        CharacterManager player = new CharacterManager();

        var inventoryData = CreateDebugInventoryData();
        var inventory = new InventoryManager<InventoryItem>();
        var spellsManager = new SpellsManager();
        inventory.Load(inventoryData);

        Game.Inint(inventory);
        Game.Inint(player);
        Game.Inint(spellsManager);
        
    }

    InventoryData<InventoryItem> CreateDebugInventoryData()
    {


        SpellItem BaseSpell1 = new SpellItem
        {
            Id = "BaseSpell1",
        };
        SpellItem WithTrigger = new SpellItem
        {
            Id = "Base1WithTrigger",
        };
        SpellItem BaseSpell2 = new SpellItem
        {
            Id = "BaseSpell2",
        };
        SpellItem Double = new SpellItem
        {
            Id = "Double",
        };

        SpellItem Watermelon = new SpellItem
        {
            Id = "WatermellonSpell",
        };
        SpellItem Hasten = new SpellItem
        {
            Id = "Hasten"
        };


        List<SpellItem> items = Enumerable.Repeat<SpellItem>(null, 14).ToList();
        items[1] = BaseSpell1;
        items[2] = WithTrigger;
        items[4] = BaseSpell2;
        items[5] = Watermelon;

        WandItem wand = new WandItem
        {
            Manapool = 220,
            ManaChargeSpeed = 25,
            CastDelay = 0.15f,
            RechargeTime = 0.2f,
            Spells = items,
            Spread = 15f
        };

        InventoryData<InventoryItem> inventoryData = new InventoryData<InventoryItem>();
        inventoryData.Items = new Dictionary<string, InventoryItem>();
        inventoryData.TypeSlots = new Dictionary<string, System.Type>();
        inventoryData.Items.Add(InventoryIds.INVENTORY_SLOTS_ID_PREFIX + 1, BaseSpell1);
        inventoryData.Items.Add(InventoryIds.INVENTORY_SLOTS_ID_PREFIX + 3, BaseSpell2);
        inventoryData.Items.Add(InventoryIds.INVENTORY_SLOTS_ID_PREFIX + 4, Double);
        inventoryData.Items.Add(InventoryIds.INVENTORY_SLOTS_ID_PREFIX + 5, Hasten);
        inventoryData.TypeSlots.Add(InventoryIds.WANDS_SLOTS_ID_PREFIX + 1, typeof(WandItem));
        inventoryData.Items.Add(InventoryIds.WANDS_SLOTS_ID_PREFIX + 1, wand);

        return inventoryData;
    }

    void SaveTest()
    {
        InventoryItem item1 = new SpellItem
        {
            Id = "BaseSpell1",
            Amount = 1,
           // ItemResources = Resources.Load<ItemResources>("Items/Spell1")
        };

        string json = JsonConvert.SerializeObject(item1, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        });

        Debug.Log(json); 
    }

}
