using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class Item : MonoBehaviour
{
    public ItemData _itemData;
    protected bool _inInventory;
    protected Player _player;
    protected Inventory _playerInventory;

    //Atributos
    protected int _amountItem = 1;
    protected string _nameItem;

    public bool getInInventory() //Retornamos estado de "InInventory"
    {
        return _inInventory;
    }
    public int getItemQuantity() //Retornamos estado de "InInventory"
    {
        return _amountItem;
    }
    public virtual void collected() //Metodo que permite que se recolecte el item.
    {
        _inInventory = _player.getItemSaved(); //Tomamos el valor del "_inInventory" proveniente del Player
        if (_inInventory == true) // Si "_inInventory" es verdadero.
        {
            Debug.Log("Lo has recogido!");

            Destroy(gameObject); //Se destruye el item.
        }
        else 
        {
            
            Debug.Log("No lo agarraste");
        }
    }
}
    
        

