using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : NetworkBehaviour
{
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private GameObject _itemSelected;
    [SerializeField] private ItemData _currentItemData;
    public ItemData[] _inventoryNames = new ItemData[15];
    public int[] _inventoryQuantity = new int[15];
    public List<ItemData> _hud = new List<ItemData>();
   //public Dictionary<ItemData, int> _hud = new Dictionary<ItemData, int>();
    private Transform _transformPlayer;
    void Start()
    {
        
    }
    public override void OnNetworkSpawn()
    {
        _inventoryUI = GameObject.Find("BackgroundInventory").GetComponent<InventoryUI>();
    }
    public void AddItem(ItemData itemName, int itemAmount) 
    {
        for (int i = 0; i<_inventoryNames.Length; i++)
        {
            if (itemName == null) return;
            {
                if (_inventoryNames[i] == itemName && itemName._type.ToString() != "equippable")
                {
                    _inventoryNames[i] = itemName;
                    _inventoryQuantity[i] += itemAmount;
                    itemName = null;
                    itemAmount = 0;
                }
                else if (_inventoryNames[i] == null)
                {
                    _inventoryNames[i] = itemName;
                    _inventoryQuantity[i] = itemAmount;
                    itemName = null;
                    itemAmount = 0;
                }
            }
        }
    }

    public void SetItemSelected(GameObject currentItem)
    {
        _itemSelected = currentItem;
    }

    public void UpdateItem(ItemData itemName, int itemAmount)
    {
        for (int i = 0; i < _inventoryNames.Length; i++)
        {
            if (itemName == null) return;
            {
                if (_inventoryNames[i] == itemName)
                {
                    _inventoryQuantity[i] -= itemAmount;
                    itemName = null;

                    if (_inventoryQuantity[i] < 1) 
                    {
                        _inventoryNames[i] = null;
                        _inventoryQuantity[i] = 0;
                    }
                }
            }
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
            Debug.Log("Tenemos el Id: " + newId);
            Debug.Log("Tenemos la parte: " + bodyPart);
            Debug.Log("Tenemos el transform de: " + _transformPlayer);
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
         _inventoryUI.RefreshInventoryUI();
    }
}
