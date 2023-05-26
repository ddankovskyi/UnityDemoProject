using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] BaseEnemy _prefab;
    [SerializeField] Character _character;
    BaseEnemy _son;
  
    // Update is called once per frame
    void Update()
    {
        if(_son == null)
        {
            _son = Instantiate(_prefab, transform);
            _son.target = _character.transform;
        }
    }
}
