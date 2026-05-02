using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected bool _inInventory;
    protected characters _player;
    protected Inventory _playerInventory;

    //Atributos
    protected int _amountObject;
    protected string _nameItem;

    void Start()
    {

    }

    protected void collected()
    {
        _inInventory = _player.getIsCollectObject();
        
        if (_inInventory == true)
        {

            Debug.Log("Lo has recogido!");
            _playerInventory.setInsertedValue(_amountObject);
            _playerInventory.setInsertedObject(_nameItem);
            Destroy(gameObject);
        }
    }


    void Update()
    {
        
    }
}
