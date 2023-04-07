using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsRegistry", menuName = "ScriptableObjects/ItemsRegistry")]
public class ItemsRegistry : ScriptableObject
{
    [SerializeField]
    List<ItemRegistryData> ItemsData;


    public AItemObject GetPrefab(string itemId)
    {
        return ItemsData.Find(data => data.ItemId == itemId)?.Prefab;
    }

    public ItemUI GetPrefabUI(string itemId)
    {
        return ItemsData.Find(data => data.ItemId == itemId)?.PrefabUI;
    }

    [Serializable]
    public class ItemRegistryData
    {
        public string ItemId;
        public ItemUI PrefabUI;
        public AItemObject Prefab;
    }    
   
}
