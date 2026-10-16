using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Destructible : NetworkBehaviour, IDamageable
{
    [SerializeField]private int _currentHealth;
    [SerializeField]private int _maxHealth = 100;

    [SerializeField] private DestructibleData _data;

    public override void OnNetworkSpawn() 
    {
        _data.maxHealth = _maxHealth;
        _currentHealth = _maxHealth;

    }
    public void TakeDamage(int amount)
    {
        Debug.Log("Tenemos daño en el barril");
            _currentHealth -= amount; // Se resta el daño de la cantidad de vida actual.

            if (_currentHealth <= 0) // Cuando la vida menor o igual a cero morimos.
            {
                DroppingItems();
                GetComponent<NetworkObject>().Despawn(true);//Se destruye el item.
            }
    }

    private void DroppingItems() 
    {
        if (_data.typeDrop == DestructibleData.TypeDrop.None)
        {
            foreach (GameObject item in _data.itemsDrop)
            {
                GameObject itemSpawned = Instantiate(item, transform.position, Quaternion.identity);
                itemSpawned.GetComponent<NetworkObject>().Spawn(true);//Se destruye el item.
            }
        }
        else 
        {
            if (_data.dropItemQuantity <= _data.itemsDrop.Count) 
            {
                Debug.Log("ESTAN EN ESTADO DE RANDOM");
                for (int i = 0; i < _data.dropItemQuantity; i++)
                {
                    int randomValue = Random.Range(0, _data.itemsDrop.Count);

                    GameObject itemSpawned = Instantiate(_data.itemsDrop[randomValue], transform.position, Quaternion.identity);
                    itemSpawned.GetComponent<NetworkObject>().Spawn(true);
                }
            }
        }
    }
}
