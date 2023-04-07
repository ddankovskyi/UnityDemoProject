using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game
{
    static List<IGlobalManager> _globalManagers = new List<IGlobalManager>();

    public static T Get<T>() where T : class, IGlobalManager
    {
        foreach (var manager in _globalManagers)
        {
            if (manager is T castedComp)
                return castedComp;
        }
        return null;
    }

    public static bool Inint(IGlobalManager manager)
    {
        var managaerType = manager.GetType();
        if (_globalManagers.Any(m => m.GetType() == managaerType))
        {
            Debug.Log($"Manager with type {manager.GetType()} already registred");
            return false;
        }

        _globalManagers.Add(manager);
        manager.Init();
        return true;
    }

    // RemoveManager?
}
