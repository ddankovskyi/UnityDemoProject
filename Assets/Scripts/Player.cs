using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : IGlobalManager 
{
    PlayerData _data;

    public int InventorySize => _data.InventorySize;

    public void Init()
    {
        _data = new PlayerData();// TODO load _data from storage;
    }
}
