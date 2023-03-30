using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneStarter : MonoBehaviour
{
    private void Awake()
    {
        Game.Inint(new InventoryManager());
    }

}
