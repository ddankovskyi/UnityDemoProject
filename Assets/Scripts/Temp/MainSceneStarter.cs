using Assets.Scripts.Items;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class MainSceneStarter : MonoBehaviour
{
    private void Awake()
    {
        Player player = new Player();

        var inventoryData = CreateDebugInventoryData();
        var inventory = new InventoryManager<InventoryItem>();
        inventory.Load(inventoryData);

        Game.Inint(inventory);

        Game.Inint(player);


        

        
    }

    InventoryData<InventoryItem> CreateDebugInventoryData()
    {

     
        InventoryItem item1 = new SpellItem
        {
            Id = "BaseSpell1",
            Amount = 1
        };
        InventoryItem item2 = new SpellItem
        {
            Id = "BaseSpell2",
            Amount = 1
        };

        InventoryData<InventoryItem> inventoryData = new InventoryData<InventoryItem>();
        inventoryData.Items = new Dictionary<string, InventoryItem>();
        inventoryData.TypeSlots = new Dictionary<string, System.Type>();
        inventoryData.Items.Add(CharacterInventoryUI.INVENTORY_SLOTS_ID_PREFIX + 1, item1);
        inventoryData.Items.Add(CharacterInventoryUI.INVENTORY_SLOTS_ID_PREFIX + 2, item2);
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
