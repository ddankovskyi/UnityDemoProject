using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage
{
    Dictionary<Type, object> _data;

    JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All
    };
    public T Get<T>() where T : class, new()
    {
        object obj;
        _data.TryGetValue(typeof(T), out obj);
        if(obj is T t)
        {
            return t;
        }
        return new T() ;
    }

    public Storage()
    {
        LoadAll();
    }

    void SaveAll()
    {
        string jsonData = JsonConvert.SerializeObject(_data, settings);
    }

    void LoadAll()
    {
        string jsonData = "";
        _data = JsonConvert.DeserializeObject<Dictionary<Type, object>>(jsonData, settings);
        if(_data == null)
        {
            _data = new Dictionary<Type, object>();
        }
    }
  

    public void Test()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        JsonConvert.DefaultSettings = () => settings;

        _data = new Dictionary<Type, object>();
        CharacterData characterData = new CharacterData();
        _data.Add(characterData.GetType(), characterData);
        string s = "SomeString";
        _data.Add(s.GetType(), s);

        string dataString = JsonConvert.SerializeObject(_data);
        _data = JsonConvert.DeserializeObject<Dictionary<Type, object>>(dataString);

        characterData = Get<CharacterData>();

        Debug.Log(characterData == null ? "Data is null" : "Inventory size is " + characterData.InventorySize);
    
    }


}
