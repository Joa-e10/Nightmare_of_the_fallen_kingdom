using UnityEngine;

public class EquppableItem : Item
{
    
    void Start()
    {
        _nameItem = gameObject.tag;
        _player = GameObject.Find("Player").GetComponent<characters>();
        _playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
    }
    void Update()
    {
        collected();
    }
}
