using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class InventoryManager : IGlobalManager
{

    public const string DEFAULT_PAGE_ID = "defaultInvetoryPageId";
    InventoryData _data;
    ICompatibilityChecker _checker = new DefaultChecker();

    public void Save() { }
    public void Load() { }
    public void Init()
    {
        DebugLoad();
    }
    public bool CreateSlot(string slotId, string typeId = null) => CreateSlot(DEFAULT_PAGE_ID, slotId, typeId);
    public bool CreateSlot(string pageId, string slotId, string typeId)
    {
        var slot = new InventorySlot
        {
            TypeId = typeId
        };

        if (_data.InventoryPages[pageId].ContainsKey(slotId))
        {
            Debug.Log($"Slot with id {slotId} already exist on page {pageId}");
            return false;
        }
        _data.InventoryPages[pageId][slotId] = slot;
        return true;

    }
    public bool SwapItems(string slot1Id, string slot2Id) => SwapItems(DEFAULT_PAGE_ID, slot1Id, DEFAULT_PAGE_ID, slot2Id);
    public bool SwapItems(string page1Id, string slot1Id, string page2Id, string slot2Id)
    {
        var slot1 = GetSlot(page1Id, slot1Id);
        if (slot1 == null) return false;
        var slot2 = GetSlot(page2Id, slot2Id);
        if (slot2 == null) return false;

        bool isCompatible = true;

        if (slot1.Content != null)
            isCompatible &= _checker.CheckCompatibility(slot2.TypeId, slot1.Content.TypeId);
        if (slot2.Content != null)
            isCompatible &= _checker.CheckCompatibility(slot1.TypeId, slot2.Content.TypeId);

        if (!isCompatible) return false;

        var item = slot1.Content;
        slot1.Content = slot2.Content;
        slot2.Content = item;
        return true;

    }

    public bool PutItem(string slotId, InventoryItem item) => PutItem(DEFAULT_PAGE_ID, slotId, item);
    public bool PutItem(string pageId, string slotId, InventoryItem item)
    {

        var slot = GetSlot(pageId, slotId);
        if (slot == null) return false;

        if (item == null)
        {
            slot.Content = null;
            return true;
        }
        else if (_checker.CheckCompatibility(slot.TypeId, item.TypeId))
        {
            slot.Content = item;
            return true;
        }
        else return false;
    }

    public InventoryItem GetItem(string slotId) => GetItem(DEFAULT_PAGE_ID, slotId);
    public InventoryItem GetItem(string pageId, string slotId)
    {
        var slot = GetSlot(pageId, slotId);
        if (slot == null) return null;
        return slot.Content;
    }
    public void RemoveItem(string slotId) => RemoveItem(DEFAULT_PAGE_ID, slotId);
    public void RemoveItem( string pageId, string slotId)
    {
        var slot = GetSlot(pageId, slotId);
        if (slot == null) return;
        slot.Content = null;
    }


    InventorySlot GetSlot(string pageId, string slotId)
    {
        if (pageId == null || pageId.Length == 0) pageId = DEFAULT_PAGE_ID;
        _data.InventoryPages.TryGetValue(pageId, out var page);
        if (page == null)
        {
            Debug.Log($"Inventory page with id {pageId} not found");
            return null;
        }

        page.TryGetValue(slotId, out var slot);
        if (slot == null)
        {
            Debug.Log($"Slot with page id {pageId} and slot id {slotId} not found");
            return null;
        }

        return slot;
    }

    public class InventoryData
    {
        public Dictionary<string, Dictionary<string, InventorySlot>> InventoryPages;
    }

    public void DebugLoad()
    {
        var pages = new Dictionary<string, Dictionary<string, InventorySlot>>();
        var page = new Dictionary<string, InventorySlot>();
        pages[DEFAULT_PAGE_ID] = page;

        _data = new InventoryData
        {
            InventoryPages = pages
        };

        InventoryItem item1 = new InventoryItem
        {
            Id = "BaseSpell1",
            TypeId = InventoryItemType.Spell.ToString()
        };
        InventoryItem item2 = new InventoryItem
        {
            Id = "BaseSpell2",
            TypeId = InventoryItemType.Spell.ToString()
        };

        for (int i = 0; i < 10; i++)
        {
            CreateSlot("MainInventory_" + i);
        }


        PutItem("MainInventory_1", item1);
        PutItem("MainInventory_2", item2);

    }

    public class WandData
    {
        public string Name = "_";
        public string IconId = "DefaultWandIcon";
        public bool Shuffle = false;
        public int SpelsCast = 1;
        public float CastDelay = 0.3f;
        public float RechargeTime = 0.5f;
        public int MaxMana = 100;
        public int ManaCharge = 25;
        public int Capasity = 5;
        public float Spread = 0f;
        public List<SpellData> Spells = Enumerable.Repeat<SpellData>(null, 5).ToList();
    }

    public class SpellData
    {
        public string SpellId;
        public int ChargesCount = -1;

        public SpellData(string spellId)
        {
            SpellId = spellId;
        }
    }
}
