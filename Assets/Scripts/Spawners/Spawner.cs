using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _listOfPositions = new List<Transform>();
    [SerializeField] private List<GameObject> _listOfEnemies = new List<GameObject>();
    [SerializeField]private int _levelRequired;
    public static event Action OnActiveSpawn;
    private bool _inicializate;
    private Player _player;
    private BoxCollider _bc;

    private void OnEnable()
    {
        
        OnActiveSpawn += SpawnEnemies;
        _bc = GetComponent<BoxCollider>();
    }

    private void OnDisable()
    {
        OnActiveSpawn -= SpawnEnemies;
    }

    private void OnTriggerEnter(Collider other)
    {
        _player = other.GetComponent<Player>();
        if (_player != null)
        {
            _inicializate = true;
           OnActiveSpawn?.Invoke();
        }
    }

    private void SpawnEnemies()
    {
       if (_inicializate)
       {
         _inicializate = false;
         GameObject _enemySpawned;
         int index = 0;

         foreach (GameObject enemy in _listOfEnemies)
         {
            _enemySpawned = Instantiate(enemy, _listOfPositions[index].position, Quaternion.identity);
            _enemySpawned.GetComponent<NetworkObject>().Spawn();
            index++;
         }
         _bc.enabled = false;
       }
    }
}
