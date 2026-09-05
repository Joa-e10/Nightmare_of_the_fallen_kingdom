using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]private GameObject _slotPrefab;
    [SerializeField]private Transform _inventoryLimit;
    private Image _backgroundInventory;
    private Inventory _playerInventory;
    private GameObject  _newSlot;
    private Player _player;
    private NetworkObject _playerObject;

    [SerializeField] private GameObject _craftingPanelB;
    [SerializeField] private GameObject _inventoryPanelB;

    [SerializeField] private CanvasManager _canvasManager;
    public void TakeOwner()
    {
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            if (client.PlayerObject.IsOwner)
            {

                _playerObject = client.PlayerObject;
                _playerInventory = client.PlayerObject.GetComponent<Inventory>();
            }
        }
    }

    public void Refresh() 
    {
        foreach (Transform t in _inventoryLimit)
        {
            Debug.Log("Entramos a limpiar");
            Destroy(t.gameObject);
        }
    }
    public void RefreshInventoryUI()
    {
        TakeOwner();

        _inventoryLimit = GameObject.Find("InventoryLimit").GetComponent<Transform>();
            foreach (Transform t in _inventoryLimit)
            {
                Destroy(t.gameObject);
            }

            for (int i = 0; i< _playerInventory._inventoryNames.Length;i++) 
            {
                if (_playerInventory._inventoryNames[i] != null) 
                {
                    _newSlot = Instantiate(_slotPrefab, _inventoryLimit);
                    SlotUI slotAttributes = _newSlot.GetComponent<SlotUI>();

                    slotAttributes._textname.text = _playerInventory._inventoryNames[i]._name;
                    slotAttributes._textamount.text = _playerInventory._inventoryQuantity[i].ToString();
                    slotAttributes._icon.sprite = _playerInventory._inventoryNames[i]._icon;
                    slotAttributes._itemData = _playerInventory._inventoryNames[i];
                    Debug.Log("Recorremos la activacion del prefab");
                }
            }
       
    }

    void Update()
    {
        
    }
}