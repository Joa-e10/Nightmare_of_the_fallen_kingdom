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

    public abstract void collected();

        /*else 
        {
           /* _amountObject = 0;
            _nameItem = "";
            _playerInventory.setInsertedValue(_amountObject);
            _playerInventory.setInsertedObject(_nameItem);
        }*/
    


    void Update()
    {
        
    }
}
