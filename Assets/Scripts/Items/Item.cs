using Unity.Netcode;
using UnityEngine;
public abstract class Item : NetworkBehaviour
{
    public ItemData _itemData;
    protected bool _inInventory;
    public Player _player;
    public Inventory _playerInventory;
    public bool _activeButton;
    public Rigidbody _rb;

    //Atributos para que floten
    public float _additionValue;
    public float _positionItemY;

    //Atributos
    protected int _amountItem = 1;
    protected string _nameItem;

    public override void OnNetworkSpawn()
    {
        PositionOnLand();
    }
    public void PositionOnLand() 
    {
        _positionItemY = transform.position.y;
        if (_positionItemY < 100 || _positionItemY > 100) 
        {
            _positionItemY = 100 + _additionValue;
            transform.position = new Vector3(transform.position.x, _positionItemY, transform.position.z);
        }
    }

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

    [Rpc(SendTo.Owner, InvokePermission = RpcInvokePermission.Everyone)]

    public void DespawnServerRpc() 
    {
        if (!IsOwner) return;
        NetworkObject.Despawn();//Se destruye el item.
    }

    private void DespawnItem()
    {
      _inInventory = _playerInventory.GetItemSaved(); //Tomamos el valor del "_inInventory" proveniente del Player
      if (_inInventory == true) // Si "_inInventory" es verdadero.
      {
          Debug.Log("Lo has recogido!");

            DespawnServerRpc();
        }
      else
      {

          Debug.Log("No lo agarraste");
      }
      Debug.Log($"Local: {NetworkManager.LocalClientId} | " + $"Owner: {NetworkObject.OwnerClientId} | " + $"IsOwner: {IsOwner}");
    }

    public abstract bool ItemAction(NetworkObject PlayerNet);
}
    
        

