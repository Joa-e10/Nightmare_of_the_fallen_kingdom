using Unity.Services.Matchmaker.Models;
using UnityEngine;

public class ConsumableItem : Item
{

    void Start()
    {
        _nameItem = gameObject.tag;
        _player = GameObject.Find("Player").GetComponent<characters>();
        _playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
    }
    void Update()
    {
        
    }
}

