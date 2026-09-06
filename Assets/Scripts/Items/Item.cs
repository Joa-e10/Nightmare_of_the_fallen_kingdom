using Unity.Netcode;
using UnityEngine;
public abstract class Item : NetworkBehaviour
{
    public ItemData _itemData;
    protected bool _inInventory;
    public Player _player;
    public Inventory _playerInventory;
    public bool _activeButton;

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
    public void collected() //Metodo que permite que se recolecte el item.
    {
        DespawnItem();
    }
    private void DespawnItem()
    {
        _inInventory = _player.GetItemSaved(); //Tomamos el valor del "_inInventory" proveniente del Player
        if (_inInventory == true) // Si "_inInventory" es verdadero.
        {
            Debug.Log("Lo has recogido!");

            //GetComponent<NetworkObject>().Despawn(true);//Se destruye el item.
            Destroy(gameObject);
        }
        else
        {

            Debug.Log("No lo agarraste");
        }
    }

    public abstract void ItemAction(NetworkObject PlayerNet);
}
    
        

