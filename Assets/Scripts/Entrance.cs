using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Entrance : NetworkBehaviour
{
    [SerializeField] private ItemData _key;
    public enum axisChanged { X, Y, Z };
    public axisChanged _axis;
    [SerializeField] private Transform _transformMove;
    private float _limitOpen = 3f;
    private HashSet<ulong> _playersInside = new HashSet<ulong>();
    private void OnTriggerEnter(Collider other)
    {
        Inventory playerInventory = other.GetComponent<Inventory>();

        if (playerInventory == null) return;


        NetworkObject playerObject = playerInventory.GetComponentInParent<NetworkObject>();

        if (playerObject == null) return;

         foreach (KeyValuePair<ItemData, int> item in playerInventory.GetPlayerInventory()) 
         {
             if (item.Key._name == _key._name && item.Value > 0)
             {
                bool wasEmpty = _playersInside.Count == 0;

                _playersInside.Add(playerObject.OwnerClientId);
                if (wasEmpty && _playersInside.Count > 0)
                {
                   OpenGridServerRpc(item.Key._name);
                }

             }
         }
        
    }

    [ClientRpc]
    private void SetGridStateClientRpc(bool isOpen)
    {
        if (_transformMove == null) return;

        if (isOpen)
        {
            OpenPositions();
        }
        else 
        {
            ClosedPosition();
        }

            Debug.Log(
                $"Reja {(isOpen ? "abierta" : "cerrada")} | Cliente: {NetworkManager.Singleton.LocalClientId}"
            );
    }

    private void OnTriggerExit(Collider other)
    {
        Inventory playerInventory = other.GetComponent<Inventory>();

        if (playerInventory == null) return;

        NetworkObject playerObject = playerInventory.GetComponentInParent<NetworkObject>();

        if (playerObject == null) return;

        foreach (KeyValuePair<ItemData, int> item in playerInventory.GetPlayerInventory())
        {
            if (item.Key._name == _key._name && item.Value > 0)
            {
                _playersInside.Remove(playerObject.OwnerClientId);
                if (_playersInside.Count == 0)
                {
                    ClosedGridServerRpc(item.Key._name);
                }

            }
        }
    }
    [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    private void OpenGridServerRpc(string nameItem)
    {
        if (nameItem != _key._name) return;

        SetGridStateClientRpc(true);
    }
    [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    private void ClosedGridServerRpc(string nameItem)
    {
        if (nameItem != _key._name) return;

        SetGridStateClientRpc(false);
    }

    private void OpenPositions() 
    {
        if (_axis.ToString() == "Y")
        {
            _transformMove.position = new Vector3(transform.position.x, _transformMove.position.y + _limitOpen, transform.position.z);
        }
        else if ( _axis.ToString() == "X")
        {
            _transformMove.position = new Vector3(transform.position.x + _limitOpen, transform.position.y, _transformMove.position.z);
        }
        else
        {
            _transformMove.position = new Vector3(_transformMove.position.x, transform.position.y, transform.position.z + _limitOpen);
        }
    }

    private void ClosedPosition() 
    {
        if (_axis.ToString() == "Y")
        {
            _transformMove.position = new Vector3(transform.position.x, _transformMove.position.y - _limitOpen, transform.position.z);
        }
        else if (_axis.ToString() == "X")
        {
            _transformMove.position = new Vector3(transform.position.x - _limitOpen, transform.position.y, _transformMove.position.z);
        }
        else
        {
            _transformMove.position = new Vector3(_transformMove.position.x, transform.position.y, transform.position.z - _limitOpen);
        }
    }
}
