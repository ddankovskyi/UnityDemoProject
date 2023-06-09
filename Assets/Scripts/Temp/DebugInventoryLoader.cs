﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DebugInventoryLoader
{
    public static InventoryData<InventoryItem> CreateDebugInventoryData()
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
        SpellItem Light = new SpellItem
        {
            Id = "Light"
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
            Spread = 0f
        };

        InventoryData<InventoryItem> inventoryData = new InventoryData<InventoryItem>();
        inventoryData.Items = new Dictionary<string, InventoryItem>();
        inventoryData.TypeSlots = new Dictionary<string, System.Type>();
        inventoryData.Items.Add(CharacterInventoryManager.INVENTORY_SLOTS_ID_PREFIX + 1, BaseSpell1);
        inventoryData.Items.Add(CharacterInventoryManager.INVENTORY_SLOTS_ID_PREFIX + 3, BaseSpell2);
        inventoryData.Items.Add(CharacterInventoryManager.INVENTORY_SLOTS_ID_PREFIX + 4, Double);
        inventoryData.Items.Add(CharacterInventoryManager.INVENTORY_SLOTS_ID_PREFIX + 5, Hasten);
        inventoryData.Items.Add(CharacterInventoryManager.INVENTORY_SLOTS_ID_PREFIX + 6, Light);
        inventoryData.TypeSlots.Add(CharacterInventoryManager.WANDS_SLOTS_ID_PREFIX + 1, typeof(WandItem));
        inventoryData.Items.Add(CharacterInventoryManager.WANDS_SLOTS_ID_PREFIX + 1, wand);

        return inventoryData;
    }
}

