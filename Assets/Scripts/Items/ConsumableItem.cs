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

    public override void collected()
    {

        _inInventory = _player.getIsCollectObject();

        if (_inInventory == true)
        {

            Debug.Log("Lo has recogido!");
            _playerInventory.setInsertedValue(_amountObject);
            _playerInventory.setInsertedObject(_nameItem);

        }

    }

    void Update()
    {
        collected();
    }
}

