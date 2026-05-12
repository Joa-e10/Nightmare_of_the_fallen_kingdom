using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class Item : MonoBehaviour
{
    protected bool _inInventory;
    protected characters _player;
    protected Inventory _playerInventory;

    //Atributos
    protected int _amountObject;
    protected string _nameItem;
    public virtual void collected() //Metodo que permite que se recolecte el item.
    {
        _inInventory = _player.getItemSaved(); //Tomamos el valor del "_inInventory" proveniente del Player
        if (_inInventory == true) // Si "_inInventory" es verdadero.
        {
            Debug.Log("Lo has recogido!");
            _playerInventory.setInsertedValue(_amountObject); //Cargamos al inventario la cantidad items recolectada.
            _playerInventory.setInsertedObject(_nameItem); //Cargamos al inventario el nombre del item recolectado.

            Destroy(gameObject); //Se destruye el item.
        }
        else 
        {
            
            Debug.Log("So un wachin!");
        }
    }
}
    
        

