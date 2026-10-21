using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
public class Inventory : NetworkBehaviour
{
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private GameObject _itemSelected;
    [SerializeField] private ItemData _currentItemData;
    public Dictionary<ItemData, int> _inventory = new Dictionary<ItemData, int>();
    private Transform _transformPlayer;
    private bool _slotsFull;
    public int _limitSlots;
    public override void OnNetworkSpawn()
    {
        _inventoryUI = GameObject.Find("BackgroundInventory").GetComponent<InventoryUI>();
    }

    public Dictionary<ItemData, int> GetPlayerInventory() 
    {
        return _inventory;
    }

    public void CheckInventorySlots(int slotsActually) 
    {
        Debug.Log("Slots Actually: "+slotsActually);

        if (slotsActually > _limitSlots)
        {
            _slotsFull = true;
        }
        else 
        {
            _slotsFull = false;
        }
    }
    public void AddItem(ItemData itemName, int itemAmount) 
    {
        if (itemName == null || _slotsFull) return;

        if (_inventory.ContainsKey(itemName))
        {
            _inventory[itemName] += itemAmount;
            Debug.Log("Suma cantidad a un item existente");
        }
        // Si no existe, lo agregamos como nuevo
        else
        {
            _inventory.Add(itemName, itemAmount);
            Debug.Log("Agrega item nuevo");
        }
    }

    public void SetItemSelected(GameObject currentItem)
    {
        _itemSelected = currentItem;
    }

    public void UpdateItem(ItemData itemName, int itemAmount)
    {
        if (itemName == null) return;

        if (_inventory.ContainsKey(itemName))
        {
            _inventory[itemName] -= itemAmount;
        }
        else
        {
            _inventory.Remove(itemName);
        }
    }

    //UTILIZACION DE ITEMS

    [ServerRpc]
    public void EquipItemServerRpc(uint itemId, string bodyPart, ulong idPlayer)
    {
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            if (client.ClientId == idPlayer) 
            {
                _transformPlayer = client.PlayerObject.GetComponent<Transform>();
                foreach (var networkPrefab in NetworkManager.Singleton.NetworkConfig.Prefabs.Prefabs)
                {
                    if (itemId == networkPrefab.SourcePrefabGlobalObjectIdHash)
                    {
                        Transform _transformPart = _transformPlayer.Find("MeshPlayer/" + bodyPart).GetComponent<Transform>(); //Tomamos el objeto en escena de la parte del cuerpo requerida.
                        GameObject itemEquiped = Instantiate(networkPrefab.Prefab, _transformPart);
                        itemEquiped.GetComponent<NetworkObject>().Spawn();
                        itemEquiped.GetComponent<NetworkObject>().TrySetParent(this.NetworkObject);
                        Debug.Log("Tenemos el transformPart de: " + _transformPart);
                    }
                }
            }
        }
    }

    public void Equip(GameObject itemRecived, string bodyPart, NetworkObject PlayerNet)
    {
        if (itemRecived != null) 
        {
            ulong newId = PlayerNet.OwnerClientId;
            _transformPlayer = this.GetComponent<Transform>();

            var idHash = itemRecived.GetComponent<NetworkObject>().PrefabIdHash;
           
            EquipItemServerRpc(idHash, bodyPart, newId);
        }

    }

    public void DropItem()
    {
        Debug.Log($"ENtramos al boton");
        if (_itemSelected == null) return;

            if (_itemSelected.GetComponent<SlotUI>() == null) return;

            _currentItemData = _itemSelected.GetComponent<SlotUI>()._itemData;

            if (_currentItemData == null) return;

            UpdateItem(_currentItemData, 1);
            GameObject _itemDrop = Instantiate(_currentItemData._itemPrefab, transform.position, Quaternion.identity);
            _itemDrop.GetComponent<NetworkObject>().Spawn();
            Debug.Log($"Se dropeo el item requerido");

         _itemSelected = null;
         _inventoryUI.RefreshPanel_UI();
    }
}
